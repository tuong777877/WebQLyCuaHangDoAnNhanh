using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    [Authorize(Roles = "AD")]
    public class CategoryCustomerController : Controller
    {
        // GET: Admin/CategoryCus
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        [Authorize(Roles = "AD")]
        public ActionResult Index(string _name)
        {
            if (_name == null)
            {
                return View(database.CategoryCustomers.ToList());
            }
            else
            {
                return View(database.CategoryCustomers.Where(s => s.IDCateCus.Contains(_name)).ToList());
            }
        }
        [Authorize(Roles = "AD")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryCustomer catecus)
        {
            if(ModelState.IsValid)
            {
                var isICateCuslAlreadyExists = database.CategoryCustomers.Any(x => x.IDCateCus == catecus.IDCateCus);
                if (isICateCuslAlreadyExists)
                {
                    ModelState.AddModelError(string.Empty, "ID này đã tồn tại");
                    return View(catecus);
                }
                database.CategoryCustomers.Add(catecus);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catecus);
        }
        [Authorize(Roles = "AD")]
        public ActionResult Details(string id)
        {
            return View(database.CategoryCustomers.Where(s => s.IDCateCus == id).FirstOrDefault());
        }
        [Authorize(Roles = "AD")]
        public ActionResult Edit(string id)
        {
            return View(database.CategoryCustomers.Where(s => s.IDCateCus == id).FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryCustomer catecus)
        {
            if(ModelState.IsValid)
            {
                database.Entry(catecus).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(catecus);
        }
        //[Authorize(Roles = "AD")]
        //public ActionResult Delete(string id)
        //{
        //    return View(database.CategoryCustomers.Where(s => s.IDCateCus == id).FirstOrDefault());
        //}
        //[HttpPost]
        //public ActionResult Delete(string id, CategoryCustomer catecus)
        //{
        //    try
        //    {
        //        catecus = database.CategoryCustomers.Where(s => s.IDCateCus == id).FirstOrDefault();
        //        database.CategoryCustomers.Remove(catecus);
        //        database.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return Content("this data is using in other table, Error delete !");
        //    }
        //}
    }
}