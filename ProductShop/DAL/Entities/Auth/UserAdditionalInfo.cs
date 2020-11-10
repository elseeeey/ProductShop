using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.DAL.Entities.Auth
{
    public class UserAdditionalInfo
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string FullName { get; set; }

        public string Image { get; set; }

        public DateTime DateOfBirth { get; set; }

        public User User { get; set; }
    }
}
