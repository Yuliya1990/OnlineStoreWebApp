using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace OnlineStoreWebApp.Controllers
{
    public class ShopCartController : Controller
    {
        private ShopCart _shopCart;
        private readonly DbOnlineStoreContext _context;

        public ShopCartController(DbOnlineStoreContext context, ShopCart shopCart)
        {
            _context = context;
            _shopCart = shopCart ;
        }

        public ViewResult Index()
        {

            var orderlines = _shopCart.GetCartOrderlines();

            _shopCart.OrderLines = orderlines;

            
            return View(_shopCart);
        }
        public IActionResult AddProduct([Bind("Id, ProductId, Quantity")] OrderLine orderLine)
        {
            var product = _context.Products.FirstOrDefault(p =>p.Id==orderLine.ProductId);
            orderLine.Product = product;
            _shopCart.AddToCard(orderLine);
            return RedirectToAction("Index");
        }
    }
}
