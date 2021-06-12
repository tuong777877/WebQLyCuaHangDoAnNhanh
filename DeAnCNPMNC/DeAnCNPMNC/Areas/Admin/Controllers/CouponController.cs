using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    [Authorize(Roles = "AD, NV")]
    public class CouponController : Controller
    {
        // GET: Admin/Coupon
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        [Authorize(Roles = "AD")]
        public ActionResult Index(string _idcoupon)
        {
            if (_idcoupon == null)
            {
                return View(database.Coupons.ToList());
            }
            else
            {
                return View(database.Coupons.Where(s => s.IDCoupon.Contains(_idcoupon)).ToList());
            }
        }
        [Authorize(Roles = "AD, NV")]
        public ActionResult Create()
        {
            Coupon cou = new Coupon();
            return View(cou);
        }
        [HttpPost]
        public ActionResult Create(Coupon cou)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var isIDAlreadyExists = database.Coupons.Any(x => x.IDCoupon == cou.IDCoupon);
                    if (isIDAlreadyExists)
                    {
                        ModelState.AddModelError(string.Empty, "ID này đã tồn tại");
                        return View(cou);
                    }
                    if (cou.DateStart < DateTime.Now || cou.DateStart > cou.DateEnd)
                    {
                        ModelState.AddModelError(string.Empty, "Ngày bắt đầu hoặc ngày kết thúc không hợp lệ");
                        return View(cou);
                    }
                    if (cou.UploadImage != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(cou.UploadImage.FileName);
                        string extent = Path.GetExtension(cou.UploadImage.FileName);
                        filename = filename + extent;
                        cou.ImageCoupon = "~/Content/images/" + filename;
                        cou.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
                    }
                    database.Coupons.Add(cou);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cou);
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "AD, NV")]
        public ActionResult Details(string idcou)
        {
            return View(database.Coupons.Where(s => s.IDCoupon == idcou).FirstOrDefault());
        }
        [Authorize(Roles = "AD, NV")]
        public ActionResult Edit(string idcou)
        {
            return View(database.Coupons.Where(s => s.IDCoupon == idcou).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(Coupon cou)
        {
            if (cou.UploadImage != null)
            {
                string filename = Path.GetFileNameWithoutExtension(cou.UploadImage.FileName);
                string extent = Path.GetExtension(cou.UploadImage.FileName);
                filename = filename + extent;
                cou.ImageCoupon = "~/Content/images/" + filename;
                cou.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), filename));
            }
            database.Entry(cou).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "AD, NV")]
        public ActionResult Delete(string idcou)
        {
            return View(database.Coupons.Where(s => s.IDCoupon == idcou).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(string idcou, Coupon cou)
        {
            cou = database.Coupons.Where(s => s.IDCoupon == idcou).FirstOrDefault();
            database.Coupons.Remove(cou);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}