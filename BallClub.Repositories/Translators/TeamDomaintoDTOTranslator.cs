using BallClub.Repositories.Interfaces;
using BallClub.Repositories.Messages;
using DOMAIN = BallClub.Domain.Models;

namespace BallClub.Repositories.Translators
{
    public class TeamDomaintoDTOTranslator : ITranslator<DOMAIN.Team, TeamDTO>
    {
        public TeamDTO Translate(DOMAIN.Team team)
        {
            return new TeamDTO
            {
                TeamId = team.Id,
                Name = team.Name
            };
        }
    }
}
