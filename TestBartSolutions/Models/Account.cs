using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace TestBartSolutions.Models;

[Index(nameof(Name), IsUnique = true)]
public class Account
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [ForeignKey(nameof(Incident))]
    public string? IncidentName { get; set; }
    public Incident? Incident { get; set; }

    public ICollection<Contact> Contacts { get; } = new List<Contact>();
}