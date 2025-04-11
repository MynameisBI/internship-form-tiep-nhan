using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Internship.Models;

[Table("lu_ethnicities")]
public partial class LuEthnicity
{
    [Key]
    [Column("ethnicity_id")]
    public int EthnicityId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Ethnicity")]
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
