using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Internship.Models;

[Table("lu_appointment_times")]
public partial class LuAppointmentTime
{
    [Key]
    [Column("appointment_time")]
    public int AppointmentTime { get; set; }

    [InverseProperty("AppointmentTimeNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
