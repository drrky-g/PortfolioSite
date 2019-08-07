using PortfolioSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PortfolioSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EmailModel model = new EmailModel();

            return View(model);
        }

        //Email Contact Action [POST]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var from = $"{model.FromEmail}-{model.FromName}<{WebConfigurationManager.AppSettings["emailto"]}>";


                    var email = new MailMessage(from, WebConfigurationManager.AppSettings["emailto"])
                    {

                        Subject = model.Subject,
                        Body = $"You've got a message from {model.FromName}: <br> <hr>{model.Body}",
                        IsBodyHtml = true
                    };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);

                    return View(new EmailModel());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            EmailModel model = new EmailModel();

            return View(model);
        }

        
    }
}