using DeAnCNPMNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeAnCNPMNC.Controllers
{
    public class LoginCusController : Controller
    {
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        // GET: LoginCus
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginCus(Customer cus)
        {
            var check = database.Customers.Where(s => s.AccountCustomer == cus.AccountCustomer && s.PassCustomer == cus.PassCustomer).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai tên tài khoản hoặc mật khẩu";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["AccountCustomer"] = cus.AccountCustomer;
                Session["NameCustomer"] = cus.NameCustomer;
                Session["ImageCustomer"] = cus.ImageCustomer;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Customer cus)
        {
            var checkID = database.Customers.Where(s => s.AccountCustomer == cus.AccountCustomer || s.EmailCustomer == cus.EmailCustomer).FirstOrDefault();
            if (checkID != null)
            {
                ViewBag.ErrorLogin = "tài khoản hoặc email đã tồn tại!";
                return View();
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                database.Customers.Add(cus);
                database.SaveChanges();
                return RedirectToAction("Index", "LoginCus");
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginCus");
        }
    }
}