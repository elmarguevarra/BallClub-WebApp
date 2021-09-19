using BallClub.Repositories.Data;
using BallClub.Repositories.Interfaces;
using BallClub.Repositories.Messages;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DOMAIN = BallClub.Domain.Models;

namespace BallClub.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITranslator<DOMAIN.Team, TeamDTO> _domainToDTOTranslator;
        private readonly ITranslator<TeamDTO, DOMAIN.Team> _dtoToDomaintranslator;

        public TeamRepository(
            ApplicationDbContext dbContext,
            ITranslator<DOMAIN.Team, TeamDTO> domainToDTOtranslator,
            ITranslator<TeamDTO, DOMAIN.Team> dtoToDomaintranslator)
        {
            _dbContext = dbContext ??
                throw new System.ArgumentNullException(nameof(dbContext));
            _domainToDTOTranslator = domainToDTOtranslator ??
                throw new System.ArgumentNullException(nameof(domainToDTOtranslator));
            _dtoToDomaintranslator = dtoToDomaintranslator ?? 
                throw new System.ArgumentNullException(nameof(dtoToDomaintranslator));
        }

        public async Task<DOMAIN.Team> AddAsync(DOMAIN.Team team)
        {
            var teamDTO = _domainToDTOTranslator.Translate(team);
            await _dbContext.Teams.AddAsync(teamDTO);
            await _dbContext.SaveChangesAsync();

            return team;
        }

        public async Task<DOMAIN.Team> FindAsync(int id)
        {
            var teamDTO = await _dbContext.FindAsync<TeamDTO>(id);
            return _dtoToDomaintranslator.Translate(teamDTO);
        }

        public async Task<List<DOMAIN.Team>> GetAll()
        {
            var teamsDTO = await _dbContext.Teams.ToListAsync();
            return teamsDTO.Select(_dtoToDomaintranslator.Translate).ToList();
        }

        public async void RemoveAsync(int id)
        {
            var teamDTO = await _dbContext.FindAsync<TeamDTO>(id);
            _dbContext.Teams.Remove(teamDTO);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<DOMAIN.Team> UpdateAsync(DOMAIN.Team team)
        {
            var teamDTO = _domainToDTOTranslator.Translate(team);
            _dbContext.Teams.Update(teamDTO);
            await _dbContext.SaveChangesAsync();

            return team;
        }
    }
}
