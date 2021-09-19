using System;

namespace BallClub.Repositories.Messages
{
    public class GameDTO
    {
        public int GameId { get; set; }
        public DateTime Schedule { get; set; }
        public string[] TeamIds { get; set; }
        public string[] PlayerIds { get; set; }
        public int SeasonId { get; set; }
    }
}
