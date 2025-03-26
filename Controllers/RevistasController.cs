using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BIBLIOTECA_APOSTILA.Data;
using BIBLIOTECA_APOSTILA.Models;

namespace BIBLIOTECA_APOSTILA.Controllers
{
    public class RevistasController : Controller
    {
        private readonly Contexto _context;

        public RevistasController(Contexto context)
        {
            _context = context;
        }

        // GET: Revistas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Revistas.Include(r => r.Autor);
            return View(await contexto.ToListAsync());
        }

        // GET: Revistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revistas = await _context.Revistas
                .Include(r => r.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revistas == null)
            {
                return NotFound();
            }

            return View(revistas);
        }

        // GET: Revistas/Create
        public IActionResult Create()
        {
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome");
            return View();
        }

        // POST: Revistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Paginas,Editora,Edicao,Id,Nome,CodigoAutor,DataPublicacao")] Revistas revistas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revistas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome", revistas.CodigoAutor);
            return View(revistas);
        }

        // GET: Revistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revistas = await _context.Revistas.FindAsync(id);
            if (revistas == null)
            {
                return NotFound();
            }
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome", revistas.CodigoAutor);
            return View(revistas);
        }

        // POST: Revistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Paginas,Editora,Edicao,Id,Nome,CodigoAutor,DataPublicacao")] Revistas revistas)
        {
            if (id != revistas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revistas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevistasExists(revistas.Id))
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
            ViewData["CodigoAutor"] = new SelectList(_context.Autores, "Id", "Nome", revistas.CodigoAutor);
            return View(revistas);
        }

        // GET: Revistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revistas = await _context.Revistas
                .Include(r => r.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revistas == null)
            {
                return NotFound();
            }

            return View(revistas);
        }

        // POST: Revistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var revistas = await _context.Revistas.FindAsync(id);
            if (revistas != null)
            {
                _context.Revistas.Remove(revistas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevistasExists(int id)
        {
            return _context.Revistas.Any(e => e.Id == id);
        }
    }
}
