using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeAnCNPMNC.Models
{
    public class CartItem
    {
        public Food _food { get; set; }
        public int _quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add_Food_Cart(Food _foo, int _quan = 1)
        {
            var item = Items.FirstOrDefault(s => s._food.IDFood == _foo.IDFood);
            if (item == null)
                items.Add(new CartItem
                {
                    _food = _foo,
                    _quantity = _quan
                });
            else
                item._quantity += _quan;
        }
        public int Total_quantity()
        {
            return items.Sum(s => s._quantity);
        }
        public decimal Total_money()
        {
            var total = items.Sum(s => s._quantity * s._food.PriceFood);
            return (decimal)total;
        }
        public void Update_quantity(string id, int _new_quan)
        {
            var item = items.Find(s => s._food.IDFood == id);
            if (item != null)
            {
                if (items.Find(s => s._food.QuantityFood > _new_quan) != null)
                    item._quantity = _new_quan;
                else item._quantity = 1;
            }

        }
        public void Remove_CartItem(string id)
        {
            items.RemoveAll(s => s._food.IDFood == id);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}