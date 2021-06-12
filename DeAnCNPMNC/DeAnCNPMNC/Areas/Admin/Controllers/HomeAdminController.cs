using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}