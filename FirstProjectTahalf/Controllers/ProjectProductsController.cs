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
    public class ProjectProductsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProjectProductsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: ProjectProducts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.ProjectProducts.Include(p => p.Category).Include(p => p.Store);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProjectProducts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectProduct = await _context.ProjectProducts
                .Include(p => p.Category)
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (projectProduct == null)
            {
                return NotFound();
            }

            return View(projectProduct);
        }

        // GET: ProjectProducts/Create
        public IActionResult Create()
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;
            ViewData["Categoryid"] = new SelectList(_context.ProjectCategories, "Categoryid", "Name");
            ViewData["Storeid"] = new SelectList(_context.ProjectStores, "Storeid", "Namestore");
            return View();
        }

        // POST: ProjectProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Productid,Productname,ImagePath,Quntitiy,Propricse,Prosale,Discount,Productdescription,Storeid,Categoryid,ImgFile")] ProjectProduct projectProduct, string StoreName , string CategoryName)
        {
            if (ModelState.IsValid)
            {
                if (projectProduct.ImgFile != null)
                {
                    string w3path = webHostEnvironment.WebRootPath;
                    string img = Guid.NewGuid() + "_" + projectProduct.ImgFile.FileName;
                    string path = Path.Combine(w3path + "/images/", img);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await projectProduct.ImgFile.CopyToAsync(fileStream);
                    }
                    projectProduct.ImagePath = img;


                    _context.Add(projectProduct);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("tables", "Admin");
                }
            }
            ViewData["Categoryid"] = new SelectList(_context.ProjectCategories, "Categoryid", "Categoryid", projectProduct.Categoryid);
            ViewData["Storeid"] = new SelectList(_context.ProjectStores, "Storeid", "Storeid", projectProduct.Storeid);
            return View(projectProduct);
        }

        // GET: ProjectProducts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;
            if (id == null)
            {
                return NotFound();
            }

            var projectProduct = await _context.ProjectProducts.FindAsync(id);
            if (projectProduct == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.ProjectCategories, "Categoryid", "Name", projectProduct.Categoryid);
            ViewData["Storeid"] = new SelectList(_context.ProjectStores, "Storeid", "Namestore", projectProduct.Storeid);
            return View(projectProduct);
        }

        // POST: ProjectProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Productid,Productname,ImagePath,Quntitiy,Propricse,Prosale,Discount,Productdescription,Storeid,Categoryid,ImgFile")] ProjectProduct projectProduct)
        {
            if (id != projectProduct.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (projectProduct.ImgFile != null)
                    {
                        string w3path = webHostEnvironment.WebRootPath;
                        string img = Guid.NewGuid() + "_" + projectProduct.ImgFile.FileName;
                        string path = Path.Combine(w3path + "/images/", img);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await projectProduct.ImgFile.CopyToAsync(fileStream);
                        }
                        projectProduct.ImagePath = img;
                    }
                    _context.Update(projectProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectProductExists(projectProduct.Productid))
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
            ViewData["Categoryid"] = new SelectList(_context.ProjectCategories, "Categoryid", "Categoryid", projectProduct.Categoryid);
            ViewData["Storeid"] = new SelectList(_context.ProjectStores, "Storeid", "Address", projectProduct.Storeid);
            return View(projectProduct);
        }

        // GET: ProjectProducts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;
            if (id == null)
            {
                return NotFound();
            }

            var projectProduct = await _context.ProjectProducts
                .Include(p => p.Category)
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (projectProduct == null)
            {
                return NotFound();
            }

            return View(projectProduct);
        }

        // POST: ProjectProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var projectProduct = await _context.ProjectProducts.FindAsync(id);
            _context.ProjectProducts.Remove(projectProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("tables", "Admin");
        }

        private bool ProjectProductExists(decimal id)
        {
            return _context.ProjectProducts.Any(e => e.Productid == id);
        }
    }
}
