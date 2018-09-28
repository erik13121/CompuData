using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CompuData.Models
{
    public class ResetPasswordModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "The New Password is required")]
        [MembershipPassword(
            MinRequiredNonAlphanumericCharacters = 1,
            MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc.)",
            MinRequiredPasswordLength = 6,
            ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc.)"
        )]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "Max of 50 characters are allowed for the Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Confirmed New Password is required")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "New password and confirmation do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ReturnToken { get; set; }
    }
}