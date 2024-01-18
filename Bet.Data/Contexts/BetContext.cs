using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bet.Data.Contexts;

public class BetContext : DbContext
{
	public BetContext(DbContextOptions<BetContext> options) 
		: base(options) {}

	public DbSet<Entities.Bet> Bets => Set<Entities.Bet>();
	public DbSet<BetRow> BetRows => Set<BetRow>();
	public DbSet<Team> Teams => Set<Team>();
	public DbSet<User> Users => Set<User>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		SeedData(builder);
	}

	private void SeedData(ModelBuilder builder)
	{
        var teams = new List<Team>
        {
            new Team {
                Id = 1,
                Name = "Hammarby IF"
            },
            new Team {
                Id = 2,
                Name = "Malmö FF"
            },
            new Team {
                Id = 3,
                Name = "IF Elfsborg"
            },
            new Team {
                Id = 4,
                Name = "BK Häcken"
            },
            new Team {
                Id = 5,
                Name = "Djurgården"
            },
            new Team {
                Id = 6,
                Name = "Kalmar FF"
            },
            new Team {
                Id = 7,
                Name = "IFK Norrköping"
            },
            new Team {
                Id = 8,
                Name = "IFK Värnamo"
            },
            new Team {
                Id = 9,
                Name = "IK Sirius"
            },
            new Team {
                Id = 10,
                Name = "Mjällby AIF"
            },
            new Team {
                Id = 11,
                Name = "AIK"
            },
            new Team {
                Id = 12,
                Name = "Hamlstad BK"
            },
            new Team {
                Id = 13,
                Name = "IFK Göteborg"
            },
            new Team {
                Id = 14,
                Name = "IF Brommapojkarna"
            },
            new Team {
                Id = 15,
                Name = "Degerfors IF"
            },
            new Team {
                Id = 16,
                Name = "Varberg BoIS"
            }
        };

        builder.Entity<Team>().HasData(teams);
	}
}
