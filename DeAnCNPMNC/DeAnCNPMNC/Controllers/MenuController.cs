using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;
using PagedList;

namespace DeAnCNPMNC.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
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
                        QuantityFood = item.QuantityFood,
                        DesFood = item.DesFood,
                        Trending = item.Trending
                    });
                }
            }
            return rtn;
        }
        public static List<CategoryFood> SelectAllArticle1()
        {
            var rtn = new List<CategoryFood>();
            using (var context = new CNPMFastFoodEntities())
            {
                foreach (var item in context.CategoryFoods)
                {
                    rtn.Add(new CategoryFood
                    {
                        IDCFood = item.IDCFood,
                        NameCategoryFood = item.NameCategoryFood
                    });
                }
            }
            return rtn;
        }
        public ActionResult Index(Food food, string category, int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            if (category == null)
            {
                var lsFood = SelectAllArticle().ToList();
                if (food.QuantityFood > 1)
                {
                    return View(lsFood.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    Session["Amount"] = food.QuantityFood;
                    return View(lsFood.ToPagedList(pageNum, pageSize));
                }
            }
            else
            {
                var lsFood = database.Foods.OrderByDescending(x => x.NameFood).Where(x => x.CateFood == category);
                if (food.QuantityFood > 1)
                {
                    return View(lsFood.ToPagedList(pageNum, pageSize));
                }
                else
                {
                    Session["Amount"] = food.QuantityFood;
                    return View(lsFood.ToPagedList(pageNum, pageSize));
                }
            }
        }
        public ActionResult Details(string id)
        {
            return View(SelectAllArticle().Where(s => s.IDFood == id).FirstOrDefault());
        }
    }
}