using FirstProjectTahalf.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProjectTahalf.Controllers
{

    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public AdminController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
     
        public async Task<IActionResult> editHomePage(decimal? id)
        {

            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> editHomePage(decimal id, [Bind("Homeid,Email,Freeshippinglimit,Phonenumber,Supportworktime,Logoimg,Homeimg,Address")] ProjectHome projectHome)
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
                return RedirectToAction("Index", "Admin");
            }
            return View(projectHome);
        }

        public IActionResult Index()
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();

            float profit = 0;
            var item = _context.ProjectUsers.Where(x => x.Roleid == 1).Count();
            var item2 = _context.ProjectProducts.Count();
            var item3= _context.ProjectProducts.ToList();

            var item4 = _context.ProjectStores.Count();

            foreach (var prof in item3)
            {
                profit += (float)prof.Prosale - (float)prof.Propricse;
            }

            ViewBag.profit = profit;
            ViewBag.numofusers = item;
            ViewBag.numofpro = item2;
            ViewBag.numofstore = item4;

            ViewBag.user = item5;

            var store = _context.ProjectStores.ToList();

            return View(store);
        }
        public IActionResult tables()
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            var item = _context.ProjectProducts.ToList();
            var item2 = _context.ProjectUsers.Where(x => x.Roleid == 1).ToList();
            var item3 = _context.ProjectCategories.ToList();
            var item4 = _context.ProjectTestimonials.ToList();

            var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectUser>,
                IEnumerable<FirstProjectTahalf.Models.ProjectCategory>,
                IEnumerable<FirstProjectTahalf.Models.ProjectProduct>,IEnumerable<FirstProjectTahalf.Models.ProjectTestimonial>>(item2, item3, item,item4);
            ViewBag.user = item5;
            return View(getAll);
        }

        public async Task<IActionResult> editProfile(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectUser = await _context.ProjectUsers.FindAsync(id);
            if (projectUser == null)
            {
                return NotFound();
            }
            return View(projectUser);
        }

        [HttpPost]
        public async Task<IActionResult> edit(decimal? id, string Fname, string Lname, string email, string password, IFormFile ImgFileProfile)
        {
            ProjectUser item = _context.ProjectUsers.Where(x => x.Userid == id).SingleOrDefault();
            string ImgPath = "";
            if (item == null)
                return NotFound();

            if (ImgFileProfile != null)
            {
                string w3path = webHostEnvironment.WebRootPath;
                string img = Guid.NewGuid() + "_" + ImgFileProfile.FileName;
                string path = Path.Combine(w3path + "/images/", img);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await ImgFileProfile.CopyToAsync(fileStream);
                }
                ImgPath = img;
            }

            //ProjectUser projectUser = new ProjectUser();
            item.Password = password;
            item.Fname = Fname;
            item.Lname = Lname;
            item.Email = email;
            item.ImagePath = ImgPath;
            item.Roleid = 2;
            item.Homeid = 21;

            //_context.ProjectUsers.FromSqlRaw($"UPDATE project_user SET password = '{password}', Fname = '{Fname}', Lname = '{Lname}', email='{email}', image_path='{ImgPath}' WHERE userid ={item.Userid}");
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context.Update(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        

        public IActionResult search(DateTime? startDate, DateTime? endDate)
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;

            if (startDate == null && endDate == null)
            {
                var item = _context.ProjectPayments.ToList();
                var item21 = _context.ProjectOrders.ToList();
                var item31 = _context.ProjectUsers.ToList();

                var getAll0 = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectPayment>,
                   IEnumerable<FirstProjectTahalf.Models.ProjectOrder>,
                   IEnumerable<FirstProjectTahalf.Models.ProjectUser>>(item, item21, item31);
                return View(getAll0);
            }
            else if (startDate != null && endDate == null)
            {
                var result1 = _context.ProjectPayments.Where(x => x.Datee >= startDate).ToList();
                var item22 = _context.ProjectOrders.ToList();
                var item32 = _context.ProjectUsers.ToList();

                var getAll1 = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectPayment>,
                   IEnumerable<FirstProjectTahalf.Models.ProjectOrder>,
                   IEnumerable<FirstProjectTahalf.Models.ProjectUser>>(result1, item22, item32);
                return View(getAll1);
            }
            else if (startDate == null && endDate != null)
            {
                var result2 = _context.ProjectPayments.Where(x => x.Datee <= endDate).ToList();
                var item23 = _context.ProjectOrders.ToList();
                var item33 = _context.ProjectUsers.ToList();

                var getAll2 = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectPayment>,
                   IEnumerable<FirstProjectTahalf.Models.ProjectOrder>,
                   IEnumerable<FirstProjectTahalf.Models.ProjectUser>>(result2, item23, item33);
                return View(getAll2);
            }


            var result = _context.ProjectPayments.Where(x => x.Datee >= startDate && x.Datee <= endDate).ToList();
            var item2 = _context.ProjectOrders.ToList();
            var item3 = _context.ProjectUsers.ToList();

            var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectPayment>,
               IEnumerable<FirstProjectTahalf.Models.ProjectOrder>,
               IEnumerable<FirstProjectTahalf.Models.ProjectUser>>(result, item2, item3);

            
                return View(getAll);
        }


        public IActionResult orders()
        {
            var item5 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            ViewBag.user = item5;

            var item = _context.ProjectPayments.ToList();
            var item2 = _context.ProjectOrders.ToList();
            var item3 = _context.ProjectUsers.ToList();

            var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectPayment>,
               IEnumerable<FirstProjectTahalf.Models.ProjectOrder>,
               IEnumerable<FirstProjectTahalf.Models.ProjectUser>>(item, item2, item3);

            return View(getAll);
        }

        private bool ProjectHomeExists(decimal homeid)
        {
            throw new NotImplementedException();
        }
    }
}
