using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using shoper.Models;

namespace shoper.Controllers
{
    public class HomeController : Controller
    {
        private readonly Model1 db;

        public HomeController()
        {
            db = new Model1();
        }
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);  
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
       
        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        
        public ActionResult Best_Seller()
        {
            var bestSellers = db.Products.Where(p => p.BestSeller == true).ToList();
            return View(bestSellers);
        }
       
        public ActionResult Top_saler()
        {
            var hotProducts = db.Products.Where(p => p.HotProduct == true).ToList();
            return View(hotProducts);
        }
    }
}