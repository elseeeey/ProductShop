using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProductShop.DAL;
using ProductShop.DAL.Entities.Auth;
using ProductShop.DTO.ResultDTO;

namespace ProductShop.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        EFContext _context;

        UserManager<User> _userManager;

        SignInManager<User> _signInManager;

        IConfiguration _configuration;

        public AuthController(
            EFContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;

            _context = context;

            _configuration = configuration;

            _signInManager = signInManager;

        }

        [HttpPost("register")]
        public async Task<ResultDTO> Register([FromBody] UserRegisterDTO model)
        {
            if (model == null)
            {
                return new ResultDTO
                {
                    StatusCode = false,
                    Message = "Model is empty"
                };
            }
            
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var userProfile = new UserAdditionalInfo
            {
                FullName = model.FullName,
                Image = model.Image,
                DateOfBirth = model.DateOfBirth,
                Id = user.Id,
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new ErrorResultDTO<IdentityError>
                {
                    Errors = result.Errors.ToList()
                };
            }
            else
            {
                _context.UserAdditionalInfos.Add(userProfile);
                _context.SaveChanges();
            }

            return new ResultDTO
            {
                StatusCode = true
            };


        }
    }
}
