using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstProjectTahalf.Models;
using Microsoft.AspNetCore.Http;

namespace FirstProjectTahalf.Controllers
{
    public class ProjectTestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public ProjectTestimonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ProjectTestimonials
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.ProjectTestimonials.Include(p => p.Product).Include(p => p.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProjectTestimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTestimonial = await _context.ProjectTestimonials
                .Include(p => p.Product)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (projectTestimonial == null)
            {
                return NotFound();
            }

            return View(projectTestimonial);
        }

        // GET: ProjectTestimonials/Create
        public IActionResult Create()
        {
            ViewData["Productid"] = new SelectList(_context.ProjectProducts, "Productid", "Productid");
            ViewData["Userid"] = new SelectList(_context.ProjectUsers, "Userid", "Email");
            return View();
        }

        // POST: ProjectTestimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rating,TestId,Message,Status,Publishdate,Userid,Productid")] ProjectTestimonial projectTestimonial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectTestimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Productid"] = new SelectList(_context.ProjectProducts, "Productid", "Productid", projectTestimonial.Productid);
            ViewData["Userid"] = new SelectList(_context.ProjectUsers, "Userid", "Email", projectTestimonial.Userid);
            return View(projectTestimonial);
        }

        // GET: ProjectTestimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTestimonial = await _context.ProjectTestimonials.FindAsync(id);
            if (projectTestimonial == null)
            {
                return NotFound();
            }
            ViewData["Productid"] = new SelectList(_context.ProjectProducts, "Productid", "Productid", projectTestimonial.Productid);
            ViewData["Userid"] = new SelectList(_context.ProjectUsers, "Userid", "Email", projectTestimonial.Userid);
            return View(projectTestimonial);
        }

        // POST: ProjectTestimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Rating,TestId,Message,Status,Publishdate,Userid,Productid")] ProjectTestimonial projectTestimonial)
        {
            if (id != projectTestimonial.TestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTestimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTestimonialExists(projectTestimonial.TestId))
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
            ViewData["Productid"] = new SelectList(_context.ProjectProducts, "Productid", "Productid", projectTestimonial.Productid);
            ViewData["Userid"] = new SelectList(_context.ProjectUsers, "Userid", "Email", projectTestimonial.Userid);
            return View(projectTestimonial);
        }

        // GET: ProjectTestimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;

            if (id == null)
            {
                return NotFound();
            }

            var projectTestimonial = await _context.ProjectTestimonials
                .Include(p => p.Product)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.TestId == id);
            if (projectTestimonial == null)
            {
                return NotFound();
            }

            return View(projectTestimonial);
        }

        // POST: ProjectTestimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var projectTestimonial = await _context.ProjectTestimonials.FindAsync(id);
            _context.ProjectTestimonials.Remove(projectTestimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        private bool ProjectTestimonialExists(decimal id)
        {
            return _context.ProjectTestimonials.Any(e => e.TestId == id);
        }
    }
}
