﻿using System;
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
    public class AutoresController : Controller
    {
        private readonly Contexto _context;

        public AutoresController(Contexto context)
        {
            _context = context;
        }

        // GET: Autores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autores.ToListAsync());
        }

        // GET: Autores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = await _context.Autores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // GET: Autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataNascimento")] Autores autores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autores);
        }

        // GET: Autores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = await _context.Autores.FindAsync(id);
            if (autores == null)
            {
                return NotFound();
            }
            return View(autores);
        }

        // POST: Autores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNascimento")] Autores autores)
        {
            if (id != autores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoresExists(autores.Id))
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
            return View(autores);
        }

        // GET: Autores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autores = await _context.Autores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autores == null)
            {
                return NotFound();
            }

            return View(autores);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autores = await _context.Autores.FindAsync(id);
            if (autores != null)
            {
                _context.Autores.Remove(autores);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoresExists(int id)
        {
            return _context.Autores.Any(e => e.Id == id);
        }
    }
}
