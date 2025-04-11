using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Internship.Models;

[Table("patients")]
public partial class Patient
{
    [Key]
    [Column("patient_id")]
    public int PatientId { get; set; }

    [Column("phone_number")]
    [StringLength(25)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [Column("name")]
    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }

    [Column("email")]
    [StringLength(255)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("city_id")]
    public int CityId { get; set; }

    [Column("district_id")]
    public int DistrictId { get; set; }

    [Column("ward_id")]
    public int WardId { get; set; }

    [Column("number_and_road")]
    [StringLength(255)]
    [Unicode(false)]
    public string? NumberAndRoad { get; set; }

    [Column("is_male")]
    public bool IsMale { get; set; }

    [Column("nationality_id")]
    public int? NationalityId { get; set; }

    [Column("ethnicity_id")]
    public int? EthnicityId { get; set; }

    [Column("profession_id")]
    public int? ProfessionId { get; set; }

    [InverseProperty("Patient")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [ForeignKey("CityId")]
    [InverseProperty("Patients")]
    public virtual City City { get; set; } = null!;

    [ForeignKey("DistrictId")]
    [InverseProperty("Patients")]
    public virtual District District { get; set; } = null!;

    [ForeignKey("EthnicityId")]
    [InverseProperty("Patients")]
    public virtual LuEthnicity? Ethnicity { get; set; }

    [ForeignKey("NationalityId")]
    [InverseProperty("Patients")]
    public virtual LuNationality? Nationality { get; set; }

    [ForeignKey("ProfessionId")]
    [InverseProperty("Patients")]
    public virtual LuProfession? Profession { get; set; }

    [ForeignKey("WardId")]
    [InverseProperty("Patients")]
    public virtual Ward Ward { get; set; } = null!;
}
