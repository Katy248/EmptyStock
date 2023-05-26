using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmptyStock.Domain.Models.Stock;
using EmptyStock.Mvc.Data;
using EmptyStock.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EmptyStock.Mvc.Controllers;

[Authorize(Roles = "admin,manager,director")]
public class RequestsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<StockUser> _userManager;

    public RequestsController(ApplicationDbContext context, UserManager<StockUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Requests
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Requests.Include(r => r.Creator).Include(r => r.Product);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Requests/Create
    public IActionResult Create()
    {
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        return View(new Request { CreatorId = int.Parse(_userManager.GetUserId(User)!) });
    }

    // POST: Requests/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductId,CreatorId,Cost,Count,Id")] Request request)
    {
        if (ModelState.IsValid)
        {
            _context.Add(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", request.ProductId);
        return View(request);
    }

    // GET: Requests/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Requests == null)
        {
            return NotFound();
        }

        var request = await _context.Requests.FindAsync(id);
        if (request == null)
        {
            return NotFound();
        }
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", request.ProductId);
        return View(request);
    }

    // POST: Requests/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProductId,CreatorId,Cost,Count,Id")] Request request)
    {
        if (id != request.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(request);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(request.Id))
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
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", request.ProductId);
        return View(request);
    }

    // GET: Requests/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Requests == null)
        {
            return NotFound();
        }

        var request = await _context.Requests
            .Include(r => r.Creator)
            .Include(r => r.Product)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (request == null)
        {
            return NotFound();
        }

        return View(request);
    }

    // POST: Requests/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Requests == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Requests'  is null.");
        }
        var request = await _context.Requests.FindAsync(id);
        if (request != null)
        {
            _context.Requests.Remove(request);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RequestExists(int id)
    {
      return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
