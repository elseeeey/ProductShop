using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductShop.DTO.ResultDTO;

namespace ProductShop.Controllers
{
    public class SingleResultDTO<T> : ResultDTO
    {
        public T Result { get; set; }
    }
}
