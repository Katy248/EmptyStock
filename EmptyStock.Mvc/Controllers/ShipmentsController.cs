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
public class ShipmentsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<StockUser> _userManager;

    public ShipmentsController(ApplicationDbContext context, UserManager<StockUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Shipments
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Shipments.Include(s => s.Creator).Include(s => s.Product).Include(s => s.Request);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Shipments/Create
    public IActionResult Create()
    {
        ViewData["RequestId"] = new SelectList(_context.Requests.Where(r => r.Shipment == null), "Id", "Id");
        return View(new Shipment { CreatorId = int.Parse(_userManager.GetUserId(User)!)});
    }

    // POST: Shipments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RequestId,ProductId,CreatorId,Id")] Shipment shipment)
    {
        var request = await _context.Requests.FindAsync(shipment.RequestId)!;
        var productsCount = request.Count;

        IEnumerable<ProductAction> productActions = (_context.Shipments.Include(s => s.Request).Where(s => s.ProductId == request.ProductId) as IEnumerable<ProductAction>).Concat(
            _context.Receipts.Where(r => r.ProductId == request.ProductId));

        var stockProducts = productActions.Sum(pa => pa.ChangeAmount);
        if (productsCount > stockProducts)
        {
            ModelState.AddModelError("", "There are no products to ship");
            return View(shipment);
        }
        if (ModelState.IsValid)
        {
            shipment.CreationDate = DateTime.UtcNow;
            shipment.Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == _context.Requests.Find(shipment.RequestId).ProductId);
            _context.Add(shipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", shipment.ProductId);
        ViewData["RequestId"] = new SelectList(_context.Requests.Where(r => r.Shipment == null), "Id", "Id", shipment.RequestId);
        return View(shipment);
    }

    // GET: Shipments/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Shipments == null)
        {
            return NotFound();
        }

        var shipment = await _context.Shipments
            .Include(s => s.Creator)
            .Include(s => s.Product)
            .Include(s => s.Request)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (shipment == null)
        {
            return NotFound();
        }

        return View(shipment);
    }

    // POST: Shipments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Shipments == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Shipments'  is null.");
        }
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment != null)
        {
            _context.Shipments.Remove(shipment);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ShipmentExists(int id)
    {
      return (_context.Shipments?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
