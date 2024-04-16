using Enums.UserRole;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models;

public class UserModel
{
    public string Id { get; set; }

    [Required]
    [StringLength(32)]
    public string Login { get; set; }

    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    [Required]
    public UserRole Role { get; set; }

    [Required]
    [StringLength(320)]
    public string Email { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }
}
