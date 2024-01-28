using Bet.Common.Classes;
using Bet.Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Bet.Users.API.Controllers;

[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<BetUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManger;

    public AccountsController(UserManager<BetUser> userManager, RoleManager<IdentityRole> roleManger)
    {
        _userManager = userManager;
        _roleManger = roleManger;
    }

    [Route("api/accounts/seed")]
    [HttpPost]
    public async Task<IResult> Seed()
    {
        try
        {
            await _roleManger.CreateAsync(new IdentityRole { Id = "1", Name = UserRole.Admin });
            await _roleManger.CreateAsync(new IdentityRole { Id = "2", Name = UserRole.Customer });
            await _roleManger.CreateAsync(new IdentityRole { Id = "3", Name = UserRole.Registered });

            await AddUserAsync("raspen@bet.com", "Pass123__");
            await AddUserAsync("sylve@bulk.com", "Pass123__");

            await AddRolesAsync("raspen@bet.com", new List<string> { UserRole.Admin, UserRole.Registered, UserRole.Customer });
            await AddRolesAsync("sylve@bulk.com", new List<string> {UserRole.Registered, UserRole.Customer });

            return Results.Ok();
        }
        catch { }

        return Results.BadRequest();
    }

    [Route("api/accounts/register")]
    [HttpPost]
    public async Task<IResult> Register(RegisterUserDTO registerUserDTO)
    {
        try
        {
            var result = await AddUserAsync(registerUserDTO.Email, registerUserDTO.Password);
            if (result.Equals(Results.BadRequest())) return Results.BadRequest();

            result = await AddRolesAsync(registerUserDTO.Email, registerUserDTO.Roles);
            if (result.Equals(Results.BadRequest())) return Results.BadRequest();

            return Results.Ok();
        }
        catch { }

        return Results.BadRequest();
    }

    [Route("api/accounts/paid")]
    [HttpPost]
    public async Task<IResult> Paid(PaidCustomerDTO paidCustomerDTO)
    {
        return await AddRolesAsync(paidCustomerDTO.Email, new List<string> { UserRole.Customer });
    }

    [Route("api/accounts/{email}")]
    [HttpGet]
    public async Task<IResult> GetResultAsync(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);
        if(result.Equals(Results.BadRequest()) || result.Equals(Results.NotFound())) return Results.NotFound();
        return Results.Ok(result);
    }

    private async Task<IResult> AddUserAsync(string email, string password)
    {
        try
        {
            if(!ModelState.IsValid) return Results.BadRequest();

            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null) return Results.BadRequest();

            BetUser newUser = new()
            {
                Email = email,
                EmailConfirmed = true,
                UserName = email
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, password);

            if (result.Succeeded) return Results.Ok();
        }
        catch(Exception ex) {
            return Results.BadRequest(ex);
        }

        return Results.BadRequest();
    }

    private async Task<IResult> AddRolesAsync(string email, List<string> roles)
    {
        try
        {
            if (!ModelState.IsValid) return Results.BadRequest();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return Results.BadRequest();

            var results = await _userManager.AddToRolesAsync(user, roles);

            if (results.Succeeded) return Results.Ok();
        }
        catch { }

        return Results.BadRequest();
    }
}
