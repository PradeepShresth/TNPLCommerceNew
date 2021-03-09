using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TNPLCommerce.Domain.UserModels
{
    public class User
    {
        public string UserId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Username should contain minimum of 3 and maximum of 10 characters")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string UserRole { get; set; }
    }
}
