using BallClub.Domain.Models.Base;
using System;

namespace BallClub.Domain.Models
{
    public class Game : Entity
    {
        public DateTime DateTime { get; set; }
        public Team[] Teams { get; set; }
        public Player[] Players { get; set; }
    }
}
