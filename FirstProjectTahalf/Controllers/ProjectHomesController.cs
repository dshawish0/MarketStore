using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProjectTahalf.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FirstProjectTahalf.Controllers
{
    public class ProjectHomesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProjectHomesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: ProjectHomes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectHomes.ToListAsync());
        }

        // GET: ProjectHomes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectHome = await _context.ProjectHomes
                .FirstOrDefaultAsync(m => m.Homeid == id);
            if (projectHome == null)
            {
                return NotFound();
            }

            return View(projectHome);
        }

        // GET: ProjectHomes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectHomes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Homeid,Email,Freeshippinglimit,Phonenumber,Supportworktime,Logoimg,Homeimg,ImgFileHome,ImgFileLogo,Address")] ProjectHome projectHome)
        {
            if (ModelState.IsValid)
            {
                if (projectHome.ImgFileHome != null && projectHome.ImgFileLogo != null)
                {
                    string w3path = webHostEnvironment.WebRootPath;
                    string img = Guid.NewGuid() + "_" + projectHome.ImgFileHome.FileName;
                    string img2 = Guid.NewGuid() + "_" + projectHome.ImgFileLogo.FileName;
                    string path = Path.Combine(w3path + "/images/", img);
                    string path2 = Path.Combine(w3path + "/images/", img2);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await projectHome.ImgFileHome.CopyToAsync(fileStream);
                    }

                    using (var fileStream = new FileStream(path2, FileMode.Create))
                    {
                        await projectHome.ImgFileLogo.CopyToAsync(fileStream);
                    }

                    projectHome.Homeimg = img;
                    projectHome.Logoimg = img2;

                    _context.Add(projectHome);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(projectHome);
        }

        // GET: ProjectHomes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectHome = await _context.ProjectHomes.FindAsync(id);
            if (projectHome == null)
            {
                return NotFound();
            }
            return View(projectHome);
        }

        // POST: ProjectHomes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Homeid,Email,Freeshippinglimit,Phonenumber,Supportworktime,Logoimg,Homeimg,Address")] ProjectHome projectHome)
        {
            if (id != projectHome.Homeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectHome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectHomeExists(projectHome.Homeid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projectHome);
        }

        // GET: ProjectHomes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectHome = await _context.ProjectHomes
                .FirstOrDefaultAsync(m => m.Homeid == id);
            if (projectHome == null)
            {
                return NotFound();
            }

            return View(projectHome);
        }

        // POST: ProjectHomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var projectHome = await _context.ProjectHomes.FindAsync(id);
            _context.ProjectHomes.Remove(projectHome);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectHomeExists(decimal id)
        {
            return _context.ProjectHomes.Any(e => e.Homeid == id);
        }
    }
}
