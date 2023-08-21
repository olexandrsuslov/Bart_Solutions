using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TestBartSolutions.Core.Models;

[Index(nameof(Email), IsUnique = true)]
public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    [Required]
    public string Email { get; set; }
    public int? AccountId { get; set; }
    public Account? Account { get; set; }
}