using BallClub.Domain.Models.Base;

namespace BallClub.Domain.Models
{
    public class Team : Entity
    {
        //TODO: Make this private set
        public string Name { get; set; }

        private Team(int id, string name)
        {
            Id = id;
            Name = name ?? 
                throw new System.ArgumentNullException(nameof(name));
        }
        public Team()
        {

        }
        public static Team CreateTeam(string name, int id = 0)
        {
            return new Team(id, name);
        }
    }
}