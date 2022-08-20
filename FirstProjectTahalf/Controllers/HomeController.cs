using FirstProjectTahalf.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FirstProjectTahalf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var it = _context.ProjectOrders.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            int it2 = 0;
            if (it != null)
            {
                it2 = _context.ProjectProductOrders.Where(x => x.Orderid == it.Orderid).ToList().Count;
                HttpContext.Session.SetInt32("cartItemsCount", it2);
            }
            else
            {
                
                HttpContext.Session.SetInt32("cartItemsCount", it2);
            }

            ViewBag.cartItemsCount = HttpContext.Session.GetInt32("cartItemsCount");
            var item2 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            var item1 = _context.ProjectHomes.ToList();
            var item3 = _context.ProjectCategories.ToList();
            var item4 = _context.ProjectProducts.ToList();
            var item5 = _context.ProjectStores.ToList();
            var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectHome>, FirstProjectTahalf.Models.ProjectUser,
                IEnumerable<FirstProjectTahalf.Models.ProjectCategory>, 
                IEnumerable<FirstProjectTahalf.Models.ProjectProduct>, 
                IEnumerable<FirstProjectTahalf.Models.ProjectStore>, FirstProjectTahalf.Models.ProjectStore, 
                FirstProjectTahalf.Models.ProjectProduct> 
                (item1, item2, item3, item4, item5,null,null);

            return View(getAll);

        }
        public IActionResult Shops()
        {
            ViewBag.cartItemsCount = HttpContext.Session.GetInt32("cartItemsCount");
            var item2 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            var item1 = _context.ProjectHomes.ToList();
            var item3 = _context.ProjectCategories.ToList();
            var item4 = _context.ProjectProducts.ToList();
            var item5 = _context.ProjectStores.ToList();
            var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectHome>, FirstProjectTahalf.Models.ProjectUser,
                 IEnumerable<FirstProjectTahalf.Models.ProjectCategory>,
                 IEnumerable<FirstProjectTahalf.Models.ProjectProduct>,
                 IEnumerable<FirstProjectTahalf.Models.ProjectStore>, FirstProjectTahalf.Models.ProjectStore,
                 FirstProjectTahalf.Models.ProjectProduct>
                 (item1, item2, item3, item4, item5, null, null) ;

            return View(getAll);
        }

        public async Task<IActionResult> shopsGrid(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectStore = await _context.ProjectStores.FindAsync(id);

            if (projectStore == null)
            {
                return NotFound();
            }
            ViewBag.cartItemsCount = HttpContext.Session.GetInt32("cartItemsCount");
            var item2 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            var item1 = _context.ProjectHomes.ToList();
            var item3 = _context.ProjectCategories.ToList();
            var item4 = _context.ProjectProducts.ToList();
            var item5 = _context.ProjectStores.ToList();
            var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectHome>, FirstProjectTahalf.Models.ProjectUser,
                IEnumerable<FirstProjectTahalf.Models.ProjectCategory>,
                IEnumerable<FirstProjectTahalf.Models.ProjectProduct>,
                IEnumerable<FirstProjectTahalf.Models.ProjectStore>, FirstProjectTahalf.Models.ProjectStore,
                FirstProjectTahalf.Models.ProjectProduct>
                (item1, item2, item3, item4, item5, projectStore, null) ;

            return View(getAll);
            //return View(projectStore);
        }

            public async Task<IActionResult> shopDetails(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ProjectProduct = await _context.ProjectProducts.FindAsync(id);

            if (ProjectProduct == null)
            {
                return NotFound();
            }
            ViewBag.cartItemsCount = HttpContext.Session.GetInt32("cartItemsCount");
            var item2 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            var item1 = _context.ProjectHomes.ToList();
            var item3 = _context.ProjectCategories.ToList();
            var item4 = _context.ProjectProducts.ToList();
            var item5 = _context.ProjectStores.ToList();
            var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectHome>, FirstProjectTahalf.Models.ProjectUser,
                 IEnumerable<FirstProjectTahalf.Models.ProjectCategory>,
                 IEnumerable<FirstProjectTahalf.Models.ProjectProduct>,
                 IEnumerable<FirstProjectTahalf.Models.ProjectStore>, FirstProjectTahalf.Models.ProjectStore,
                 FirstProjectTahalf.Models.ProjectProduct>
                 (item1, item2, item3, item4, item5, null, ProjectProduct);

            return View(getAll);
            //return View(projectStore);
        }

        public IActionResult Cart(int Productid)
        {
            var item = _context.ProjectOrders.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();

            if (item == null)
            {
                ProjectOrder projectOrder = new ProjectOrder();
                projectOrder.Userid = HttpContext.Session.GetInt32("UserId");
                projectOrder.Datee = DateTime.Now;
                projectOrder.Total = 0;
                _context.Add(projectOrder);
                _context.SaveChangesAsync();

                ProjectProductOrder projectProductOrder = new ProjectProductOrder();
                projectProductOrder.Orderid = projectOrder.Orderid;
                projectProductOrder.Proid = (decimal)Productid;
                _context.Add(projectProductOrder);
                _context.SaveChangesAsync();
            }
            else
            {
                ProjectProductOrder projectProductOrder = new ProjectProductOrder();
                projectProductOrder.Orderid = item.Orderid;
                projectProductOrder.Proid = (decimal)Productid;
                _context.Add(projectProductOrder);
                _context.SaveChangesAsync();

            }

            var it = _context.ProjectOrders.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            int it2 = 0;
            if (it != null)
            {
                it2 = _context.ProjectProductOrders.Where(x => x.Orderid == it.Orderid).ToList().Count;
            }
            else
            {
                
                HttpContext.Session.SetInt32("cartItemsCount", it2);
            }



            HttpContext.Session.SetInt32("cartItemsCount", it2);
            ViewBag.cartItemsCount = HttpContext.Session.GetInt32("cartItemsCount");
            return RedirectToAction("shopDetails", new { id = Productid });


        }

       

        public IActionResult cartDetails()
        {
            ViewBag.cartItemsCount = HttpContext.Session.GetInt32("cartItemsCount");
            var item2 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            var item1 = _context.ProjectHomes.ToList();
            var item3 = _context.ProjectCategories.ToList();
            var item4 = _context.ProjectProducts.ToList();
            var item5 = _context.ProjectStores.ToList();
            

            var it = _context.ProjectOrders.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            if (it != null)
            {
                var it2 = _context.ProjectProductOrders.Where(x => x.Orderid == it.Orderid).ToList();

                ViewBag.orderList = it2;
            }
            else
                ViewBag.orderList = null;

           var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectHome>, FirstProjectTahalf.Models.ProjectUser,
                 IEnumerable<FirstProjectTahalf.Models.ProjectCategory>,
                 IEnumerable<FirstProjectTahalf.Models.ProjectProduct>,
                 IEnumerable<FirstProjectTahalf.Models.ProjectStore>, FirstProjectTahalf.Models.ProjectStore,
                 FirstProjectTahalf.Models.ProjectProduct>
                 (item1, item2, item3, item4, item5, null, null);
            return View(getAll);
        }

        public async Task<IActionResult> deletProFormCart(int ProductOrderid)
        {
            var projectProductOrder = await _context.ProjectProductOrders.FindAsync((decimal)ProductOrderid);
            _context.ProjectProductOrders.Remove(projectProductOrder);
            await _context.SaveChangesAsync();

            var it = _context.ProjectOrders.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            int it2 = 0;
            if (it != null)
            {
                it2 = _context.ProjectProductOrders.Where(x => x.Orderid == it.Orderid).ToList().Count;
                HttpContext.Session.SetInt32("cartItemsCount", it2);
            }
            else
            {

                HttpContext.Session.SetInt32("cartItemsCount", it2);
            }

            return RedirectToAction("cartDetails");
        }


        public IActionResult pushOrder()
        {
            var item2 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            var item1 = _context.ProjectHomes.ToList();
            var item3 = _context.ProjectCategories.ToList();
            var item4 = _context.ProjectProducts.ToList();
            var item5 = _context.ProjectStores.ToList();
            var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectHome>, FirstProjectTahalf.Models.ProjectUser,
                 IEnumerable<FirstProjectTahalf.Models.ProjectCategory>,
                 IEnumerable<FirstProjectTahalf.Models.ProjectProduct>,
                 IEnumerable<FirstProjectTahalf.Models.ProjectStore>, FirstProjectTahalf.Models.ProjectStore,
                 FirstProjectTahalf.Models.ProjectProduct>
                 (item1, item2, item3, item4, item5, null, null);

            var it = _context.ProjectOrders.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            var it2 = _context.ProjectProductOrders.Where(x => x.Orderid == it.Orderid).ToList();

            ViewBag.orderList = it2;

            return View(getAll);
        }

        [HttpPost]
        public async Task<IActionResult> pushOrder(string Country, string Address, string City, string Phone, string notes, string CardNumber)
        {
            var itc = _context.ProjectCreditcards.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();

            if (itc == null)
            {
                ProjectCreditcard projectCreditcard = new ProjectCreditcard();
                int card = Int32.Parse(CardNumber);
                projectCreditcard.Cardnumber = card;
                projectCreditcard.Balance = 1000;
                projectCreditcard.Userid = (decimal)HttpContext.Session.GetInt32("UserId");
                _context.Add(projectCreditcard);
                await _context.SaveChangesAsync();
            }

            //ProjectPayment projectPayment = new ProjectPayment();
            //projectPayment.Status = 1;
            var it = _context.ProjectOrders.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            //projectPayment.Orderid = it.Orderid;
            //add dateTime 
            //projectPayment.Datee = DateTime.Now;
            //add total
            //projectPayment.Total = (decimal)cost;

           // _context.Add(projectPayment);
            //await _context.SaveChangesAsync();

            var pro = _context.ProjectProductOrders.Where(x => x.Orderid == it.Orderid).ToList();
            float cost = 0;
            foreach (var item in pro)
            {
                var prooo = _context.ProjectProducts.Where(x => x.Productid == item.Proid).SingleOrDefault();
                cost += (float)prooo.Prosale;
            }


            ProjectPayment projectPayment = new ProjectPayment();
            projectPayment.Status = 1;
            projectPayment.Orderid = it.Orderid;
            //add dateTime 
            projectPayment.Datee = DateTime.Now;
            //add total
            projectPayment.Total = (decimal)cost;
            _context.Add(projectPayment);
            await _context.SaveChangesAsync();

            var user = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();

            string fullName = user.Fname + user.Lname;
            MimeMessage obj = new MimeMessage();
            MailboxAddress emailfrom = new MailboxAddress("OnlineStore", "teeeeeestemail@gmail.com");
            MailboxAddress emailto = new MailboxAddress(fullName, user.Email);

            obj.From.Add(emailfrom);
            obj.To.Add(emailto);

            BodyBuilder bb = new BodyBuilder();
            DateTime dt = DateTime.Now;

            bb.HtmlBody = "<html>" + 
                
                "<h1>Thank You for Picking Us</h1>" + 
                
                "<h2>Ur order:</h2>" +
                "<h3>Country: " + Country + "</h3>" +
                "<h3>Address: " + Address + "</h3>" +
                "<h3>City: " + City + "</h3>" +
                "<h3>Phone: " + Phone + "</h3>" +
                "<h3>notes: " + notes + "</h3>" +
                "<h3>Total: $" + cost + "</h3>" +
                "<h3>Date: " + DateTime.Now + "</h3>"

                + "</html>";
            obj.Body = bb.ToMessageBody();

            SmtpClient emailClinet = new SmtpClient();
            emailClinet.Connect("smtp.gmail.com", 465, true);
            emailClinet.Authenticate("teeeeeestemail@gmail.com", "zvvugvfrinavklfj");
            emailClinet.Send(obj);

            emailClinet.Disconnect(true);
            emailClinet.Dispose();


            //bool flag = true;

            var it2 = _context.ProjectProductOrders.Where(x => x.Orderid == it.Orderid).ToList();
            foreach (var item in it2)
            {
                
               
                    //var order = await _context.ProjectOrders.FindAsync((item.Orderid));
                    _context.ProjectProductOrders.Remove(item);
                    await _context.SaveChangesAsync();
                    //flag = false;
               

            }

            var userCard = _context.ProjectCreditcards.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            userCard.Balance -= (decimal)cost;
            _context.Update(userCard);
            await _context.SaveChangesAsync();

            //Proudct proudct = new Proudct();
            //_context.Add(proudct);
            //await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public IActionResult search(string search)
        {
            var item2 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            var item1 = _context.ProjectHomes.ToList();
            var item3 = _context.ProjectCategories.ToList();
            var item4 = _context.ProjectProducts.ToList();
            var item5 = _context.ProjectStores.ToList();
            var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectHome>, FirstProjectTahalf.Models.ProjectUser,
                IEnumerable<FirstProjectTahalf.Models.ProjectCategory>,
                IEnumerable<FirstProjectTahalf.Models.ProjectProduct>,
                IEnumerable<FirstProjectTahalf.Models.ProjectStore>, FirstProjectTahalf.Models.ProjectStore,
                FirstProjectTahalf.Models.ProjectProduct>
                (item1, item2, item3, item4, item5, null, null);

            var item6 = _context.ProjectProducts.FromSqlRaw($"select * from project_product WHERE productname LIKE '%{search}%'");

            ViewBag.products = item6;

            return View(getAll);
        }


        public IActionResult contact()
        {
            var it = _context.ProjectOrders.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            int it2 = 0;
            if (it != null)
            {
                it2 = _context.ProjectProductOrders.Where(x => x.Orderid == it.Orderid).ToList().Count;
                HttpContext.Session.SetInt32("cartItemsCount", it2);
            }
            else
            {

                HttpContext.Session.SetInt32("cartItemsCount", it2);
            }

            ViewBag.cartItemsCount = HttpContext.Session.GetInt32("cartItemsCount");
            var item2 = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            var item1 = _context.ProjectHomes.ToList();
            var item3 = _context.ProjectCategories.ToList();
            var item4 = _context.ProjectProducts.ToList();
            var item5 = _context.ProjectStores.ToList();
            var getAll = Tuple.Create<IEnumerable<FirstProjectTahalf.Models.ProjectHome>, FirstProjectTahalf.Models.ProjectUser,
                IEnumerable<FirstProjectTahalf.Models.ProjectCategory>,
                IEnumerable<FirstProjectTahalf.Models.ProjectProduct>,
                IEnumerable<FirstProjectTahalf.Models.ProjectStore>, FirstProjectTahalf.Models.ProjectStore,
                FirstProjectTahalf.Models.ProjectProduct>
                (item1, item2, item3, item4, item5, null, null);

            var user = _context.ProjectUsers.Where(x => x.Roleid == 1).ToList(); 
            var test = _context.ProjectTestimonials.ToList();

            ViewBag.user = user.ToList();
            ViewBag.test = test.ToList();

            return View(getAll);
        }

        public async Task<IActionResult> feedback(string mes)
        {

            var user = _context.ProjectUsers.Where(x => x.Userid == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            string fullName = user.Fname + user.Lname;


            ProjectTestimonial projectTestimonial = new ProjectTestimonial();
            projectTestimonial.Productid = 26;
            projectTestimonial.Message = mes;
            projectTestimonial.Status = 0;
            projectTestimonial.Publishdate = DateTime.Now;
            projectTestimonial.Userid = user.Userid;
            projectTestimonial.Rating = "null";


            _context.ProjectTestimonials.Add(projectTestimonial);
            await _context.SaveChangesAsync();

            MimeMessage obj = new MimeMessage();
            MailboxAddress emailfrom = new MailboxAddress("OnlineStore", "teeeeeestemail@gmail.com");
            MailboxAddress emailto = new MailboxAddress(fullName, user.Email);

            obj.From.Add(emailfrom);
            obj.To.Add(emailto);

            BodyBuilder bb = new BodyBuilder();
            DateTime dt = DateTime.Now;

            bb.HtmlBody = "<html>" +


                "<h1 style="+"color: green; "+" >Thanks for your testimony</h1>" +

                

                "</html>";
            obj.Body = bb.ToMessageBody();

            SmtpClient emailClinet = new SmtpClient();
            emailClinet.Connect("smtp.gmail.com", 465, true);
            emailClinet.Authenticate("teeeeeestemail@gmail.com", "zvvugvfrinavklfj");
            emailClinet.Send(obj);

            emailClinet.Disconnect(true);
            emailClinet.Dispose();




            return RedirectToAction("Index");
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
            if (item==null)
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
            item.Roleid = 1;
            item.Homeid = null;

            //_context.ProjectUsers.FromSqlRaw($"UPDATE project_user SET password = '{password}', Fname = '{Fname}', Lname = '{Lname}', email='{email}', image_path='{ImgPath}' WHERE userid ={item.Userid}");
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context.Update(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
