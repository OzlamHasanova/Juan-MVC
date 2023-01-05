using Core.Entities;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication_hw.Areas.Adminn.Controllers;
[Area("Adminn")]
public class SlideItemController : Controller
{
    private readonly AppDbContext _context;
    public SlideItemController(AppDbContext context)
    {
            _context=context;
    }
    public IActionResult Index()
    {
        return View(_context.SlideItems);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SlideItem item)
    {
        if (!ModelState.IsValid) return View(item);
        await _context.SlideItems.AddAsync(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        var model = await _context.SlideItems.FindAsync(id);
        if(model == null)return NotFound(); 
        return View(model);
    }

    public async Task<IActionResult> Update(int id)
    {
        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }



    [HttpPost]
    public async Task<IActionResult> Update(int id,SlideItem item)
    {
        if (id != item.Id) return BadRequest();
        if (!ModelState.IsValid) return View(item);
        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();
        model.Title = item.Title;
        model.SubTitle=item.SubTitle;
        model.Desc=item.Desc;
        model.Button=item.Button;
        _context.SlideItems.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();
        _context.SlideItems.Remove(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}
