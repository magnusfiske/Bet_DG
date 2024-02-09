namespace Bet.Data.Services
{
    public interface IUserService
    {
        Task<BetUser> GetUserAsync(LoginUserDTO loginUser);
        Task<BetUser> GetUserEmailAsync(string email);
    }
}