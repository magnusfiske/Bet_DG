using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bet.Common.DTOs;

public record BetRowDTO
{
    public int Id { get; set; }

    public int BetId { get; set; }

    public int Placing { get; set; }

    public int TeamId { get; set; }

    public virtual TeamDTO? Team { get; set; }
}


