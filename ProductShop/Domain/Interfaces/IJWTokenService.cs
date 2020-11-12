using ProductShop.DAL.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.Domain.Interfaces
{
    public interface IJWTokenService
    {
        public string CreateToken(User user);
    }
}
