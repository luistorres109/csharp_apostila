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
    public class ConfiguracoesController : Controller
    {
        private readonly Contexto _context;

        public ConfiguracoesController(Contexto context)
        {
            _context = context;
        }

        // GET: Configuracoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Configuracoes.ToListAsync());
        }

        // GET: Configuracoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracoes = await _context.Configuracoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracoes == null)
            {
                return NotFound();
            }

            return View(configuracoes);
        }

        // GET: Configuracoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configuracoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiasEmprestimo,DiasLancamento,ValorMulta")] Configuracoes configuracoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configuracoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuracoes);
        }

        // GET: Configuracoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracoes = await _context.Configuracoes.FindAsync(id);
            if (configuracoes == null)
            {
                return NotFound();
            }
            return View(configuracoes);
        }

        // POST: Configuracoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DiasEmprestimo,DiasLancamento,ValorMulta")] Configuracoes configuracoes)
        {
            if (id != configuracoes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuracoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfiguracoesExists(configuracoes.Id))
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
            return View(configuracoes);
        }

        // GET: Configuracoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracoes = await _context.Configuracoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracoes == null)
            {
                return NotFound();
            }

            return View(configuracoes);
        }

        // POST: Configuracoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configuracoes = await _context.Configuracoes.FindAsync(id);
            if (configuracoes != null)
            {
                _context.Configuracoes.Remove(configuracoes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfiguracoesExists(int id)
        {
            return _context.Configuracoes.Any(e => e.Id == id);
        }
    }
}
