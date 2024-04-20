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
    public class PedidoEncabezadoesController : Controller
    {
        private readonly LibreriaDbContext _context;

        public PedidoEncabezadoesController(LibreriaDbContext context)
        {
            _context = context;
        }

        // GET: PedidoEncabezadoes
        public async Task<IActionResult> Index()
        {
            var libreriaDbContext = _context.PedidoEncabezados.Include(p => p.IdClienteNavigation);
            return View(await libreriaDbContext.ToListAsync());
        }

        // GET: PedidoEncabezadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoEncabezado = await _context.PedidoEncabezados
                .Include(p => p.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoEncabezado == null)
            {
                return NotFound();
            }

            return View(pedidoEncabezado);
        }

        // GET: PedidoEncabezadoes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id");
            return View();
        }

        // POST: PedidoEncabezadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCliente,CantidadLibros,Total,estado")] PedidoEncabezado pedidoEncabezado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidoEncabezado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", pedidoEncabezado.IdCliente);
            return View(pedidoEncabezado);
        }

        // GET: PedidoEncabezadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoEncabezado = await _context.PedidoEncabezados.FindAsync(id);
            if (pedidoEncabezado == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", pedidoEncabezado.IdCliente);
            return View(pedidoEncabezado);
        }

        // POST: PedidoEncabezadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCliente,CantidadLibros,Total,estado")] PedidoEncabezado pedidoEncabezado)
        {
            if (id != pedidoEncabezado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidoEncabezado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoEncabezadoExists(pedidoEncabezado.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", pedidoEncabezado.IdCliente);
            return View(pedidoEncabezado);
        }

        // GET: PedidoEncabezadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidoEncabezado = await _context.PedidoEncabezados
                .Include(p => p.IdClienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedidoEncabezado == null)
            {
                return NotFound();
            }

            return View(pedidoEncabezado);
        }

        // POST: PedidoEncabezadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedidoEncabezado = await _context.PedidoEncabezados.FindAsync(id);
            if (pedidoEncabezado != null)
            {
                _context.PedidoEncabezados.Remove(pedidoEncabezado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoEncabezadoExists(int id)
        {
            return _context.PedidoEncabezados.Any(e => e.Id == id);
        }
    }
}
