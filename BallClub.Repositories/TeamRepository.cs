using BallClub.Repositories.Data;
using BallClub.Repositories.Interfaces;
using BallClub.Repositories.Messages;
using System.Threading.Tasks;
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

        public async Task<DOMAIN.Team> AddAsync(DOMAIN.Team team)
        {
            var teamDTO = _translator.Translate(team);
            await _dbContext.AddAsync(teamDTO);
            await _dbContext.SaveChangesAsync();

            return team;
        }
    }
}
