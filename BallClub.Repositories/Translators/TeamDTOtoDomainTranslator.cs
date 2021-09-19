using BallClub.Repositories.Interfaces;
using BallClub.Repositories.Messages;
using DOMAIN = BallClub.Domain.Models;

namespace BallClub.Repositories.Translators
{
    public class TeamDTOtoDomainTranslator : ITranslator<TeamDTO, DOMAIN.Team>
    {
        public DOMAIN.Team Translate(TeamDTO team)
        {
            return DOMAIN.Team.CreateTeam(team.Name);
        }
    }
}
