using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
  {
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      if (await UserExits(registerDto.Username)) return BadRequest("Username is taken");

      return Ok();

      // using var hmac = new HMACSHA512();

      // var user = new AppUser
      // {
      //   UserName = registerDto.Username,
      //   PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
      //   PasswordSalt = hmac.Key
      // };

      // context.Users.Add(user);
      // await context.SaveChangesAsync();

      // return new UserDto
      // {
      //   UserName = user.UserName,
      //   Token = tokenService.CreateToken(user)
      // };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
      var user = await context.Users
        .Include(p => p.Photos)
        .FirstOrDefaultAsync(x =>
        x.UserName == loginDto.Username.ToLower());

      if (user == null) return Unauthorized("Invalid username");

      using var hmac = new HMACSHA512(user.PasswordSalt);
      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

      for (int i = 0; i < computedHash.Length; i++)
      {
        if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
      }

      return new UserDto
      {
        UserName = user.UserName,
        Token = tokenService.CreateToken(user),
        PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
      };
    }

    private async Task<bool> UserExits(string username)
    {
      return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
  }
}