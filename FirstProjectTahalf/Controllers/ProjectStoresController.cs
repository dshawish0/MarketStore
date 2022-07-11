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
    public class ProjectStoresController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public ProjectStoresController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: ProjectStores
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectStores.ToListAsync());
        }

        // GET: ProjectStores/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectStore = await _context.ProjectStores
                .FirstOrDefaultAsync(m => m.Storeid == id);
            if (projectStore == null)
            {
                return NotFound();
            }

            return View(projectStore);
        }

        // GET: ProjectStores/Create
        public IActionResult Create()
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;
            return View();
        }

        // POST: ProjectStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Storeid,Namestore,Email,Phonenumber,Opentime,Address,ImagePath,ImgFile")] ProjectStore projectStore)
        {
            if (ModelState.IsValid)
            {
                var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
                ViewBag.user = item5;

                if (projectStore.ImgFile != null)
                {
                    string w3path = webHostEnvironment.WebRootPath;
                    string img = Guid.NewGuid() + "_" + projectStore.ImgFile.FileName;
                    string path = Path.Combine(w3path + "/images/", img);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await projectStore.ImgFile.CopyToAsync(fileStream);
                    }
                    projectStore.ImagePath = img;
                    _context.Add(projectStore);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View(projectStore);
        }

        // GET: ProjectStores/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;
            if (id == null)
            {
                return NotFound();
            }

            var projectStore = await _context.ProjectStores.FindAsync(id);
            if (projectStore == null)
            {
                return NotFound();
            }
            return View(projectStore);
        }

        // POST: ProjectStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Storeid,Namestore,Email,Phonenumber,Opentime,Address,ImagePath")] ProjectStore projectStore)
        {
            if (id != projectStore.Storeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectStoreExists(projectStore.Storeid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Admin");
            }
            return View(projectStore);
        }

        // GET: ProjectStores/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;

            if (id == null)
            {
                return NotFound();
            }

            var projectStore = await _context.ProjectStores
                .FirstOrDefaultAsync(m => m.Storeid == id);
            if (projectStore == null)
            {
                return NotFound();
            }

            return View(projectStore);
        }

        // POST: ProjectStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var projectStore = await _context.ProjectStores.FindAsync(id);
            _context.ProjectStores.Remove(projectStore);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        private bool ProjectStoreExists(decimal id)
        {
            return _context.ProjectStores.Any(e => e.Storeid == id);
        }
    }
}
