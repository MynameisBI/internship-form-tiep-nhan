using Internship.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Internship.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HospitalDbContext _context;

        public AppointmentsController(ILogger<HomeController> logger, HospitalDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return View(appointments);
        }

        [HttpPost]
        public IActionResult UpdateIsDone(int appointmentId, bool? isDone)
        {
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                // If isDone is null, treat it as false (un-checked box)
                appointment.IsDone = isDone ?? false;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
