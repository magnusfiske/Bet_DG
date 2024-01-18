using Bet.Common.DTOs;
using Bet.Users.Database.Entities;
using Bet.Users.Database.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bet.Token.API.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly UserManager<BetUser> _userManager;

    public TokenService(IConfiguration configuration, IUserService userService, UserManager<BetUser> userManager)
    {
        _configuration = configuration;
        _userService = userService;
        _userManager = userManager;
    }

    public string? CreateToken(IList<string> roles, BetUser user)
    {
        try
        {
            if (_configuration["Jwt:SigningSecret"] == null || roles == null || user == null)
            {
                throw new ArgumentException("JWT configuration missing");
            }

            var signingKey = Convert.FromBase64String(_configuration["Jwt:SigningSecret"]);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature);

            int duration = int.Parse(_configuration["Jwt:Duration"]);

            var now = DateTime.UtcNow;

            var expires = now.AddDays(duration);

            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"] ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"] ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Nbf, now.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, expires.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id)

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim("Role", role));
            }

            var jwtToken = new JwtSecurityToken(new JwtHeader(credentials), new JwtPayload(claims));

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.WriteToken(jwtToken);

            return token;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string?> GenerateTokenAsync(TokenUserDTO tokenUserDTO)
    {
        try
        {
            var user = await _userService.GetUserAsync(tokenUserDTO.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = CreateToken(roles, user);

            if (tokenUserDTO.Save)
            {
                var result = await _userManager.SetAuthenticationTokenAsync(user, _configuration["Jwt:Issuer"], "JwtToken", token);
                if (!result.Succeeded)
                {
                    throw new SecurityTokenException("Could not add token to user");
                }
            }

            return token;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //    Call the JwtParser.CompareTokenClaims with the token and compareToken values and
    //    store the result in a variable named success.
    //    16. If success it false, call the GenerateTokenAsync method and pass it an instance of the
    //    TokenDTO record with only the email to use the deafult true value for the other parameter
    //    to save the token in the database. Store the returned token in the existing token varaible.
    //17. Return an instance of the AuthenticatedUserDTO with token and user name.

    public async Task<AuthenticatedUserDTO> GetTokenAsync(LoginUserDTO loginUserDTO)
    {
        try
        {
            if (loginUserDTO == null) throw new UnauthorizedAccessException();

            var user = await _userService.GetUserAsync(loginUserDTO.Email);

            if (user == null) throw new UnauthorizedAccessException();

            var token = await _userManager.GetAuthenticationTokenAsync(user, _configuration["Jwt:Issuer"], "JwtToken");

            var compareToken = await GenerateTokenAsync(new TokenUserDTO(loginUserDTO.Email, false));

            var success = JwtParser.CompareTokenClaims(token, compareToken);

            if (!success)
            {
                token = await GenerateTokenAsync(new TokenUserDTO(loginUserDTO.Email));
            }

            return new AuthenticatedUserDTO(token, user.UserName);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}

public static class JwtParser
{
    public static bool CompareTokenClaims(string? token1, string? token2)
    {
        try
        {
            var claims1 = GetClaims(token1);
            var claims2 = GetClaims(token2);

            if(claims1.Count != claims2.Count)
            {
                return false;
            }

            for (int i = 0; i < claims1.Count; i++)
            {
                if (claims1[i] != claims2[i]) { return false; }
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static List<(string Type, string Value)> GetClaims(string? jwt)
    {
        var token = ConvertJwtStringToJwtSecurityToken(jwt);
        return token.Claims.Select(claim => (claim.Type, claim.Value)).ToList();
    }

    public static JwtSecurityToken ConvertJwtStringToJwtSecurityToken(string? jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        return token;
    }
}
