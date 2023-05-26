using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmptyStock.Domain.Models.Stock;
using EmptyStock.Mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EmptyStock.Domain.Models.Identity;

namespace EmptyStock.Mvc.Controllers;

[Authorize(Roles = "admin,stockWorker,director")]
public class ReceiptsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<StockUser> _userManager;

    public ReceiptsController(ApplicationDbContext context, UserManager<StockUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Receipts
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Receipts.Include(r => r.Creator).Include(r => r.Product);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Receipts/Create
    public IActionResult Create()
    {
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        return View(new Receipt { CreatorId = int.Parse(_userManager.GetUserId(User)!) });
    }

    // POST: Receipts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Count,ChangeAmount,ProductId,CreatorId,Id")] Receipt receipt)
    {
        if (ModelState.IsValid)
        {
            receipt.CreationDate = DateTime.UtcNow;
            _context.Add(receipt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", receipt.ProductId);
        return View(receipt);
    }

    // GET: Receipts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Receipts == null)
        {
            return NotFound();
        }

        var receipt = await _context.Receipts
            .Include(r => r.Creator)
            .Include(r => r.Product)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (receipt == null)
        {
            return NotFound();
        }

        return View(receipt);
    }

    // POST: Receipts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Receipts == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Receipts'  is null.");
        }
        var receipt = await _context.Receipts.FindAsync(id);
        if (receipt != null)
        {
            _context.Receipts.Remove(receipt);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ReceiptExists(int id)
    {
      return (_context.Receipts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
