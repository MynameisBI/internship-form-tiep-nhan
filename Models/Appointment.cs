using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Internship.Models;

[Table("appointments")]
public partial class Appointment
{
    [Key]
    [Column("appointment_id")]
    public int AppointmentId { get; set; }

    [Column("patient_id")]
    public int PatientId { get; set; }

    [Column("appointment_time")]
    public int AppointmentTime { get; set; }

    [Column("clinic_id")]
    public int ClinicId { get; set; }

    [ForeignKey("AppointmentTime")]
    [InverseProperty("Appointments")]
    public virtual LuAppointmentTime AppointmentTimeNavigation { get; set; } = null!;

    [ForeignKey("ClinicId")]
    [InverseProperty("Appointments")]
    public virtual Clinic Clinic { get; set; } = null!;

    [ForeignKey("PatientId")]
    [InverseProperty("Appointments")]
    public virtual Patient Patient { get; set; } = null!;
}
