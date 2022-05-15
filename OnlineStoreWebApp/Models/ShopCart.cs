using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStoreWebApp.Models;
using OnlineStoreWebApp.Models.ViewModels;
using System.Diagnostics;
namespace OnlineStoreWebApp.Models
{
    public class ShopCart
    {
        private readonly DbOnlineStoreContext _onlineStoreContext;
        public string ShopCartId { get; set; }
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public decimal TotalAmount 
        { get 
            {
                decimal totalAmount = 0;
                foreach (var item in OrderLines)
                {
                    totalAmount += item.Quantity * item.Product.Price;
                }
                return totalAmount;
            }
        }

        public ShopCart(DbOnlineStoreContext context)
        {
            _onlineStoreContext = context;
        }

        //чи існує сессія? Чи існує корзина, в яку вже додали товари?
        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<DbOnlineStoreContext>();

            //якщо існує, то матимемо якийсь кардІД, а якщо ні - створюємо новий
            string shopCartId;
            if (session.GetString("CartId") == null)
            {
                 shopCartId = Guid.NewGuid().ToString();
            }
            else
            {
                shopCartId = session.GetString("CartId");
            }
            session.SetString("CartId", shopCartId);

            //створення корзини
            return new ShopCart(context) { ShopCartId = shopCartId};
        }
    /*    public decimal TotalAmount()
        {
            decimal totalAmount = 0;
            foreach (var item in OrderLines)
            {
                totalAmount = item.Quantity * item.Product.Price;
            }
            return totalAmount;
        }*/
        public void AddToCard(OrderLine orderline)
        {
            orderline.ShopCartId = ShopCartId;
            OrderLines.Add(orderline);
            _onlineStoreContext.Add(orderline);
            _onlineStoreContext.SaveChanges();
        }

        public List<OrderLine> GetCartOrderlines() 
        {
            var orderlines = _onlineStoreContext.OrderLines.Where(ol => ol.ShopCartId == ShopCartId).Include(ol => ol.Product);
            return orderlines.ToList();
        }
    }
}
