﻿using Internship.Data;
using Internship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;

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
            var patients = await _context.Patients.ToListAsync();
            return View(patients);
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
            if (ModelState.IsValid)
            {
                patient.PatientId = (_context.Patients.Any() ? _context.Patients.Max(p => p.PatientId) : 0) + 1;
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Cities = _context.Cities.ToList();
            return View(patient);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Patient inputPatient)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PhoneNumber == inputPatient.PhoneNumber);
            if (patient == null)
            {
                ModelState.AddModelError("", "Can't find patient with phone number");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, patient.Name),
                new Claim("PatientId", patient.PatientId.ToString()),
                new Claim(ClaimTypes.Email, patient.Email ?? string.Empty),
                new Claim("PhoneNumber", patient.PhoneNumber ?? string.Empty),
                new Claim("DateOfBirth", patient.DateOfBirth.ToString()),
                new Claim("CityId", patient.CityId.ToString()),
                new Claim("DistrictId", patient.DistrictId.ToString()),
                new Claim("WardId", patient.WardId.ToString()),
                new Claim("NumberAndRoad", patient.NumberAndRoad ?? string.Empty),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("LoginAuth", principal);

            return RedirectToAction("Index", "Home");
        }

    }
}
