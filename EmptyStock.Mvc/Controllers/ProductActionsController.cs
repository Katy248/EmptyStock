using EmptyStock.Domain.Models.Stock;
using EmptyStock.Mvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmptyStock.Mvc.Controllers;

public class ProductActionsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductActionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<ProductAction> actions = (await _context.Receipts.Include(a => a.Creator).Include(a => a.Product).ToArrayAsync())
            .AsEnumerable<ProductAction>()
            .Concat(await _context.Shipments.Include(a => a.Creator).Include(a => a.Product).Include(s => s.Request).ToArrayAsync());
        return View(actions);
    }
}
