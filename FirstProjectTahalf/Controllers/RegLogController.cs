using FirstProjectTahalf.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProjectTahalf.Controllers
{
    public class RegLogController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public RegLogController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult SigInSignUp()
        {
            return View();
        }

        public async Task<IActionResult> Register([Bind("Fname,Lname,Email,Password,ImagePath,ImgFileProfile")] ProjectUser projectUser)
        {
            if (ModelState.IsValid)
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
                    _context.Add(projectUser);
                    await _context.SaveChangesAsync();

                    //send Email
                    MimeMessage obj = new MimeMessage();
                    MailboxAddress emailfrom = new MailboxAddress("user", "teeeeeestemail@gmail.com");
                    MailboxAddress emailto = new MailboxAddress("user", projectUser.Email);

                    obj.From.Add(emailfrom);
                    obj.To.Add(emailto);
                    obj.Subject = "Thank you for Regst";
                    BodyBuilder bb = new BodyBuilder();
                    DateTime dt = DateTime.Now;
                    bb.HtmlBody = "<html>" + "<h1>" + dt.ToString() + "</h1>" + "</html>";
                    obj.Body = bb.ToMessageBody();

                    SmtpClient emailClinet = new SmtpClient();
                    emailClinet.Connect("smtp.gmail.com", 465, true);
                    emailClinet.Authenticate("teeeeeestemail@gmail.com", "zvvugvfrinavklfj");
                    emailClinet.Send(obj);

                    emailClinet.Disconnect(true);
                    emailClinet.Dispose();

                    return RedirectToAction(nameof(SigInSignUp));
                }
            }
            ViewData["Homeid"] = new SelectList(_context.ProjectHomes, "Homeid", "Homeid", projectUser.Homeid);
            ViewData["Roleid"] = new SelectList(_context.ProjectRoles, "Roleid", "Roleid", projectUser.Roleid);
            return View(projectUser);
        }


        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var item = _context.ProjectUsers.Where(x => x.Email == email && x.Password == password).SingleOrDefault();

            if (item != null) {

                if (item.Roleid == 1)
                {
                    HttpContext.Session.SetInt32("UserId", (int)item.Userid);
                    return RedirectToAction("Index", "Home", new
                    {
                        id = item.Userid,
                        name = item.Fname + item.Lname
                    });
                }
                else
                {
                    HttpContext.Session.SetInt32("UserId", (int)item.Userid);
                    return RedirectToAction("Index", "Admin", new
                    {
                        id = item.Userid,
                        name = item.Fname + item.Lname
                    });
                }
            }

            return RedirectToAction("SigInSignUp", "RegLog");
        }
    }
}
