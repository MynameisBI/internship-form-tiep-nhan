using Internship.Data;
using Internship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Internship.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HospitalDbContext _context;

        public PatientsController(ILogger<HomeController> logger, HospitalDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var patient = await _context.Patients.ToListAsync();
            return View(patient);
        }

        public IActionResult Register()
        {
            ViewBag.Cities = _context.Cities.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("PatientId", "PhoneNumber", "Name")] Patient patient)
        {
            patient.DateOfBirth = DateOnly.FromDateTime(DateTime.Now);
            patient.Email = "hallo@hallo.com";
            patient.CityId = 1;
            patient.DistrictId = 1;
            patient.WardId = 1;
            patient.NumberAndRoad = "123 Main St";
            patient.IsMale = true;
            patient.NationalityId = 1;
            patient.EthnicityId = 1;
            patient.ProfessionId = 1;
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(patient);
        }
    }
}
