using Internship.Data;
using Internship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Register([Bind("PatientId",
                "PhoneNumber", "Name", "DateOfBirth", "Email",
                "CityId", "DistrictId", "WardId", "NumberAndRoad",
                "IsMale", "NationalityId", "EthnicityId", "ProfessionId")] Patient patient)
        {
            //patient.City = await _context.Cities.FindAsync(patient.CityId) ?? null!;
            //patient.District = await _context.Districts.FindAsync(patient.DistrictId) ?? null!;
            //patient.Ward = await _context.Wards.FindAsync(patient.WardId) ?? null!;

            if (ModelState.IsValid)
            {
                patient.PatientId = _context.Patients.Max(p => p.PatientId) + 1;
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cities = _context.Cities.ToList();
            return View(patient);
        }
    }
}
