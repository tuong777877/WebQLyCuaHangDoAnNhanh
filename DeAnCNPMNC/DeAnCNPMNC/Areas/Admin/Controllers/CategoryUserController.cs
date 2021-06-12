using DeAnCNPMNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    [Authorize(Roles = "AD")]
    public class CategoryUserController : Controller
    {
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        [Authorize(Roles = "AD")]
        // GET: Admin/CategoryUser
        public ActionResult Index(string _name)
        {
            if (_name == null)
            {
                return View(database.CategoryUsers.ToList());
            }
            else
            {
                return View(database.CategoryUsers.Where(s => s.NameCateUser.Contains(_name)).ToList());
            }
        }
        [Authorize(Roles = "AD")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryUser categoryUser)
        {
            if(ModelState.IsValid)
            {
                var isICateUserlAlreadyExists = database.CategoryUsers.Any(x => x.IDCUser == categoryUser.IDCUser);
                if (isICateUserlAlreadyExists)
                {
                    ModelState.AddModelError(string.Empty, "ID này đã tồn tại");
                    return View(categoryUser);
                }
                database.CategoryUsers.Add(categoryUser);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoryUser);
        }
        [Authorize(Roles = "AD")]
        public ActionResult Details(string id)
        {
            return View(database.CategoryUsers.Where(s=>s.IDCUser==id).FirstOrDefault());
        }
        [Authorize(Roles = "AD")]
        public ActionResult Edit(string id)
        {
            return View(database.CategoryUsers.Where(s => s.IDCUser == id).FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryUser categoryUser)
        {
            if(ModelState.IsValid)
            {
                database.Entry(categoryUser).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(categoryUser);
        }
        //[Authorize(Roles = "AD")]
        //public ActionResult Delete(string id)
        //{
        //    return View(database.CategoryUsers.Where(s => s.IDCUser == id).FirstOrDefault());
        //}
        //[HttpPost]
        //public ActionResult Delete(string id, CategoryUser categoryUser)
        //{
        //    try
        //    {
        //        categoryUser = database.CategoryUsers.Where(s => s.IDCUser == id).FirstOrDefault();
        //        database.CategoryUsers.Remove(categoryUser);
        //        database.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return Content("this data is using in other table, Error delete !");
        //    }
        //}
        //public ActionResult GetList()
        //{
        //    var emlist = database.CategoryUsers.ToList<CategoryUser>();
        //    return Json(new { data = emlist }, JsonRequestBehavior.AllowGet);
        //}
    }
}