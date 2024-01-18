
namespace Bet.Users.Database.Services;

public class UserService : IUserService
{
    private readonly UserManager<BetUser?> _userManager;

    public UserService(UserManager<BetUser?> userManager)
    {
        _userManager = userManager;
    }

    public async Task<BetUser> GetUserAsync(string email)
    {
        try
        {
            return await _userManager.FindByEmailAsync(email);
        }
        catch
        {
            return default;
        }
    }

    public async Task<BetUser> GetUserAsync(LoginUserDTO loginUser)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user == null) return default;

            var hasher = new PasswordHasher<BetUser>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, loginUser.Password);

            if (result.Equals(PasswordVerificationResult.Success)) return user;
        }
        catch
        {
        }

        return default;
    }
}
