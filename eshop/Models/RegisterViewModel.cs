using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(256)]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        //[UniqueCharacters(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$", ErrorMessage=RegisterViewModel.ErrorMessagePassword)]
        public string Password { get; set; }
        private const string ErrorMessagePassword = "Password does not met requirements";

        [Required]
        [Compare(nameof(Password),ErrorMessage = "Passwords do not match")]
        public string RepeatedPassword { get; set; }

        public string[] ErrorsDuringRegister { get; set; }


    }
}
