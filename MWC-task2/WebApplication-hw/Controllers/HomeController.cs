using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication_hw.Controllers;

public class HomeController : Controller
{
    private AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.SlideItems);
    }
}
