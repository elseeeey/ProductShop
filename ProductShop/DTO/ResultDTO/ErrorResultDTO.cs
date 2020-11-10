using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.DTO.ResultDTO
{
    public class ErrorResultDTO<T> : ResultDTO
    {
        public List<T> Errors { get; set; }
    }
}
