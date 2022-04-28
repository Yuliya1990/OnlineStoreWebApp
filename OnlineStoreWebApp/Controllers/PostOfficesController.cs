using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStoreWebApp;

namespace OnlineStoreWebApp.Controllers
{
    public class PostOfficesController : Controller
    {
        private readonly DbOnlineStoreContext _context;

        public PostOfficesController(DbOnlineStoreContext context)
        {
            _context = context;
        }

        // GET: PostOffices
        public async Task<IActionResult> Index(int? id, string? name, bool isPost)
        {
            //BACK
            if (id == null || name == null)
            {
                //TO POSTS LIST
                if (isPost)
                    return RedirectToAction("Index", "Posts");

                //TO CITIES LIST
                else if (!isPost && id != null)
                {
                    var city = await _context.Cities
                                .Include(c => c.Country)
                                .FirstOrDefaultAsync(m => m.Id == id);
                    var country = city.Country;

                    return RedirectToAction("Index", "Cities", new { id = country.Id, name = country.Name });
                }
            }

            ViewBag.IsPost = isPost;

            if (isPost)
            {
                ViewBag.PostId = id;
                ViewBag.PostName = name;
                var PostOffByPosts = _context.PostOffices.Where(p => p.PostId == id).Include(p => p.Post);
                return View(await PostOffByPosts.ToListAsync());
            }
            else
            {
                ViewBag.CityId = id;
                ViewBag.CityName = name;
                var PostOffByCity = _context.PostOffices.Where(p => p.CityId == id).Include(p => p.City);
                return View(await PostOffByCity.ToListAsync());
            }

        }

        // GET: PostOffices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postOffice = await _context.PostOffices
                .Include(p => p.City)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postOffice == null)
            {
                return NotFound();
            }

            return View(postOffice);
        }

        // GET: PostOffices/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name");
            return View();
        }

        // POST: PostOffices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostId,CityId")] PostOffice postOffice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postOffice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", postOffice.CityId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", postOffice.PostId);
            return View(postOffice);
        }

        // GET: PostOffices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postOffice = await _context.PostOffices.FindAsync(id);
            if (postOffice == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", postOffice.CityId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", postOffice.PostId);
            return View(postOffice);
        }

        // POST: PostOffices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostId,CityId")] PostOffice postOffice)
        {
            if (id != postOffice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postOffice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostOfficeExists(postOffice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", postOffice.CityId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", postOffice.PostId);
            return View(postOffice);
        }

        // GET: PostOffices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postOffice = await _context.PostOffices
                .Include(p => p.City)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postOffice == null)
            {
                return NotFound();
            }

            return View(postOffice);
        }

        // POST: PostOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postOffice = await _context.PostOffices.FindAsync(id);
            _context.PostOffices.Remove(postOffice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostOfficeExists(int id)
        {
            return _context.PostOffices.Any(e => e.Id == id);
        }
    }
}
