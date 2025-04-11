using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Internship.Models;

[Table("wards")]
public partial class Ward
{
    [Key]
    [Column("ward_id")]
    public int WardId { get; set; }

    [Column("district_id")]
    public int DistrictId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Ward")]
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
