
namespace Bet.API.Services
{
    public interface ITokenService
    {
        string? CreateToken(IList<string> roles, BetUser user);
        Task<string?> GenerateTokenAsync(TokenUserDTO tokenUserDTO);
        Task<AuthenticatedUserDTO> GetTokenAsync(LoginUserDTO loginUserDTO);
    }
}