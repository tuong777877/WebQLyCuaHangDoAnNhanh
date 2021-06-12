using DeAnCNPMNC.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace DeAnCNPMNC.Areas.Admin.Controllers
{
    public class BillController : Controller
    {
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        // GET: Admin/Bill
        public static List<Bill> SelectAllArticle()
        {
            var rtn = new List<Bill>();
            using (var context = new CNPMFastFoodEntities())
            {
                foreach (var item in context.Bills)
                {
                    rtn.Add(new Bill
                    {
                        IDBill = item.IDBill,
                        DateOrder = item.DateOrder,
                        AccCustomer = item.AccCustomer,
                        CouponName = item.CouponName,
                        TotalBill = item.TotalBill,
                        AddressCus = item.AddressCus,
                        Status = item.Status
                    }); ;
                }
            }
            return rtn;
        }
        public ActionResult DSBill()
        {
            var lsBill = SelectAllArticle().OrderByDescending(x => x.IDBill).ToList();
            return View(lsBill);
        }
        public ActionResult EditBill(int Id)
        {
            return View(database.Bills.Where(s => s.IDBill == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditBill(int Id, Bill bill)
        {
            database.Entry(bill).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            bool status = Convert.ToBoolean(database.Bills.Where(s => s.IDBill == Id).FirstOrDefault().Status);
            string taikhoan = database.Bills.Where(s => s.IDBill == Id).FirstOrDefault().AccCustomer;
            string gmailkhach = database.Customers.Where(s => s.AccountCustomer == taikhoan).FirstOrDefault().EmailCustomer;
            if (status == true)
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add(gmailkhach);
                    mail.From = new MailAddress("nguyendaoduchuy2808@gmail.com");
                    mail.Subject = "Agent Fast Food";
                    mail.Body = "Đơn hàng của bạn đã được xác nhận!";
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential("nguyendaoduchuy2808@gmail.com", "01887220286Huy");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }    
                }                        
            }    
            return RedirectToAction("DSBill");
        }

        public ActionResult DetailsNV(int Id)
        {
            var Details = database.DetailBills.Where(s => s.ID == Id).FirstOrDefault();
            return View(Details);
        }
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //[HttpPost]
        public ActionResult DetailsAD(int Id)
        {
            var Details = database.DetailBills.Where(s => s.ID == Id).FirstOrDefault();
            return View(Details);
        }
        public ActionResult Index(string searchString, DateTime? date, DateTime? date1, int? Id)
        {
            var list = SelectAllArticle().OrderByDescending(x => x.IDBill).ToList();
            if (searchString == null && date == null)
            {
                list = SelectAllArticle().OrderByDescending(x => x.IDBill).ToList();
            }
            else if (searchString == null || date != null || date1 != null)
            {
                //var list = SelectAllArticle().Where(s => s.DateOrder.Value.Equals(date.Value));
                using (var context = new CNPMFastFoodEntities())
                {
                    list = context.Bills.SqlQuery("SELECT * FROM Bill where DateOrder BETWEEN '" + date.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) + "' and '" + date1.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) + "'").ToList();
                }
                Session["date"] = date.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                Session["date1"] = date1.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                return View(list);
            }
            return View(list);
        }
        //[HttpPost]
        //public ActionResult Tranfer()
        //{
        //    using (var context = new CNPMFastFoodEntities())
        //    {
        //        context.Database.ExecuteSqlCommand("INSERT INTO Revenue (RevenueDateStart, RevenueDateEnd, RevenueMoney) SELECT '" + Session["date"] + "', '" + Session["date1"] + "', SUM(IntoMoney) FROM Bill WHERE DateOrder BETWEEN '" + Session["date"] + "' and '" + Session["date1"] + "'");
        //        context.SaveChanges();
        //    }
        //    return View();
        //}
    }
}