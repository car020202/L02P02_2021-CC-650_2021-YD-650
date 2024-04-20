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
    public class ComentariosLibroesController : Controller
    {
        private readonly LibreriaDbContext _context;

        public ComentariosLibroesController(LibreriaDbContext context)
        {
            _context = context;
        }

        // GET: ComentariosLibroes
        public async Task<IActionResult> Index()
        {
            var libreriaDbContext = _context.ComentariosLibros.Include(c => c.IdLibroNavigation);
            return View(await libreriaDbContext.ToListAsync());
        }

        // GET: ComentariosLibroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentariosLibro = await _context.ComentariosLibros
                .Include(c => c.IdLibroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentariosLibro == null)
            {
                return NotFound();
            }

            return View(comentariosLibro);
        }

        // GET: ComentariosLibroes/Create
        public IActionResult Create()
        {
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Id");
            return View();
        }

        // POST: ComentariosLibroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdLibro,Comentarios,Usuario,CreatedAt")] ComentariosLibro comentariosLibro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comentariosLibro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Id", comentariosLibro.IdLibro);
            return View(comentariosLibro);
        }

        // GET: ComentariosLibroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentariosLibro = await _context.ComentariosLibros.FindAsync(id);
            if (comentariosLibro == null)
            {
                return NotFound();
            }
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Id", comentariosLibro.IdLibro);
            return View(comentariosLibro);
        }

        // POST: ComentariosLibroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdLibro,Comentarios,Usuario,CreatedAt")] ComentariosLibro comentariosLibro)
        {
            if (id != comentariosLibro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentariosLibro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentariosLibroExists(comentariosLibro.Id))
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
            ViewData["IdLibro"] = new SelectList(_context.Libros, "Id", "Id", comentariosLibro.IdLibro);
            return View(comentariosLibro);
        }

        // GET: ComentariosLibroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentariosLibro = await _context.ComentariosLibros
                .Include(c => c.IdLibroNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comentariosLibro == null)
            {
                return NotFound();
            }

            return View(comentariosLibro);
        }

        // POST: ComentariosLibroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comentariosLibro = await _context.ComentariosLibros.FindAsync(id);
            if (comentariosLibro != null)
            {
                _context.ComentariosLibros.Remove(comentariosLibro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentariosLibroExists(int id)
        {
            return _context.ComentariosLibros.Any(e => e.Id == id);
        }
    }
}
