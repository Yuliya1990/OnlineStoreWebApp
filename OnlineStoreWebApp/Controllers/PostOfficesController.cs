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

        public async Task<IActionResult> goBack()
        {
            return RedirectToAction("Index", "Posts");
        }
        public async Task<IActionResult> Index(int? cityId, int? postId)
        {
            //GET ALL POSTOFFICES
            if (cityId==null && postId==null)
            {
                var dbOnlineStoreContext = _context.PostOffices.Include(c => c.Post).Include(c => c.City);
                return View(await dbOnlineStoreContext.ToListAsync());
            }
             
            //FROM POSTS
            if (postId != null)
            {
                var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
                ViewBag.PostId = postId;
                ViewBag.PostName = post.Name;
                var PostOffByPosts = _context.PostOffices.Where(p => p.PostId == postId).Include(p => p.Post).Include(c => c.City);
                return View(await PostOffByPosts.ToListAsync());
            }
            //FROM CITIES
            else if (cityId != null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(p => p.Id == cityId);
                ViewBag.CityId = cityId;
                ViewBag.CityName= city.Name;
                var PostOffByCity = _context.PostOffices.Where(p => p.CityId == cityId).Include(p => p.Post).Include(c => c.City);
                return View(await PostOffByCity.ToListAsync());
            }

            return View();
        }


        // GET: PostOffices/Create
        public IActionResult Create(int? cityId, int? postId)
        {
            ViewBag.VCityid = cityId;
            ViewBag.VPostid = postId;
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name");
            return View();
        }

        // POST: PostOffices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create(int? postId, int? cityId, bool? isPost, [Bind("Id,PostId,CityId")] PostOffice postOffice)
          {
           if (ModelState.IsValid)
              {
               _context.Add(postOffice);
                await _context.SaveChangesAsync();
               
               if (isPost == null)
                   return RedirectToAction(nameof(Index));
               else if ((bool)isPost)
                   return RedirectToAction("Index", new { postId = postId });

               return RedirectToAction("Index", new { cityId = cityId });
              }
              ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", postOffice.CityId);
              ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", postOffice.PostId);
              return View(postOffice);
          }

        // GET: PostOffices/Edit/5
        public async Task<IActionResult> Edit(int? id, int? postId)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.VPostid = postId;

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
        public async Task<IActionResult> Edit(int id, int? postId, [Bind("Id,PostId,CityId")] PostOffice postOffice)
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
                if (postId == null)
                    return RedirectToAction(nameof(Index));
                else if (postId != null)
                    return RedirectToAction("Index", new { postId });
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", postOffice.CityId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", postOffice.PostId);
            return View(postOffice);
        }

        // GET: PostOffices/Delete/5
        public async Task<IActionResult> Delete(int? id, int? postId)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.VPostid = postId;

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
        public async Task<IActionResult> DeleteConfirmed(int id, int? postId)
        {
            var postOffice = await _context.PostOffices.FindAsync(id);
            _context.PostOffices.Remove(postOffice);
            await _context.SaveChangesAsync();
            if (postId == null)
                return RedirectToAction(nameof(Index));
            
                return RedirectToAction("Index", new { postId });
        }

        private bool PostOfficeExists(int id)
        {
            return _context.PostOffices.Any(e => e.Id == id);
        }
    }
}
