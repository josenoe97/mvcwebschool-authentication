using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcWebSchool_Identity.Data;
using MvcWebSchool_Identity.Entities;
using System;

namespace MvcWebIdentity.Controllers;

[Authorize]
public class ProdutosController : Controller
{
    private readonly WebSchoolContext _context;

    public ProdutosController(WebSchoolContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return _context.Produtos != null ?
                    View(await _context.Produtos.ToListAsync()) :
                    Problem("Entity set 'AppDbContext.Produtos' é null.");
    }


    [Authorize(Policy = "TempoCadastroMinimo")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Produtos == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (produto == null)
        {
            return NotFound();
        }
        return View(produto);
    }

    [Authorize(Policy = "TempoCadastroMinimo")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProdutoId,Nome,Preco")] Produto produto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(produto);
    }

    [Authorize(Policy = "TempoCadastroMinimo")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Produtos == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }
        return View(produto);
    }


    // POST: Produtos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,Nome,Preco")] Produto produto)
    {
        if (id != produto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(produto.Id))
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
        return View(produto);
    }


    //[Authorize(Policy = "TesteClaim")]
    [Authorize(Policy = "TempoCadastroMinimo", Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Produtos == null)
        {
            return NotFound();
        }

        var produto = await _context.Produtos
            .FirstOrDefaultAsync(m => m.Id== id);

        if (produto == null)
        {
            return NotFound();
        }

        return View(produto);
    }

    // POST: Produtos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Produtos == null)
        {
            return Problem("Entity set 'AppDbContext.Produtos'  is null.");
        }
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProdutoExists(int id)
    {
        return (_context.Produtos?.Any(e => e.Id== id)).GetValueOrDefault();
    }
}

