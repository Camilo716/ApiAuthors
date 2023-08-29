using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiAuthors.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ApiAuthors.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<IdentityUser> _signInManager;

    public UserController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser>  signInManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _signInManager = signInManager;
    }

    [HttpPost("signup")]
    public async Task<ActionResult<AuthenticationResponse>> SignUpAsync(UserCredentials userCredentials)
    {
        var user = new IdentityUser {
            UserName=userCredentials.Email,
            Email=userCredentials.Email};

        var result = await _userManager.CreateAsync(user, userCredentials.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return buildToken(userCredentials);  
    } 

    [HttpPost("logging")]
    public async Task<ActionResult<AuthenticationResponse>> LoggingAsync(UserCredentials userCredentials)
    {
        var result = await _signInManager.PasswordSignInAsync(
            userCredentials.Email,
            userCredentials.Password,
            isPersistent:false,
            lockoutOnFailure:false);

        if (!result.Succeeded)
            return BadRequest("failed logging");

        return buildToken(userCredentials);
    }
    private AuthenticationResponse buildToken(UserCredentials userCredentials)
    {
        var claims = new List<Claim>()
        {
            new Claim("email", userCredentials.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["keyjwt"]!)); 
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.Now.AddYears(1);

        var securityToken = new JwtSecurityToken(
            issuer:null, audience:null,
            claims:claims, expires:expiration, signingCredentials: credentials );

        return new AuthenticationResponse()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
            Expiration = expiration
        };
    }
}