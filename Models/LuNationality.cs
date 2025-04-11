using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Internship.Models;

[Table("lu_nationalities")]
public partial class LuNationality
{
    [Key]
    [Column("nationality_id")]
    public int NationalityId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Nationality")]
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
