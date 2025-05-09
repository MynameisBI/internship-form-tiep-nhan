using System.Diagnostics;
using Internship.Data;
using Internship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index([Bind("AppointmentId",
                "ClinicId", "AppointmentTime")] Appointment appointment)
        {
            appointment.PatientId = int.Parse(User.FindFirst("PatientId")?.Value);

            if (ModelState.IsValid)
            {
                appointment.AppointmentId = (_context.Appointments.Any() ? _context.Appointments.Max(a => a.AppointmentId) : 0) + 1;
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(appointment);
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

        public JsonResult GetClinics()
        {
            return Json(_context.Clinics.ToList());
        }
    }
}
