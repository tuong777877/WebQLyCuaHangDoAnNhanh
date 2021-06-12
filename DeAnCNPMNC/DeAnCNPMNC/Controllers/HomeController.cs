using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Controllers
{
    public class HomeController : Controller
    {
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        public ActionResult Index(string _name)
        {
            if (_name == null)
            {
                return View(database.Foods.ToList());
            }
            else
            {
                return View(database.Foods.Where(s => s.IDFood.Contains(_name)).ToList());
            }
        }
        public ActionResult Details(string id)
        {
            return View(database.Foods.Where(s => s.IDFood == id).FirstOrDefault());
        }
    }
}