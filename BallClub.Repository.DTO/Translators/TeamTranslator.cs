using DOMAIN = BallClub.Domain.Models;
using DTO = BallClub.Repository.DTO.Models;

namespace BallClub.Repository.DTO.Translators
{
    public class TeamTranslator 
    {
        public DOMAIN.Team TranslateDTOToDomain(DTO.Models.Team team)
        {
            return DOMAIN.Team.CreateTeam(team.Name);
        }
    }
}
