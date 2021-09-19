using DOMAIN = BallClub.Domain.Models;
using DTO = BallClub.Repository.DTO.Messages;

namespace BallClub.Repository.DTO.Translators
{
    public class TeamTranslator 
    {
        public DOMAIN.Team TranslateDTOToDomain(DTO.Messages.TeamDTO team)
        {
            return DOMAIN.Team.CreateTeam(team.Name);
        }
    }
}
