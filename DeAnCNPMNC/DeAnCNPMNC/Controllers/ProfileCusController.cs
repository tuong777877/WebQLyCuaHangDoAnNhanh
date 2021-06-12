using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Controllers
{
    public class ProfileCusController : Controller
    {
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        // GET: ProfileCus
        public ActionResult Index(Customer cus)
        {
            Session["AccountCustomer"] = cus.AccountCustomer;
            Session["NameCustomer"] = cus.NameCustomer;
            Session["PassCustomer"] = cus.PassCustomer;
            Session["ImageCustomer"] = cus.ImageCustomer;
            Session["DateOfBirthCustomer"] = cus.DateOfBirthCustomer;
            Session["PhoneCustomer"] = cus.PhoneCustomer;
            Session["EmailCustomer"] = cus.EmailCustomer;
            Session["AddressCustomer"] = cus.AddressCustomer;
            Session["SexCustomer"] = cus.SexCustomer;
            Session["NameCateCustomer"] = cus.NameCateCustomer;
            Session["Point"] = cus.Point;
            return RedirectToAction("Index", "Home");  
        }
        //public ActionResult Profile(string id, Customer cus)
        //{
        //    if (Session["AccountCustomer"] == cus.AccountCustomer)
        //    {
        //        return View(database.Customers.Where(s => s.AccountCustomer == id).FirstOrDefault());
        //    }
        //    else
        //    {
        //        return View("Index");
        //    }
            
        //}
        public ActionResult Details(string id)
        {
            id = (string)Session["AccountCustomer"];
            return View(database.Customers.Where(s => s.AccountCustomer == id).FirstOrDefault());
        }
        public ActionResult Edit(string id)
        {
            id = (string)Session["AccountCustomer"];
            return View(database.Customers.Where(s => s.AccountCustomer == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Customer cus)
        {
            database.Entry(cus).State = System.Data.Entity.EntityState.Modified;
            try
            {
                if (cus.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(cus.UploadImage.FileName);
                    string extent = Path.GetExtension(cus.UploadImage.FileName);
                    filename = filename + extent;
                    cus.ImageCustomer = "~/Content/images/" + filename;
                    cus.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                }
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }
    }
}