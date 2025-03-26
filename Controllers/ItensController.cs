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
    public class ItensController : Controller
    {
        private readonly Contexto _context;

        public ItensController(Contexto context)
        {
            _context = context;
        }

        // GET: Itens
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Itens.Include(i => i.material);
            return View(await contexto.ToListAsync());
        }

        // GET: Itens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itens = await _context.Itens
                .Include(i => i.material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itens == null)
            {
                return NotFound();
            }

            return View(itens);
        }

        // GET: Itens/Create
        public IActionResult Create()
        {
            ViewData["CodigoMaterial"] = new SelectList(_context.Materiais, "Id", "Nome");
            return View();
        }

        // POST: Itens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoMaterial")] Itens itens)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itens);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoMaterial"] = new SelectList(_context.Materiais, "Id", "Nome", itens.CodigoMaterial);
            return View(itens);
        }

        // GET: Itens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itens = await _context.Itens.FindAsync(id);
            if (itens == null)
            {
                return NotFound();
            }
            ViewData["CodigoMaterial"] = new SelectList(_context.Materiais, "Id", "Nome", itens.CodigoMaterial);
            return View(itens);
        }

        // POST: Itens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoMaterial")] Itens itens)
        {
            if (id != itens.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itens);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItensExists(itens.Id))
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
            ViewData["CodigoMaterial"] = new SelectList(_context.Materiais, "Id", "Nome", itens.CodigoMaterial);
            return View(itens);
        }

        // GET: Itens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itens = await _context.Itens
                .Include(i => i.material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itens == null)
            {
                return NotFound();
            }

            return View(itens);
        }

        // POST: Itens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itens = await _context.Itens.FindAsync(id);
            if (itens != null)
            {
                _context.Itens.Remove(itens);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItensExists(int id)
        {
            return _context.Itens.Any(e => e.Id == id);
        }
    }
}
