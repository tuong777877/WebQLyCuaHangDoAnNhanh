using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeAnCNPMNC.Models;

namespace DeAnCNPMNC.Controllers
{
    public class ShopingCartController : Controller
    {
        // GET: ShopingCart
        CNPMFastFoodEntities database = new CNPMFastFoodEntities();
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                return View("Empty_Cart");
            }
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(string id)
        {
            var _fo = database.Foods.SingleOrDefault(s => s.IDFood == id);
            if (_fo != null)
            {
                GetCart().Add_Food_Cart(_fo);
            }
            return RedirectToAction("ShowCart", "ShopingCart");
        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id_food = (form["idFood"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            cart.Update_quantity(id_food, _quantity);
            return RedirectToAction("ShowCart", "ShopingCart");
        }
        public ActionResult RemoveCart(string id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShopingCart");
        }
        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                total_quantity_item = cart.Total_quantity();
            }
            ViewBag.QuantityCart = total_quantity_item;
            return PartialView("BagCart");
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Bill _bill = new Bill();
                Customer cus = new Customer();
                _bill.DateOrder = DateTime.Now;
                _bill.TotalBill = (double)cart.Total_money();
                if (Convert.ToString(Session["AccountCustomer"]) != "")
                {
                    string tendangnhap = Convert.ToString(Session["AccountCustomer"]);
                    _bill.AccCustomer = database.Customers.Where(s => s.AccountCustomer == tendangnhap).FirstOrDefault().AccountCustomer;
                    _bill.AddressCus = database.Customers.Where(s => s.AccountCustomer == tendangnhap).FirstOrDefault().AddressCustomer;
                }
                else
                {
                    return RedirectToAction("Index", "LoginCus");
                }
                database.Bills.Add(_bill);
                foreach (var item in cart.Items)
                {
                    DetailBill _bill_detail = new DetailBill();
                    _bill_detail.BillName = _bill.IDBill;
                    _bill_detail.NameFood = item._food.IDFood;
                    _bill_detail.UnitPrice = (double)item._food.PriceFood;
                    _bill_detail.Amount = item._quantity;
                    database.DetailBills.Add(_bill_detail);
                    foreach (var p in database.Foods.Where(s => s.IDFood == _bill_detail.NameFood))
                    {
                        var update_quan_food = p.QuantityFood - item._quantity;
                        p.QuantityFood = update_quan_food;
                    }
                }
                database.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Access", "ShopingCart");
            }
            catch
            {
                return Content("Erorr checkout. Please check infomation of Customer...Thanks.");
            }
        }
        public ActionResult CheckOut_Access()
        {
            return View();
        }
    }
}