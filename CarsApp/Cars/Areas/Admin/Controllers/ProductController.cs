using Cars.DAL;
using Cars.Models;
using Cars.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        
        public ProductController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var products=await _context.Products.Include(p=>p.Category).ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            CreateProductVM productVM = new CreateProductVM
            {
                Categories = await _context.Categories.ToListAsync()
            };
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {

            if(!ModelState.IsValid) 
            {
                productVM.Categories = await _context.Categories.ToListAsync();
              return View(productVM);
            }

            string filename=Guid.NewGuid().ToString()+productVM.Photo.FileName;

            string path=Path.Combine(_env.WebRootPath,"assets","images",filename);
            FileStream file = new FileStream(path,FileMode.Create);
            await productVM.Photo.CopyToAsync(file);
            await _context.Products.AddAsync(new Product
            {
                CategoryId = productVM.CategoryId,
                Image = filename,
                Name = productVM.Name
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
