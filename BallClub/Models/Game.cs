using BallClub.Models.Base;
using System;

namespace BallClub.Models
{
    public class Game : Entity
    {
        public DateTime DateTime { get; set; }
        public Team[] Teams { get; set; }
        public Player[] Players { get; set; }
    }
}
