using System.ComponentModel.DataAnnotations;

namespace BallClub.Models
{
    public class Team
    {
        [Key]
        public string Name { get; set; }
        public Player[] Players { get; set; }
    }
}