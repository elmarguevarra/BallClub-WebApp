using System.ComponentModel.DataAnnotations.Schema;

namespace BallClub.Repositories.Messages
{
    public class TeamDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }
        public string Name { get; set; }
    }
}