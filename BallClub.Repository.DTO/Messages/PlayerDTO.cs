namespace BallClub.Repository.DTO.Messages
{
    public class PlayerDTO
    {
        public int PlayerId { get; set; }
        public string Username { get; set; }
        public int TeamId { get; set; }
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