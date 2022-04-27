#nullable disable
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
    public class CitiesController : Controller
    {
        private readonly DbOnlineStoreContext _context;

        public CitiesController(DbOnlineStoreContext context)
        {
            _context = context;
        }
       

        // GET: Cities
        public async Task<IActionResult> Index(int? id, string? name)
        {
            //RETURN TO COUNTRIES LIST
            if (id == null || name == null) 
                return RedirectToAction("Index", "Countries");

            ViewBag.CountryId = id;
            ViewBag.CountryName = name;

            var citiesByCountries = _context.Cities.Where(c => c.CountryId == id).Include(c => c.Country);
            return View(await citiesByCountries.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create(int countryId)
        {
            //ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id");
            ViewBag.CountryId = countryId;
            ViewBag.CountryName = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault().Name;
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int countryId, [Bind("Id,Name")] City city)
        {
            city.CountryId = countryId;
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Cities", new { id = countryId, 
                                                               name = _context.Countries.Where(c=>c.Id==countryId).FirstOrDefault().Name});
            }
            //ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", city.CountryId);
            //return View(city);
            return RedirectToAction("Index", "Cities", new { id = countryId, name = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault().Name });
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? cityId, int? countryId)
        {
            if (cityId == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(cityId);
            if (city == null)
            {
                return NotFound();
            }

            ViewBag.VCountryId = countryId;
            ViewBag.CountryName = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault().Name;

            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", city.CountryId);
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CountryId")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                int countryId = _context.Cities.Where(c => c.Id == id).FirstOrDefault().CountryId;
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Cities", new { id = countryId, name = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault().Name });
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", city.CountryId);
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id, int? countryId)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.VCountryId = countryId;
            ViewBag.CountryName = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault().Name;

            var city = await _context.Cities
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int countryId = _context.Cities.Where(c => c.Id == id).FirstOrDefault().CountryId;
            var city = await _context.Cities.FindAsync(id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            
            return RedirectToAction("Index", "Cities", new { id = countryId, name = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault().Name });
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}
