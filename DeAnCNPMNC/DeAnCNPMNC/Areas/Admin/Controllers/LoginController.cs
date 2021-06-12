using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        CNPMFastFoodEntities database=new CNPMFastFoodEntities();
        
        public ActionResult LoginAccount()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginAccount(Employee _emp)
        {
            var check = database.Employees.Where(s => s.AccountEmp == _emp.AccountEmp && s.PassEmp == _emp.PassEmp).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Sai tên tài khoản hoặc mật khẩu";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["AccountEmployee"] = _emp.AccountEmp;
                Session["NameEmployee"] = _emp.NameEmp;
                UserStatus status = GetUserValidity(_emp);
                bool IsAdmin = false;
                var returnurl = RedirectToAction("Index", "Employee");
                if (status == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (status == UserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Ivalid UserName or Password");
                    return View("LoginAccount");
                }
                FormsAuthentication.SetAuthCookie(_emp.AccountEmp, false);
                Session["IsAdmin"] = IsAdmin;
                if (IsAdmin == true)
                {
                    returnurl = RedirectToAction("Index", "Employee");
                }
                else if (IsAdmin == false)
                {
                    foreach (var item in database.Employees.Where(x => x.AccountEmp == _emp.AccountEmp && x.PassEmp == _emp.PassEmp))
                    {
                        returnurl = RedirectToAction("Index", "Employee", new { accemp = item.AccountEmp });
                    }
                }
                return returnurl;
            }
            
        }
        public ActionResult LogoutAccount()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginAccount");
        }
        public UserStatus GetUserValidity(Employee emp)
        {
            var status = UserStatus.NonAuthenticatedUser;
            foreach (var item in database.Employees.Where(x => x.AccountEmp == emp.AccountEmp && x.PassEmp == emp.PassEmp))
            {
                if (item.CateUser.Trim() == "AD")
                {
                    status = UserStatus.AuthenticatedAdmin;
                }
                else if (item.CateUser.Trim() == "NV")
                {
                    status = UserStatus.AuthenticatedUser;
                }
                else
                    status = UserStatus.NonAuthenticatedUser;
            }
            return status;
        }
    }
}