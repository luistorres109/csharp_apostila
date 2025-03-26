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
    public class EmprestimosController : Controller
    {
        private readonly Contexto _context;

        public EmprestimosController(Contexto context)
        {
            _context = context;
        }

        // GET: Emprestimos
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Emprestimos.Include(e => e.Cliente).Include(e => e.Usuario).Include(e => e.Item);
            return View(await contexto.ToListAsync());
        }

        // GET: Emprestimos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimos = await _context.Emprestimos
                .Include(e => e.Cliente)
                .Include(e => e.Usuario)
                .Include(e => e.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimos == null)
            {
                return NotFound();
            }

            return View(emprestimos);
        }

        // GET: Emprestimos/Create
        public IActionResult Create()
        {
            Configuracoes configuracoes = _context.Configuracoes.Find(1);
            var dias = configuracoes.DiasEmprestimo;

            Emprestimos emprestimos = new Emprestimos();
            emprestimos.data_emprestimo = DateTime.Now;
            emprestimos.data_devolucao = DateTime.Now.AddDays((double)dias);

            ViewData["CodigoCliente"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["CodigoItem"] = new SelectList(_context.Itens.Include(e => e.material), "Id", "material.Nome");
            ViewData["CodigoUsuario"] = new SelectList(_context.Usuarios, "Id", "Login");
            return View(emprestimos);
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Emprestimos emprestimos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emprestimos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCliente"] = new SelectList(_context.Clientes, "Id", "Nome", emprestimos.CodigoCliente);
            ViewData["CodigoItem"] = new SelectList(_context.Itens.Include(e => e.material), "Id", "material.Nome", emprestimos.CodigoItem);
            ViewData["CodigoUsuario"] = new SelectList(_context.Usuarios, "Id", "Login", emprestimos.CodigoUsuario);
            return View(emprestimos);
        }

        // GET: Emprestimos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimos = await _context.Emprestimos.FindAsync(id);
            if (emprestimos == null)
            {
                return NotFound();
            }
            ViewData["CodigoCliente"] = new SelectList(_context.Clientes, "Id", "Nome", emprestimos.CodigoCliente);
            ViewData["CodigoItem"] = new SelectList(_context.Itens, "Id", "Id", emprestimos.CodigoItem);
            ViewData["CodigoUsuario"] = new SelectList(_context.Usuarios, "Id", "Login", emprestimos.CodigoUsuario);
            return View(emprestimos);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoUsuario,CodigoCliente,CodigoItem,Situacao,data_emprestimo,data_devolucao,data_pos_devolvida")] Emprestimos emprestimos)
        {
            if (id != emprestimos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprestimos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmprestimosExists(emprestimos.Id))
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
            ViewData["CodigoCliente"] = new SelectList(_context.Clientes, "Id", "Nome", emprestimos.CodigoCliente);
            ViewData["CodigoItem"] = new SelectList(_context.Itens, "Id", "Id", emprestimos.CodigoItem);
            ViewData["CodigoUsuario"] = new SelectList(_context.Usuarios, "Id", "Login", emprestimos.CodigoUsuario);
            return View(emprestimos);
        }

        // GET: Emprestimos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimos = await _context.Emprestimos
                .Include(e => e.Cliente)
                .Include(e => e.Usuario)
                .Include(e => e.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimos == null)
            {
                return NotFound();
            }

            return View(emprestimos);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimos = await _context.Emprestimos.FindAsync(id);
            if (emprestimos != null)
            {
                _context.Emprestimos.Remove(emprestimos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmprestimosExists(int id)
        {
            return _context.Emprestimos.Any(e => e.Id == id);
        }

        [HttpPost, ActionName("Devolver")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Devolver(int? Id)
        {
            var emprestimo = await _context.Emprestimos.FindAsync(Id);
            try
            {
                if (ModelState.IsValid)
                {
                    emprestimo.Situacao = Emprestimos.SituacaoEmprestimo.Devolvido;
                    emprestimo.data_pos_devolvida = DateTime.Today;
                    await _context.SaveChangesAsync();
                    TempData["MensagemSucesso"] = $"Devolução realizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não foi possivel devolver o item, detalhe de erro: {erro.Message}";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
