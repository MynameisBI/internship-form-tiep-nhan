using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Internship.Models;

[Table("lu_professions")]
public partial class LuProfession
{
    [Key]
    [Column("profession_id")]
    public int ProfessionId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Profession")]
    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
