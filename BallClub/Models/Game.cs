using System;

namespace BallClub.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public DateTime Schedule { get; set; }
        public string[] TeamIds { get; set; }
        public string[] PlayerIds { get; set; }
        public int SeasonId { get; set; }
    }
}
