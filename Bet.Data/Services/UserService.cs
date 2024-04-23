namespace Bet.Data.Services;

public class UserService : IUserService
{
    private readonly UserManager<BetUser?> _userManager;

    public UserService(UserManager<BetUser?> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<BetUser>> GetUsersAsync(List<string> ids)
    {
        try
        {
            var users = new List<BetUser>();

            foreach (var id in ids)
            {
                users.Add(await _userManager.FindByIdAsync(id) ?? new BetUser());
            }

            return users;
        }
        catch 
        {
            return default;
        }
    }

    public async Task<BetUser> GetUserEmailAsync(string email)
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

            if (result.Equals(PasswordVerificationResult.Success))
            {
                return user;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
        catch
        {
        }

        return default;
    }
}
