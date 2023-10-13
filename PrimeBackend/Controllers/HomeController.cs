using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeBackend.Models;
using System.Diagnostics;

namespace PrimeBackend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        // GET: FormDatas
        public async Task<IActionResult> Index(string? alert)
        {
            ViewBag.Alert = alert;
            return View();
        }

        // GET: FormDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FormDatas == null)
            {
                return NotFound();
            }

            var formData = await _context.FormDatas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formData == null)
            {
                return NotFound();
            }

            return View(formData);
        }


        // POST: FormDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Occupation,PhoneNumber")] FormData formData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { alert = "true" });
            }
            return RedirectToAction("Index", new { alert = "false" });
        }

        [Route("{action}")]
        public async Task<IActionResult> GetAll(string secretKey)
        {
            if (secretKey=="prime2023")
            {
                return _context.FormDatas != null ?
                       View(await _context.FormDatas.ToListAsync()) :
                       Problem("Entity set 'AppDbContext.FormDatas'  is null.");
            }
            return BadRequest();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}