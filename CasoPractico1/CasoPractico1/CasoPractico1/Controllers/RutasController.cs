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
    public class RutasController : Controller
    {
        private readonly CasoContext _context;

        public RutasController(CasoContext context)
        {
            _context = context;
        }

        // GET: Rutas
        public async Task<IActionResult> Index()
        {
            var casoContext = _context.Rutas.Include(r => r.Horarios).Include(r => r.Paradas).Include(r => r.Usuarios).Include(r => r.Vehiculos);
            return View(await casoContext.ToListAsync());
        }

        // GET: Rutas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Rutas
                .Include(r => r.Horarios)
                .Include(r => r.Paradas)
                .Include(r => r.Usuarios)
                .Include(r => r.Vehiculos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        // GET: Rutas/Create
        public IActionResult Create()
        {
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id");
            ViewData["ParadaId"] = new SelectList(_context.Paradas, "Id", "Nombre");
            ViewData["usuarioRegistroId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario");
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa");
            return View();
        }

        // POST: Rutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Nombre,Descripcion,Estado,VehiculoId,ParadaId,HorarioId")] Ruta ruta)
        {
            if (ModelState.IsValid)
            {
                var vehiculo = await _context.Vehiculos.FindAsync(ruta.VehiculoId);


                if (vehiculo == null)
                {
                    ModelState.AddModelError("VehiculoId", "El vehículo seleccionado no existe.");
                    return View(ruta);
                }

                ruta.FechaRegistro = DateTime.Now;
                ruta.usuarioRegistroId = 1;
                ruta.EspaciosDisponibles = vehiculo.CapacidadPasajeros;

                _context.Add(ruta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", ruta.HorarioId);
            ViewData["ParadaId"] = new SelectList(_context.Paradas, "Id", "Nombre", ruta.ParadaId);
            ViewData["usuarioRegistroId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", ruta.usuarioRegistroId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa", ruta.VehiculoId);
            return View(ruta);
        }

        // GET: Rutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Rutas.FindAsync(id);
            if (ruta == null)
            {
                return NotFound();
            }
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", ruta.HorarioId);
            ViewData["ParadaId"] = new SelectList(_context.Paradas, "Id", "Nombre", ruta.ParadaId);
            ViewData["usuarioRegistroId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", ruta.usuarioRegistroId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa", ruta.VehiculoId);
            return View(ruta);
        }

        // POST: Rutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Nombre,Descripcion,Estado,EspaciosDisponibles,FechaRegistro,usuarioRegistroId,VehiculoId,ParadaId,HorarioId")] Ruta ruta)
        {
            if (id != ruta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ruta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RutaExists(ruta.Id))
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
            ViewData["HorarioId"] = new SelectList(_context.Horarios, "Id", "Id", ruta.HorarioId);
            ViewData["ParadaId"] = new SelectList(_context.Paradas, "Id", "Nombre", ruta.ParadaId);
            ViewData["usuarioRegistroId"] = new SelectList(_context.Usuarios, "Id", "NombreUsuario", ruta.usuarioRegistroId);
            ViewData["VehiculoId"] = new SelectList(_context.Vehiculos, "Id", "Placa", ruta.VehiculoId);
            return View(ruta);
        }

        // GET: Rutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Rutas
                .Include(r => r.Horarios)
                .Include(r => r.Paradas)
                .Include(r => r.Usuarios)
                .Include(r => r.Vehiculos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        // POST: Rutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ruta = await _context.Rutas.FindAsync(id);
            if (ruta != null)
            {
                _context.Rutas.Remove(ruta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RutaExists(int id)
        {
            return _context.Rutas.Any(e => e.Id == id);
        }
    }
}
