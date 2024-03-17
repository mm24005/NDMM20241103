using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NDMM20241103.Models;

namespace NDMM20241103.Controllers
{
    public class FacturaVentasController : Controller
    {
        private readonly NDMM20241103Contex _context;

        public FacturaVentasController(NDMM20241103Contex context)
        {
            _context = context;
        }

        // GET: FacturaVentas
        public async Task<IActionResult> Index()
        {
            return View(await _context.FacturaVentas.ToListAsync());
        }

        // GET: FacturaVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaVenta = await _context.FacturaVentas
                .Include(s => s.DetFacturaVenta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturaVenta == null)
            {
                return NotFound();
            }

            return View(facturaVenta);
        }

        // GET: FacturaVentas/Create
        public IActionResult Create()
        {
            var facturaVenta = new FacturaVenta();
            facturaVenta.FechaVenta = DateTime.Now;
            facturaVenta.TotalVenta = 0;
            facturaVenta.DetFacturaVenta = new List<DetFacturaVenta>();
            facturaVenta.DetFacturaVenta.Add(new DetFacturaVenta
            {
                Cantidad = 1
            });
            ViewBag.Accion = "Create";
            return View(facturaVenta);
        }

        // POST: FacturaVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaVenta,Correlativo,Cliente,TotalVenta,DetFacturaVenta")] FacturaVenta facturaVenta)
        {
            facturaVenta.TotalVenta = facturaVenta.DetFacturaVenta.Sum(s => s.Cantidad * s.PrecioUnitario);
            _context.Add(facturaVenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            // return View(facturaVenta);
        }
        [HttpPost]
        public ActionResult AgregarDetalles([Bind("Id,FechaVenta,Correlativo,Cliente,TotalVenta,DetFacturaVenta")] FacturaVenta facturaVenta, string accion)
        {
            facturaVenta.DetFacturaVenta.Add(new DetFacturaVenta { Cantidad = 1 });
            ViewBag.Accion = accion;
            return View(accion, facturaVenta);
        }
        public ActionResult EliminarDetalles([Bind("Id,FechaVenta,Correlativo,Cliente,TotalVenta,DetFacturaVenta")] FacturaVenta facturaVenta,
           int index, string accion)
        {
            var det = facturaVenta.DetFacturaVenta[index];
            if (accion == "Edit" && det.Id > 0)
            {
                det.Id = det.Id * -1;
            }
            else
            {
                facturaVenta.DetFacturaVenta.RemoveAt(index);
            }

            ViewBag.Accion = accion;
            return View(accion, facturaVenta);
        }
        // GET: FacturaVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaVenta = await _context.FacturaVentas.FindAsync(id);
            if (facturaVenta == null)
            {
                return NotFound();
            }
            return View(facturaVenta);
        }

        // POST: FacturaVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaVenta,Correlativo,Cliente,TotalVenta")] FacturaVenta facturaVenta)
        {
            if (id != facturaVenta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturaVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaVentaExists(facturaVenta.Id))
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
            return View(facturaVenta);
        }

        // GET: FacturaVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaVenta = await _context.FacturaVentas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturaVenta == null)
            {
                return NotFound();
            }

            return View(facturaVenta);
        }

        // POST: FacturaVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturaVenta = await _context.FacturaVentas.FindAsync(id);
            if (facturaVenta != null)
            {
                _context.FacturaVentas.Remove(facturaVenta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaVentaExists(int id)
        {
            return _context.FacturaVentas.Any(e => e.Id == id);
        }
    }
}
