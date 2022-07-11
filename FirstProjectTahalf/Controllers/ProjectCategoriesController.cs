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
using Microsoft.AspNetCore.Http;

namespace FirstProjectTahalf.Controllers
{
    public class ProjectCategoriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProjectCategoriesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: ProjectCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectCategories.ToListAsync());
        }

        // GET: ProjectCategories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectCategory = await _context.ProjectCategories
                .FirstOrDefaultAsync(m => m.Categoryid == id);
            if (projectCategory == null)
            {
                return NotFound();
            }

            return View(projectCategory);
        }

        // GET: ProjectCategories/Create
        public IActionResult Create()
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;
            return View();
        }

        // POST: ProjectCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Categoryid,Name,ImagePath,ImgFileCategory")] ProjectCategory projectCategory)
        {
            if (ModelState.IsValid)
            {
                if (projectCategory.ImgFileCategory != null)
                {
                    string w3path = webHostEnvironment.WebRootPath;
                    string img = Guid.NewGuid() + "_" + projectCategory.ImgFileCategory.FileName;
                    string path = Path.Combine(w3path + "/images/", img);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await projectCategory.ImgFileCategory.CopyToAsync(fileStream);
                    }
                    projectCategory.ImagePath = img;
                    _context.Add(projectCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("tables", "Admin");
                }
            }
            return View(projectCategory);
        }

        // GET: ProjectCategories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;
            if (id == null)
            {
                return NotFound();
            }

            var projectCategory = await _context.ProjectCategories.FindAsync(id);
            if (projectCategory == null)
            {
                return NotFound();
            }
            return View(projectCategory);
        }

        // POST: ProjectCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Categoryid,Name,ImagePath,ImgFileCategory")] ProjectCategory projectCategory)
        {
            if (id != projectCategory.Categoryid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (projectCategory.ImgFileCategory != null)
                    {
                        string w3path = webHostEnvironment.WebRootPath;
                        string img = Guid.NewGuid() + "_" + projectCategory.ImgFileCategory.FileName;
                        string path = Path.Combine(w3path + "/images/", img);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await projectCategory.ImgFileCategory.CopyToAsync(fileStream);
                        }
                        projectCategory.ImagePath = img;
                    }
                    _context.Update(projectCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectCategoryExists(projectCategory.Categoryid))
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
            return View(projectCategory);
        }

        // GET: ProjectCategories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;
            if (id == null)
            {
                return NotFound();
            }

            var projectCategory = await _context.ProjectCategories
                .FirstOrDefaultAsync(m => m.Categoryid == id);
            if (projectCategory == null)
            {
                return NotFound();
            }

            return View(projectCategory);
        }

        // POST: ProjectCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var projectCategory = await _context.ProjectCategories.FindAsync(id);
            _context.ProjectCategories.Remove(projectCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("tables", "Admin");
        }

        private bool ProjectCategoryExists(decimal id)
        {
            return _context.ProjectCategories.Any(e => e.Categoryid == id);
        }
    }
}
