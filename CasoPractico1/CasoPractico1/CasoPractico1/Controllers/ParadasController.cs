using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasoPractico1.Models;

namespace CasoPractico1.Controllers
{
    public class ParadasController : Controller
    {
        private readonly CasoContext _context;

        public ParadasController(CasoContext context)
        {
            _context = context;
        }

        // GET: Paradas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Paradas.ToListAsync());
        }

        // GET: Paradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parada = await _context.Paradas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parada == null)
            {
                return NotFound();
            }

            return View(parada);
        }

        // GET: Paradas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Ubicacion")] Parada parada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parada);
        }

        // GET: Paradas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parada = await _context.Paradas.FindAsync(id);
            if (parada == null)
            {
                return NotFound();
            }
            return View(parada);
        }

        // POST: Paradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Ubicacion")] Parada parada)
        {
            if (id != parada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParadaExists(parada.Id))
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
            return View(parada);
        }

        // GET: Paradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parada = await _context.Paradas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parada == null)
            {
                return NotFound();
            }

            return View(parada);
        }

        // POST: Paradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parada = await _context.Paradas.FindAsync(id);
            if (parada != null)
            {
                _context.Paradas.Remove(parada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParadaExists(int id)
        {
            return _context.Paradas.Any(e => e.Id == id);
        }
    }
}
