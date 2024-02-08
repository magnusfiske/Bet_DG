using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bet.Common.DTOs; 
public record UserDTO 
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool Paid { get; set; }

    public bool Submited { get; set; }

    public string? AspNetUserId { get; set; }

    public virtual List<int>? Bets { get; set; }
}
