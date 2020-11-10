using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.DTO.ResultDTO
{
    public class CollectionResultDTO<T> : ResultDTO
    {
        public ICollection<T> Result { get; set; }
    }
}
