using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BallClub.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int TeamId { get; set; }
        public int SeasonId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string[] SocialMediaLinks { get; set; }
        public int Points { get; set; }
        public int Assists { get; set; }
        public int Rebounds { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Wins { get; set; }
        public int Loss { get; set; }
    }
}