using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BIBLIOTECA_APOSTILA.Data;
using BIBLIOTECA_APOSTILA.Models;

namespace BIBLIOTECA_APOSTILA.Controllers
{
    public class ApostilasController : Controller
    {
        private readonly Contexto _context;

        public ApostilasController(Contexto context)
        {
            _context = context;
        }

        // GET: Apostilas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Apostilas.Include(a => a.Autor);
            return View(await contexto.ToListAsync());
        }

        // GET: Apostilas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apostilas = await _context.Apostilas
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apostilas == null)
            {
                return NotFound();
            }

            return View(apostilas);
        }

        // GET: Apostilas/Create
        public IActionResult Create()
        {
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome");
            return View();
        }

        // POST: Apostilas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CodigoAutor,DataPublicacao")] Apostilas apostilas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apostilas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome", apostilas.CodigoAutor);
            return View(apostilas);
        }

        // GET: Apostilas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apostilas = await _context.Apostilas.FindAsync(id);
            if (apostilas == null)
            {
                return NotFound();
            }
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome", apostilas.CodigoAutor);
            return View(apostilas);
        }

        // POST: Apostilas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CodigoAutor,DataPublicacao")] Apostilas apostilas)
        {
            if (id != apostilas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apostilas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApostilasExists(apostilas.Id))
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
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome", apostilas.CodigoAutor);
            return View(apostilas);
        }

        // GET: Apostilas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apostilas = await _context.Apostilas
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apostilas == null)
            {
                return NotFound();
            }

            return View(apostilas);
        }

        // POST: Apostilas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apostilas = await _context.Apostilas.FindAsync(id);
            if (apostilas != null)
            {
                _context.Apostilas.Remove(apostilas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApostilasExists(int id)
        {
            return _context.Apostilas.Any(e => e.Id == id);
        }
    }
}
