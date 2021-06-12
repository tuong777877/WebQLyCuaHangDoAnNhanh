using DeAnCNPMNC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeAnCNPMNC.Controllers
{
    public class CategoryFoodMenuController : Controller
    {
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        // GET: CategoryFoodMenu
        public ActionResult Index()
        {
            return View(database.CategoryFoods.ToList());
        }
        public PartialViewResult CategoryFoodPartial()
        {
            var listcate = database.CategoryFoods.ToList();
            return PartialView(listcate);
        }
    }
}