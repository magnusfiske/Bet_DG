using Bet.Common.DTOs;
using Bet.Users.Database.Entities;

namespace Bet.Token.API.Services
{
    public interface ITokenService
    {
        string? CreateToken(IList<string> roles, BetUser user);
        Task<string?> GenerateTokenAsync(TokenUserDTO tokenUserDTO);
        Task<AuthenticatedUserDTO> GetTokenAsync(LoginUserDTO loginUserDTO);
    }
}