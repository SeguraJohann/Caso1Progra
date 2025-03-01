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
    public class BoletoesController : Controller
    {
        private readonly CasoContext _context;

        public BoletoesController(CasoContext context)
        {
            _context = context;
        }

        // GET: Boletoes
        public async Task<IActionResult> Index()
        {
            var casoContext = _context.Boletos.Include(b => b.Rutas).Include(b => b.Usuarios);
            return View(await casoContext.ToListAsync());
        }

        // GET: Boletoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boletos
                .Include(b => b.Rutas)
                .Include(b => b.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        // GET: Boletoes/Create
        public IActionResult Create()
        {
            ViewData["ID_ruta"] = new SelectList(_context.Rutas, "Id", "Codigo");
            ViewData["ID_usuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario");
            return View();
        }

        // POST: Boletoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ID_usuario,ID_ruta,FechaCompra,Estado")] Boleto boleto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boleto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_ruta"] = new SelectList(_context.Rutas, "Id", "Codigo", boleto.ID_ruta);
            ViewData["ID_usuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", boleto.ID_usuario);
            return View(boleto);
        }

        // GET: Boletoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boletos.FindAsync(id);
            if (boleto == null)
            {
                return NotFound();
            }
            ViewData["ID_ruta"] = new SelectList(_context.Rutas, "Id", "Codigo", boleto.ID_ruta);
            ViewData["ID_usuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", boleto.ID_usuario);
            return View(boleto);
        }

        // POST: Boletoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ID_usuario,ID_ruta,FechaCompra,Estado")] Boleto boleto)
        {
            if (id != boleto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boleto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoletoExists(boleto.Id))
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
            ViewData["ID_ruta"] = new SelectList(_context.Rutas, "Id", "Codigo", boleto.ID_ruta);
            ViewData["ID_usuario"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", boleto.ID_usuario);
            return View(boleto);
        }

        // GET: Boletoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boletos
                .Include(b => b.Rutas)
                .Include(b => b.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        // POST: Boletoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boleto = await _context.Boletos.FindAsync(id);
            if (boleto != null)
            {
                _context.Boletos.Remove(boleto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoletoExists(int id)
        {
            return _context.Boletos.Any(e => e.Id == id);
        }
    }
}
