using BallClub.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BallClub.Models
{
    public class Player : Entity
    {
        [Key]
        public int Username { get; set; }
        public int TeamID { get; set; }
        public int SeasonID { get; set; }
        public Name Name { get; set; }
        public List<string> SocialMediaLinks { get; set; }
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