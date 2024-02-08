using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bet.Common.DTOs; 
public record TeamDTO 
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Position { get; set; }

    public int? PreviousPosition { get; set; }
}
