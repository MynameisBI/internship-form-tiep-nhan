using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Internship.Models;

[Table("clinics")]
public partial class Clinic
{
    [Key]
    [Column("clinic_id")]
    public int ClinicId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [InverseProperty("Clinic")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
