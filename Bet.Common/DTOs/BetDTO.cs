using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bet.Common.DTOs;

public record BetDTO
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public virtual List<BetRowDTO>? BetRows { get; set; } = new();

    public virtual UserDTO? User { get; set; } = new();
}
