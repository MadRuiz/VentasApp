using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VentasApp.Models;

namespace VentasApp.Controllers
{
    public class VentasController : Controller
    {
        private readonly ContextBase _context;

        public VentasController(ContextBase context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            ViewBag.tiendaList = (from t in _context.Tiendas select t).ToList();
            return View(await _context.Ventas.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .FirstOrDefaultAsync(m => m.Id_Venta == id);
            if (ventas == null)
            {
                return NotFound();
            }

            ViewBag.productosList = (from p in _context.Productos select p).ToList();
            ViewBag.ventaObject = (from v in _context.Ventas where v.Id_Venta == id select v).FirstOrDefault();
            ViewBag.detList = (from d in _context.DetalleVentas where d.Id_Venta == id select d).ToList();
            return View();
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewBag.tiendaList = (from t in _context.Tiendas select t).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_Venta,Codigo_Factura,Nombre_Cliente,Fecha,Total,Id_Tienda")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventas);
                await _context.SaveChangesAsync();
                return RedirectToAction("CreateDetalleVenta", new { id = ventas.Id_Venta });
            }
            return View(ventas);
        }

        public IActionResult CreateDetalleVenta(int id){

                ViewBag.productosList = (from p in _context.Productos select p).ToList();
                ViewBag.ventaObject = (from v in _context.Ventas where v.Id_Venta == id select v).FirstOrDefault();
                ViewBag.detList = (from d in _context.DetalleVentas where d.Id_Venta == id select d).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDetalleVenta([Bind("Id_DetalleVenta,Subtotal,Cantidad,Id_Venta,Id_Producto")] DetalleVentas detalleVentas)
        {
            if (detalleVentas != null)
            {
                Productos producto = (from p in _context.Productos where p.Id_Producto == detalleVentas.Id_Producto select p).FirstOrDefault();

                double newSubTotal = detalleVentas.Subtotal + ( detalleVentas.Cantidad * producto.Precio);
                Math.Round((newSubTotal * 100d) / 100d);

                detalleVentas.Subtotal = newSubTotal;
                _context.Add(detalleVentas);
                _context.SaveChanges();
                ViewBag.productosList = (from p in _context.Productos select p).ToList();
            
                ViewBag.ventaObject = (from v in _context.Ventas where v.Id_Venta == detalleVentas.Id_Venta select v).FirstOrDefault();
                ViewBag.detList = (from d in _context.DetalleVentas where d.Id_Venta == detalleVentas.Id_Venta select d).ToList();
                
                Ventas newTotalizedVenta  = (from v in _context.Ventas where v.Id_Venta == detalleVentas.Id_Venta select v).FirstOrDefault();
                newTotalizedVenta.Total = newTotalizedVenta.Total + newSubTotal;
                _context.SaveChanges();

                return View("CreateDetalleVenta", new { id = detalleVentas.Id_Venta });
            }
            return View("Index");
        }
        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas.FindAsync(id);
            if (ventas == null)
            {
                return NotFound();
            }
            ViewBag.tiendaList = (from t in _context.Tiendas select t).ToList();
            return View(ventas);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_Venta,Codigo_Factura,Nombre_Cliente,Fecha,Total,Id_Tienda")] Ventas ventas)
        {
            if (id != ventas.Id_Venta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentasExists(ventas.Id_Venta))
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
            return View(ventas);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventas = await _context.Ventas
                .FirstOrDefaultAsync(m => m.Id_Venta == id);
            if (ventas == null)
            {
                return NotFound();
            }

            return View(ventas);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventas = await _context.Ventas.FindAsync(id);
            _context.Ventas.Remove(ventas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentasExists(int id)
        {
            return _context.Ventas.Any(e => e.Id_Venta == id);
        }
    }
}
