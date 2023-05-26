using EmptyStock.Domain.Models.Identity;
using EmptyStock.Mvc.Data;
using EmptyStock.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmptyStock.Mvc.Controllers;

[Authorize(Roles = "admin")]
public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<StockUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public UsersController(ApplicationDbContext context, UserManager<StockUser> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.ToArrayAsync();
        return View(users);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

        var roles = await _userManager.GetRolesAsync(user);
        var model = new EditUserViewModel(user, string.Join(' ', roles));
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(EditUserViewModel viewModel)
    {
        if (IdentityHelpers.GetRole(viewModel.Role) == string.Empty)
            return RedirectToAction(nameof(Index));

        //var roles = _roleManager.Roles.ToArray();

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == viewModel.UserId);

        await _userManager.RemoveFromRolesAsync(user, IdentityHelpers.Roles);
        await _userManager.AddToRoleAsync(user, viewModel.Role);

        return RedirectToAction(nameof(Index));
    }
}
