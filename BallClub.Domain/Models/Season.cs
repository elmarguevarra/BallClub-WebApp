using BallClub.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BallClub.Models
{
    public class Season : Entity
    {
        public string Name { get; set; }
    }
}