using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    [Authorize(Roles = "AD")]
    public class CategoryFoodController : Controller
    {
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        // GET: Admin/CategoryFood
        [Authorize(Roles = "AD")]
        public ActionResult Index(string _name)
        {
            if (_name == null)
            {
                return View(database.CategoryFoods.ToList());
            }
            else
            {
                return View(database.CategoryFoods.Where(s => s.NameCategoryFood.Contains(_name)).ToList());
            }
        }
        [Authorize(Roles = "AD")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CategoryFood catefood)
        {
            if(ModelState.IsValid)
            {
                var isICateFoodlAlreadyExists = database.CategoryFoods.Any(x => x.IDCFood == catefood.IDCFood);
                if(isICateFoodlAlreadyExists)
                {
                    ModelState.AddModelError(string.Empty, "ID này đã tồn tại");
                    return View(catefood);
                }
                database.CategoryFoods.Add(catefood);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catefood);
        }
        [Authorize(Roles = "AD")]
        public ActionResult Details(string id)
        {
            return View(database.CategoryFoods.Where(s => s.IDCFood == id).FirstOrDefault());
        }
        [Authorize(Roles = "AD")]
        public ActionResult Edit(string id)
        {
            return View(database.CategoryFoods.Where(s => s.IDCFood == id).FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryFood catefood)
        {
            if(ModelState.IsValid)
            {
                database.Entry(catefood).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(catefood);
        }
        //[Authorize(Roles = "AD")]
        //public ActionResult Delete(string id)
        //{
        //    return View(database.CategoryFoods.Where(s => s.IDCFood == id).FirstOrDefault());
        //}
        //[HttpPost]
        //public ActionResult Delete(string id,CategoryFood catefood)
        //{
        //    try
        //    {
        //        catefood = database.CategoryFoods.Where(s => s.IDCFood == id).FirstOrDefault();
        //        database.CategoryFoods.Remove(catefood);
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