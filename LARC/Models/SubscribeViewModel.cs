using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LARC.Models
{
  public class SubscribeViewModel
  {
    [Required]
    [Display(Name = "First Name:")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name:")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "E-mail address:")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Password:")]
    public string Password { get; set; }

    [Required]
    [Display(Name = "Confirm Password:")]
    [Compare("Password",
        ErrorMessage = "Confirm password doesn't match, Type again !")]
    public string ConfirmPassword { get; set; }
  }
}