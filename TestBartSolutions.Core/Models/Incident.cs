using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestBartSolutions.Core.Models;

public class Incident
{
    //DatabaseGenerated(DatabaseGeneratedOption.Identity)
    [Key]
    public string IncidentName { get; set; }
    public string Description { get; set; }
    public ICollection<Account> Accounts { get; } = new List<Account>();
}