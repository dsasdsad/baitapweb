using DeThiThu2023.Data;
using DeThiThu2023.Models;
using DeThiThu2023.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace DeThiThu2023.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _db;


        public HomeController(ILogger<HomeController> logger, DataContext db)
        {
            _logger = logger;
            _db = db;

        }

        public IActionResult Index()
        {
            var hangHoas = _db.HangHoa.Where(x => x.Gia >= 100).ToList();

            ViewBag.Categories = _db.LoaiHang.ToList();
            return View(hangHoas);
        }

		public IActionResult Create()
		{
            var productViewModel = new ProductViewModel
            {
                LoaiHangs = _db.LoaiHang.ToList().Select(u => new SelectListItem
                {
                    Text = u.TenLoai,
                    Value = u.MaLoai.ToString(),
                }),
                HangHoa = new HangHoa()
			};

			return View(productViewModel);
		}

        [HttpPost]
		public IActionResult Create(ProductViewModel productViewModel)
		{

            if (ModelState.IsValid)
            {
                var product = _db.HangHoa.Add(productViewModel.HangHoa);
                productViewModel.LoaiHangs = _db.LoaiHang.ToList().Select(u => new SelectListItem
                {
                    Text = u.TenLoai,
                    Value = u.MaLoai.ToString(),
                });

				_db.SaveChanges();
            }

			return View(productViewModel);
		}

		public IActionResult LayTheoLoai(int maLoai)
		{
			var hangHoaTheoLoai = _db.HangHoa.Where(h => h.MaLoai == maLoai).ToList();
			return PartialView("TaQuangDung_MainContent", hangHoaTheoLoai);
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