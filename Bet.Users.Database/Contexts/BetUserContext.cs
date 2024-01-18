namespace Bet.Users.Database.Contexts;

public class BetUserContext : IdentityDbContext<BetUser>
{
    public BetUserContext(DbContextOptions<BetUserContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
