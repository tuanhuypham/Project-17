using shoper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace shoper.Controllers
{
    public class HomeController : Controller
    {
        private ShoperEntitiesdb db = new ShoperEntitiesdb();

        public ActionResult Index()
        {
            return View();
        }
    
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Success");
            }
            return View("Login", customer);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}