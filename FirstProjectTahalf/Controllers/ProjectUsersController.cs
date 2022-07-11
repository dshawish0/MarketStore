using FirstProjectTahalf.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProjectTahalf.Controllers
{
    public class ProjectUsersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProjectUsersController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;

        }

        // GET: ProjectUsers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.ProjectUsers.Include(p => p.Home).Include(p => p.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProjectUsers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectUser = await _context.ProjectUsers
                .Include(p => p.Home)
                .Include(p => p.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (projectUser == null)
            {
                return NotFound();
            }

            return View(projectUser);
        }

        // GET: ProjectUsers/Create
        public IActionResult Create()
        {
            ViewData["Homeid"] = new SelectList(_context.ProjectHomes, "Homeid", "Homeid");
            ViewData["Roleid"] = new SelectList(_context.ProjectRoles, "Roleid", "Roleid");
            return View();
        }

        // POST: ProjectUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Fname,Lname,Email,Password,ImagePath,Roleid,Homeid")] ProjectUser projectUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectUser);
                await _context.SaveChangesAsync();



            }
            ViewData["Homeid"] = new SelectList(_context.ProjectHomes, "Homeid", "Homeid", projectUser.Homeid);
            ViewData["Roleid"] = new SelectList(_context.ProjectRoles, "Roleid", "Roleid", projectUser.Roleid);
            return View(projectUser);
        }

        // GET: ProjectUsers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;

            if (id == null)
            {
                return NotFound();
            }

            var projectUser = await _context.ProjectUsers.FindAsync(id);
            if (projectUser == null)
            {
                return NotFound();
            }
            ViewData["Homeid"] = new SelectList(_context.ProjectHomes, "Homeid", "Homeid", projectUser.Homeid);
            ViewData["Roleid"] = new SelectList(_context.ProjectRoles, "Roleid", "Roleid", projectUser.Roleid);
            return View(projectUser);
        }

        // POST: ProjectUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Fname,Lname,Email,Password,ImagePath,ImgFileProfile")] ProjectUser projectUser)
        {
            if (id != projectUser.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //Console.WriteLine(projectUser.ToString());
                try
                {
                    if (projectUser.ImgFileProfile != null)
                    {
                        string w3path = webHostEnvironment.WebRootPath;
                        string img = Guid.NewGuid() + "_" + projectUser.ImgFileProfile.FileName;
                        string path = Path.Combine(w3path + "/images/", img);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await projectUser.ImgFileProfile.CopyToAsync(fileStream);
                        }
                        projectUser.ImagePath = img;
                        projectUser.Roleid = 1;
                        projectUser.Homeid = null;
                    }
                    _context.Update(projectUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectUserExists(projectUser.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("tables", "Admin");
            }
            ViewData["Homeid"] = new SelectList(_context.ProjectHomes, "Homeid", "Homeid", projectUser.Homeid);
            ViewData["Roleid"] = new SelectList(_context.ProjectRoles, "Roleid", "Roleid", projectUser.Roleid);
            return View(projectUser);
        }

        // GET: ProjectUsers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;
            if (id == null)
            {
                return NotFound();
            }

            var projectUser = await _context.ProjectUsers
                .Include(p => p.Home)
                .Include(p => p.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (projectUser == null)
            {
                return NotFound();
            }

            return View(projectUser);
        }

        // POST: ProjectUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var projectUser = await _context.ProjectUsers.FindAsync(id);
            _context.ProjectUsers.Remove(projectUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("tables", "Admin");
        }

        private bool ProjectUserExists(decimal id)
        {
            return _context.ProjectUsers.Any(e => e.Userid == id);
        }
    }
}
