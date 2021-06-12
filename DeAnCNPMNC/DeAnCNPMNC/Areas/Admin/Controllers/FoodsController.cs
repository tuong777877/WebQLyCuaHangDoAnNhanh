using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    [Authorize(Roles = "AD, NV,NVO")]
    public class FoodsController : Controller
    {

        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        // GET: Admin/Foods
        public static List<Food> SelectAllArticle()
        {
            var rtn = new List<Food>();
            using (var context = new CNPMFastFoodEntities())
            {
                foreach (var item in context.Foods)
                {
                    rtn.Add(new Food
                    {
                        IDFood = item.IDFood,
                        NameFood = item.NameFood,
                        ImageFood = item.ImageFood,
                        PriceFood = item.PriceFood,
                        StatusFood = item.StatusFood,
                        CateFood = item.CategoryFood.NameCategoryFood,
                        QuantityFood=item.QuantityFood,
                        DesFood=item.DesFood,
                        Trending=item.Trending,
                    });
                }
            }
            return rtn;
        }
        [Authorize(Roles = "AD, NV, NVO")]
        public ActionResult Index(string food)
        {
            if (food == null)
            {
                //var foodlist = database.Foods.OrderByDescending(x => x.NameFood);
                return View(SelectAllArticle().ToList());
            }
            else
            {
                var foodlist = database.Foods.OrderByDescending(x => x.IDFood).Where(s=>s.CategoryFood.NameCategoryFood==food);
                return View(foodlist);
            }
        }
        [Authorize(Roles = "AD")]
        public ActionResult Create()
        {
            List<CategoryFood> list = database.CategoryFoods.ToList();
            ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
            Food food = new Food();
            return View(food);
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult Create(Food food,FormCollection collection)
        {
            List<CategoryFood> list = database.CategoryFoods.ToList();
            try
            {
                if(ModelState.IsValid)
                {
                    var isIdFoodlAlreadyExists = database.Foods.Any(x => x.IDFood == food.IDFood);
                    if(isIdFoodlAlreadyExists)
                    {
                        ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
                        ModelState.AddModelError(string.Empty, "ID này đã tồn tại");
                        return View(food);
                    }
                    var isNameFoodlAlreadyExists = database.Foods.Any(x => x.NameFood == food.NameFood);
                    if (isNameFoodlAlreadyExists)
                    {
                        ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
                        ModelState.AddModelError(string.Empty, "Tên món ăn này đã tồn tại");
                        return View(food);
                    }
                    if (food.UploadImage != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(food.UploadImage.FileName);
                        string extent = Path.GetExtension(food.UploadImage.FileName);
                        filename = filename + extent;
                        food.ImageFood = "~/Content/images/" + filename;
                        food.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    }
                    if (food.QuantityFood <= 1)
                    {
                        food.StatusFood = false;
                    }
                    else if (food.QuantityFood > 1)
                    {
                        food.StatusFood = true;
                    }                        
                    ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
                    database.Foods.Add(food);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(food);

            }
            catch
            {
                return View();
            }
        }
        public ActionResult SelectCate()
        {
            CategoryFood se_Cate = new CategoryFood();
            se_Cate.ListCate = database.CategoryFoods.ToList<CategoryFood>();
            return PartialView(se_Cate);
        }
        [Authorize(Roles = "AD, NV, NVO")]
        public ActionResult Details(string id)
        {
            return View(database.Foods.Where(s => s.IDFood == id).FirstOrDefault());
        }
        [Authorize(Roles = "AD, NV")]
        public ActionResult Edit(string id)
        {
            List<CategoryFood> list = database.CategoryFoods.ToList();
            ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
            return View(database.Foods.Where(s => s.IDFood == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Food food, string id)
        {
            List<CategoryFood> list = database.CategoryFoods.ToList();
            database.Entry(food).State = System.Data.Entity.EntityState.Modified;
            if (food.QuantityFood <= 1)
            {
                food.StatusFood = false;
            }
            if (food.UploadImage != null)
            {
                string filename = Path.GetFileNameWithoutExtension(food.UploadImage.FileName);
                string extent = Path.GetExtension(food.UploadImage.FileName);
                filename = filename + extent;
                food.ImageFood = "~/Content/images/" + filename;
                food.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
            }
            ViewBag.ListCategoryFood = new SelectList(list, "IDCFood", "NameCategoryFood", "");
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        //[Authorize(Roles = "AD")]
        //public ActionResult Delete(string id)
        //{
        //    return View(database.Foods.Where(s => s.IDFood == id).FirstOrDefault());
        //}
        //[HttpPost]
        //public ActionResult Delete(string id,Food food)
        //{
        //    food=database.Foods.Where(s=>s.IDFood==id).FirstOrDefault();
        //    database.Foods.Remove(food);
        //    database.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}