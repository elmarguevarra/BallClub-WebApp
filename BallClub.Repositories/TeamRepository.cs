using BallClub.Repositories.Data;
using BallClub.Repositories.Interfaces;
using BallClub.Repositories.Messages;
using DOMAIN = BallClub.Domain.Models;

namespace BallClub.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITranslator<DOMAIN.Team, TeamDTO> _translator;

        public TeamRepository(
            ApplicationDbContext dbContext,
            ITranslator<DOMAIN.Team, TeamDTO> translator)
        {
            _dbContext = dbContext ??
                throw new System.ArgumentNullException(nameof(dbContext));
            _translator = translator ??
                throw new System.ArgumentNullException(nameof(translator));
        }

        public DOMAIN.Team Add(DOMAIN.Team team)
        {
            var teamDTO = _translator.Translate(team);
            _dbContext.Add(teamDTO);
            _dbContext.SaveChanges();

            return team;
        }
    }
}
