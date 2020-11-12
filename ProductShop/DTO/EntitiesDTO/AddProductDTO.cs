using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.DTO.EntitiesDTO
{
    public class AddProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Year { get; set; }

        public string Image { get; set; }

        public string CategoryName { get; set; }
    }
}
