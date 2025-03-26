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
    public class MateriaisController : Controller
    {
        private readonly Contexto _context;

        public MateriaisController(Contexto context)
        {
            _context = context;
        }

        // GET: Materiais
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Materiais.Include(m => m.Autor);
            return View(await contexto.ToListAsync());
        }

        // GET: Materiais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiais = await _context.Materiais
                .Include(m => m.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiais == null)
            {
                return NotFound();
            }

            return View(materiais);
        }

        // GET: Materiais/Create
        public IActionResult Create()
        {
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome");
            return View();
        }

        // POST: Materiais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CodigoAutor,DataPublicacao")] Materiais materiais)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materiais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome", materiais.CodigoAutor);
            return View(materiais);
        }

        // GET: Materiais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiais = await _context.Materiais.FindAsync(id);
            if (materiais == null)
            {
                return NotFound();
            }
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome", materiais.CodigoAutor);
            return View(materiais);
        }

        // POST: Materiais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CodigoAutor,DataPublicacao")] Materiais materiais)
        {
            if (id != materiais.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materiais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaisExists(materiais.Id))
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
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome", materiais.CodigoAutor);
            return View(materiais);
        }

        // GET: Materiais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiais = await _context.Materiais
                .Include(m => m.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materiais == null)
            {
                return NotFound();
            }

            return View(materiais);
        }

        // POST: Materiais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materiais = await _context.Materiais.FindAsync(id);
            if (materiais != null)
            {
                _context.Materiais.Remove(materiais);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaisExists(int id)
        {
            return _context.Materiais.Any(e => e.Id == id);
        }
    }
}
