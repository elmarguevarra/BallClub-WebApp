using BallClub.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BallClub.Models
{
    public class Player : Entity
    {
        [Key]
        public string Username { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        [ForeignKey("Season")]
        public int SeasonId { get; set; }
        public Name Name { get; set; }
        public string[] SocialMediaLinks { get; set; }
        public Stats Stats { get; set; }
        public Record Record { get; set; }
    }

    public class Record : ValueObject
    {
        public int Wins { get; set; }
        public int Loss { get; set; }
    }

    public class Stats : ValueObject
    {
        public int Points { get; set; }
        public int Assists { get; set; }
        public int Rebounds { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
    }

    public class Name : ValueObject
    {
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
        public string Suffix { get; set; }
    }
}