using ecommerce_api.Controllers.Base;
using ecommerce_api.Domain.Models.Identity;
using ecommerce_api.Dtos;
using ecommerce_api.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_api.Controllers;

public class AccountController : BaseController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) return Unauthorized(new ApiResponse(401));

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

        return new UserDto
        {
            Email = user.Email,
            Token = "This will be a token",
            DisplayName = user.DisplayName
        };
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(new ApiResponse(400));

        return new UserDto
        {
            DisplayName = user.DisplayName,
            Token = "This will be a token",
            Email = user.Email
        };
    }
}