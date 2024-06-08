using System;
using System.Linq;
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

        //HTTP get /Home/Register
        public ActionResult Register()
        {
            return View();
        }

        //HTTP Post /Home/Register
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            Customer customer = new Customer();
            customer.FullName = form["FullName"];
            customer.Sex = form["Sex"];
            customer.Email = form["Email"];
            customer.Phone = form["Phone"];
            customer.Username = form["Username"];
            customer.Password = form["Password"];

            // Tạo CustomerID tự động
            int maxId = db.Customers.Any() ? db.Customers.Max(c => c.CustomerID) : 0;
            customer.CustomerID = maxId + 1;

            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("LogIn");
        }

        //HTTP get /Home/LogIn
        public ActionResult LogIn()
        {
            return View();
        }

        //HTTP Post /Home/LogIn
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var users = db.Customers.Where(u => u.Username == username && u.Password == password).ToList();
            if (users.Any())
            {
                
                foreach (var user in users)
                {
                    if (user != null)
                    {
                
                        Session["Username"] = user.Username;
                        Session["Password"] = user.Password;
                        Session["Sex"] = user.Sex;
                        var redirectUrl = Session["RedirectUrl"];
                        if (redirectUrl != null)
                        {
                            Session["RedirectUrl"] = null;
                            return Redirect(redirectUrl.ToString());
                        }
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                ViewBag.Message = "Username or password is incorrect or does not exist.";
            }
            return View();
        }


        [HttpPost]
        public ActionResult Logout()
        {
            Session["Username"] = null;
            return View("Index");
        }

        public ActionResult Best_Seller()
        {
            var bestSellers = db.Products.Where(p => p.BestSeller == true).ToList();
            return View(bestSellers);
        }

        public ActionResult Top_saler()
        {
            var hotproducts = db.Products.Where(p => p.HotProduct == true).ToList();
            return View(hotproducts);
        }
   
        public ActionResult ShoppingCart()
        {
           
            return View();
        }
        public ActionResult Product_Detail(int id)
        {
            ViewBag.ProductID = id;
            var productDT = db.ProductDetailPages.ToList();
            return View(productDT); // Truyền model tới view
        }
    }
}
