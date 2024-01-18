using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Bet.Data.Interfaces;

namespace Bet.Data.Entities;

public class BetRow : IEntity
{
    public int Id { get; set; }

    public int BetId { get; set; }

    public int Placing { get; set; }

    public int TeamId { get; set; }

    public virtual Bet? Bet { get; set; } 

    public virtual Team? Team { get; set; }
}
