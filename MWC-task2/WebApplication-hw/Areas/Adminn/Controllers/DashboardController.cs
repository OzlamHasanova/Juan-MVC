using Microsoft.AspNetCore.Mvc;

namespace WebApplication_hw.Areas.Adminn.Controllers;
[Area("Adminn")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
