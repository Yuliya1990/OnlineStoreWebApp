using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStoreWebApp.Models;
using OnlineStoreWebApp.Models.ViewModels;
using System.Diagnostics;

namespace OnlineStoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbOnlineStoreContext _context;

        public HomeController(ILogger<HomeController> logger, DbOnlineStoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            HomeViewModels HomeVM = new HomeViewModels
            {
                Categories = _context.Categories
            };
            if (categoryId!=null)
            {
                HomeVM.Products = _context.Products.Where(p => p.CategoryId==categoryId).Include(p =>p.Category);
            }
            else
            {
                HomeVM.Products = _context.Products.Include(p => p.Category);
            }

            return View(HomeVM);

        }

        public IActionResult Buy(int productId)
        {
            
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                OrderLine orderLine = new OrderLine { ProductId=product.Id, Quantity=1, Product=product};
                return View(orderLine);
            }
            return NotFound();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}