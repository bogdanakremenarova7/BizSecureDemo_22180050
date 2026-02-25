using System.Security.Claims;
using BizSecureDemo_22180050.Data;
using BizSecureDemo_22180050.Models;
using BizSecureDemo_22180050.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizSecureDemo.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly AppDbContext _db;
    public OrdersController(AppDbContext db) => _db = db;

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOrderVm vm)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index", "Home");

        var uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        _db.Orders.Add(new Order
        {
            UserId = uid,
            Title = vm.Title,
            Amount = vm.Amount
        });

        await _db.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Details(int id)
    {
        var uid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id && o.UserId == uid);


        if (order == null)
        {
            return NotFound(); //скрива дали записът съществува
        }

        return View(order);
    }
}

