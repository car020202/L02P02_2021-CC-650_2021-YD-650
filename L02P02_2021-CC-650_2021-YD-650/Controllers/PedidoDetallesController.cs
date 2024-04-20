using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using L02P02_2021_CC_650_2021_YD_650.Models;

namespace L02P02_2021_CC_650_2021_YD_650.Controllers
{
    public class PedidoDetallesController : Controller
    {
        private readonly LibreriaDbContext _context;

        public PedidoDetallesController(LibreriaDbContext context)
        {
            _context = context;
        }

        // GET: PedidoDetalles
        public async Task<IActionResult> Index()
        {
            var libreriaDbContext = _context.PedidoDetalles.Include(p => p.IdLibroNavigation).Include(p => p.IdPedidoNavigation);
            return View(await libreriaDbContext.ToListAsync());
        }

        // GET: PedidoDetalles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoDetalle = await _context.PedidoDetalles
                .Include(p => p.IdLibroNavigation)
                .Include(p => p.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoDetalle == null)
            {
                return NotFound();
            }

            return View(pedidoDetalle);
        }

        // GET: PedidoDetalles/Create
        public IActionResult Create()
        {
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Id");
            ViewData["IdPedido"] = new SelectList(_context.PedidoEncabezados, "Id", "Id");
            return View();
        }

        // POST: PedidoDetalles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPedido,IdLibro,CreatedAt")] PedidoDetalle pedidoDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Id", pedidoDetalle.IdLibro);
            ViewData["IdPedido"] = new SelectList(_context.PedidoEncabezados, "Id", "Id", pedidoDetalle.IdPedido);
            return View(pedidoDetalle);
        }

        // GET: PedidoDetalles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoDetalle = await _context.PedidoDetalles.FindAsync(id);
            if (pedidoDetalle == null)
            {
                return NotFound();
            }
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Id", pedidoDetalle.IdLibro);
            ViewData["IdPedido"] = new SelectList(_context.PedidoEncabezados, "Id", "Id", pedidoDetalle.IdPedido);
            return View(pedidoDetalle);
        }

        // POST: PedidoDetalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPedido,IdLibro,CreatedAt")] PedidoDetalle pedidoDetalle)
        {
            if (id != pedidoDetalle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoDetalleExists(pedidoDetalle.Id))
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
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Id", pedidoDetalle.IdLibro);
            ViewData["IdPedido"] = new SelectList(_context.PedidoEncabezados, "Id", "Id", pedidoDetalle.IdPedido);
            return View(pedidoDetalle);
        }

        // GET: PedidoDetalles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoDetalle = await _context.PedidoDetalles
                .Include(p => p.IdLibroNavigation)
                .Include(p => p.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoDetalle == null)
            {
                return NotFound();
            }

            return View(pedidoDetalle);
        }

        // POST: PedidoDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoDetalle = await _context.PedidoDetalles.FindAsync(id);
            if (pedidoDetalle != null)
            {
                _context.PedidoDetalles.Remove(pedidoDetalle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoDetalleExists(int id)
        {
            return _context.PedidoDetalles.Any(e => e.Id == id);
        }
    }
}
