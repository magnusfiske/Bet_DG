using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bet.Data.Interfaces;

namespace Bet.Data.Entities;

public class User : IEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool Paid { get; set; }

    public bool Submited { get; set; }

    public string? AspNetUserId { get; set; }

    public virtual ICollection<Bet>? Bets { get; set; }

    public virtual BetUser AspNetUser { get; set; }
}
