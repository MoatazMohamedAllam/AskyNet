using API.Dtos;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized();

            return new UserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = "this is token"
            };
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest();

            return new UserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = "this is token"
            };

        }
    }
}
