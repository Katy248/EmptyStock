using EmptyStock.Domain.Models.Identity;
using EmptyStock.Domain.Models.Stock;
using EmptyStock.Mvc.Data;
using EmptyStock.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmptyStock.Mvc.Controllers;

[Authorize(Roles = "admin,stockWorker,director")]
public class ProductActionsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<StockUser> _userManager;

    public ProductActionsController(
        ApplicationDbContext context,
        UserManager<StockUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<ProductAction> actions = (await _context.Receipts
            .Include(a => a.Creator)
            .Include(a => a.Product)
            .ToArrayAsync())
            .AsEnumerable<ProductAction>()
            .Concat(await _context.Shipments
            .Include(a => a.Creator)
            .Include(a => a.Product)
            .Include(s => s.Request)
            .ToArrayAsync());
        return View(actions);
    }
    [Authorize]
    public async Task<IActionResult> CreateBill(int id)
    {
        var action = await _context.ProductActions
            .Include(a => a.Product)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (action == null)
        {
            return RedirectToAction("Index");
        }
        if (action is Receipt)
        {
            action = await _context.Receipts
            .Include(a => a.Product)
            .FirstOrDefaultAsync(a => a.Id == id);
        }
        else if (action is Shipment)
        {
            action = await _context.Shipments
            .Include(a => a.Product)
            .Include(a => a.Request)
            .FirstOrDefaultAsync(a => a.Id == id);
        }
        var model = new CreateBillViewModel 
        { 
            ProductAction = action,
            User = await _context.Users.FindAsync(int.Parse(_userManager.GetUserId(User))),
        };
        return View(model);
    }

}
