using System;
using System.Collections.Generic;
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

        public ActionResult Index(int? id)
        {
            var products = db.Product.ToList();
            if (id.HasValue)
            {
                var user = db.Customer.Find(id);
                if (user != null)
                {
                    ViewBag.UserName = user.CustomerID;
                }
            }
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
            int maxId = db.Customer.Any() ? db.Customer.Max(c => c.CustomerID) : 0;
            customer.CustomerID = maxId + 1;

            db.Customer.Add(customer);
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
            var users = db.Customer.Where(u => u.Username == username && u.Password == password).ToList();
            if (users.Any())
            {
                foreach (var user in users)
                {
                    if (user != null)
                    {
                        // Đăng nhập thành công
                        Session["Username"] = user.Username;
                        Session["Password"] = user.Password;
                        Session["Sex"] = user.Sex;
                        Session["CustomerID"] = user.CustomerID;  //Lưu ID của người dùng
                        var redirectUrl = Session["RedirectUrl"];
                        if (redirectUrl != null)
                        {
                            Session["RedirectUrl"] = null;
                            return Redirect(redirectUrl.ToString());
                        }
                        // Chuyển hướng đến trang Index với ID người dùng
                        return RedirectToAction("Index", new { id = user.CustomerID });
                    }
                }
            }
            else
            {
                // Đăng nhập thất bại, hiển thị lỗi
                ViewBag.Message = "Username or password is incorrect or does not exist.";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["Password"] = null;
            Session["Sex"] = null;
            Session["CustomerID"] = null; // Xóa ID của người dùng
            return RedirectToAction("Index");
        }

        public ActionResult Hot_Products()
        {
            var bestSellers = db.Product.Where(p => p.BestSeller == true).ToList();
            return View(bestSellers);
        }

        public ActionResult Best_Seller_Products()
        {
            var hotproducts = db.Product.Where(p => p.HotProduct == true).ToList();
            return View(hotproducts);
        }
   
        public ActionResult ShoppingCart()
        {
           
            return View();
        }

        public ActionResult Product_Detail(int id)
        {
            ViewBag.ProductID = id;
            var productDT = db.ProductDetailPage.ToList();
            return View(productDT); // Truyền model tới view
        }

        public ActionResult Dress()
        {
            var dress = db.Product.Where(p => p.Dress == false).ToList();
            if (dress == null)
            {
                dress = new List<Product>();
            }
            return View(dress);
        }

        public ActionResult Sexy_Nightgown()
        {
            var Sexy_Nightgown = db.Product.Where(p => p.Sexy_Nightgown == true).ToList();
            if (Sexy_Nightgown == null)
            {
                Sexy_Nightgown = new List<Product>();
            }
            return View(Sexy_Nightgown);
        }
    }
}
