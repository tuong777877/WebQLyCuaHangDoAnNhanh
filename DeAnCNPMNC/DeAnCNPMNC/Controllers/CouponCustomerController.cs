using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;
using PagedList;

namespace DeAnCNPMNC.Controllers
{
    public class CouponCustomerController : Controller
    {
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        // GET: CouponCustomer
        public ActionResult Index(Coupon coup, int? page)
        {
            int pageSize = 6;
            int pageNum = (page ?? 1);
            var lscoupon = database.Coupons.ToList();
            if (coup.Quantity > 1)
            {
                return View(lscoupon.ToPagedList(pageNum, pageSize));
            }
            else
            {
                Session["Amount"] = coup.Quantity;
                return View(lscoupon.ToPagedList(pageNum, pageSize));
            }
        }
    }
}