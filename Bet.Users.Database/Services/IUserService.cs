namespace Bet.Users.Database.Services
{
    public interface IUserService
    {
        Task<BetUser> GetUserAsync(LoginUserDTO loginUser);
        Task<BetUser> GetUserAsync(string email);
    }
}