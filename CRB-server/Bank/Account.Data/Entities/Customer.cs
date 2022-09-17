using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data.Entities;

[Index("Email", IsUnique = true)]

public class Customer
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [MaxLength(50)]
    [Required]
    public string FirstName { get; set; }
    [MaxLength(50)]
    [Required]
    public string LastName { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [MaxLength(16)]
    [MinLength(8)]
    [Required]
    public string Password { get; set; }
}
