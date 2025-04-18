using System.Diagnostics;
using Internship.Data;
using Internship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Internship.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HospitalDbContext _context;

        public HomeController(ILogger<HomeController> logger, HospitalDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var cities = _context.Cities.ToList();
            //ViewBag.Cities = new SelectList(cities, "CityId", "Name");
            return View(cities);
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

        public JsonResult GetDistricts(int cityId)
        {
            var districts = _context.Districts
                .Where(d => d.CityId == cityId)
                .Select(d => new { d.DistrictId, d.Name })
                .ToList();

            return Json(districts);
        }

        public JsonResult GetWards(int districtId)
        {
            var wards = _context.Wards
                .Where(w => w.DistrictId== districtId)
                .Select(w => new { w.WardId, w.Name })
                .ToList();

            return Json(wards);
        }

        public JsonResult GetNationalities()
        {
            return Json(_context.LuNationalities.ToList());
        }

        public JsonResult GetEthnicities()
        {
            return Json(_context.LuEthnicities.ToList());
        }

        public JsonResult GetProfessions()
        {
            return Json(_context.LuProfessions.ToList());
        }

        public JsonResult GetAppointmentTimes()
        {
            return Json(_context.LuAppointmentTimes.ToList());
        }
    }
}
