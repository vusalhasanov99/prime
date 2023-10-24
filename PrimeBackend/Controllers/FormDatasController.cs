using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrimeBackend.Models;

namespace PrimeBackend.Controllers
{
    public class FormDatasController : Controller
    {
        private readonly AppDbContext _context;

        public FormDatasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: FormDatas
        public async Task<IActionResult> Index()
        {
              return _context.FormDatas != null ? 
                          View(await _context.FormDatas.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.FormDatas'  is null.");
        }

        // GET: FormDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FormDatas == null)
            {
                return NotFound();
            }

            var formData = await _context.FormDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formData == null)
            {
                return NotFound();
            }

            return View(formData);
        }

        // GET: FormDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FormDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Occupation,PhoneNumber,CountryCode")] FormData formData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formData);
        }

        // GET: FormDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FormDatas == null)
            {
                return NotFound();
            }

            var formData = await _context.FormDatas.FindAsync(id);
            if (formData == null)
            {
                return NotFound();
            }
            return View(formData);
        }

        // POST: FormDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Occupation,PhoneNumber")] FormData formData)
        {
            if (id != formData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormDataExists(formData.Id))
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
            return View(formData);
        }

        // GET: FormDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FormDatas == null)
            {
                return NotFound();
            }

            var formData = await _context.FormDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formData == null)
            {
                return NotFound();
            }

            return View(formData);
        }

        // POST: FormDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FormDatas == null)
            {
                return Problem("Entity set 'AppDbContext.FormDatas'  is null.");
            }
            var formData = await _context.FormDatas.FindAsync(id);
            if (formData != null)
            {
                _context.FormDatas.Remove(formData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormDataExists(int id)
        {
          return (_context.FormDatas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
