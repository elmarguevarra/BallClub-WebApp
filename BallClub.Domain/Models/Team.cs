namespace BallClub.Domain.Models
{
    public class Team
    {
        public int TeamId { get; private set; }
        public string Name { get; private set; }

        private Team(string name)
        {
            Name = name ?? 
                throw new System.ArgumentNullException(nameof(name));
        }

        public static Team CreateTeam(string name)
        {
            return new Team(name);
        }
    }
}