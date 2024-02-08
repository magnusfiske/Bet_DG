using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bet.Data.Interfaces;

namespace Bet.Data.Entities;

public class Team : IEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int Position { get; set; }

    public int PreviousPosition { get; set; }

}
