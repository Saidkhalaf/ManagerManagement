using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerManagement.BL.Domain;

public class Developer : IValidatableObject
{
    [Required(ErrorMessage = "Id is required.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required]
    public string ProgrammingLanguage { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime JoinDate { get; set; }

    public ProjectStatus ProjectStatus { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
    public double Salary { get; set; }
    
    //Navigation Relation
    public ICollection<DeveloperTask> AssignedTasks { get; set; }

    public int? PhoneNr { get; set; }

    public Developer()
    {
    }

    public Developer(int id, string name, string programmingLanguage)
    {
        Id = id;
        Name = name;
        ProgrammingLanguage = programmingLanguage;
        AssignedTasks = new List<DeveloperTask>();
    }

    public Developer(int id, string name, string programmingLanguage, DateTime joinDate, ProjectStatus projectStatus,
        double salary, int? phoneNr)
    {
        Id = id;
        Name = name;
        ProgrammingLanguage = programmingLanguage;
        JoinDate = joinDate;
        ProjectStatus = projectStatus;
        Salary = salary;
        PhoneNr = phoneNr;
        AssignedTasks = new List<DeveloperTask>();
    }

    public override string ToString()
    {
        return
            $"Developer Name: {Name}\nEmployee ID: {Id}\nTask: {ProgrammingLanguage}\nSalary: {Salary}";
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        if (Id == 0)
        {
            results.Add(new ValidationResult("Id cannot be 0.", new[] { nameof(Id) }));
        }

        return results;
    }
}