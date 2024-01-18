using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bet.Data.Interfaces;

namespace Bet.Data.Entities;

public class Bet : IEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<BetRow>? BetRows { get; set; }

    public virtual User? User { get; set; }
}
