using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.DAL.Entities.Auth
{
    public class UserRegisterDTO
    {
        [Required]
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public string Image { get; set; }
    }
}
