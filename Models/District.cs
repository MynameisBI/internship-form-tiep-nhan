using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Internship.Models;

[Table("districts")]
public partial class District
{
    [Key]
    [Column("district_id")]
    public int DistrictId { get; set; }

    [Column("city_id")]
    public int CityId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("District")]
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
