using Bet.Common.DTOs;
using Bet.Data.Entities;
using Bet.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Bet.API.Services;

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

            var signingKey = Encoding.ASCII.GetBytes(_configuration["Jwt:SigningSecret"]);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature);

            int duration = int.Parse(_configuration["Jwt:Duration"]);

            var now = DateTime.UtcNow;

            var expires = now.AddDays(duration);

            //List<Claim> claims = new()
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimTypes.Email, user.Email),
            //};

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

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
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var subject = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                SigningCredentials = credentials,

            };


            //var jwtToken = new JwtSecurityToken(new JwtHeader(credentials), new JwtPayload(claims));

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            //var token = tokenHandler.WriteToken(jwtToken);

            return tokenHandler.WriteToken(token);
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
            var user = await _userService.GetUserEmailAsync(tokenUserDTO.Email);

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

    public async Task<AuthenticatedUserDTO> GetTokenAsync(LoginUserDTO loginUserDTO)
    {
        try
        {
            if (loginUserDTO == null) throw new UnauthorizedAccessException();

            var user = await _userService.GetUserEmailAsync(loginUserDTO.Email);

            //bool isValidPassword = await _userManager.CheckPasswordAsync(user, loginUserDTO.Password);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDTO.Password)) throw new UnauthorizedAccessException();

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
