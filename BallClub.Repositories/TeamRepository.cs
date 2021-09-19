using BallClub.Repositories.Data;
using BallClub.Repositories.Interfaces;
using BallClub.Repositories.Messages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public DOMAIN.Team Add(DOMAIN.Team team)
        {
            var teamDTO = _domainToDTOTranslator.Translate(team);
            _dbContext.Teams.Add(teamDTO);
            _dbContext.SaveChanges();

            return team;
        }

        public async Task<DOMAIN.Team> AddAsync(DOMAIN.Team team)
        {
            var teamDTO = _domainToDTOTranslator.Translate(team);
            await _dbContext.Teams.AddAsync(teamDTO);
            await _dbContext.SaveChangesAsync();

            return team;
        }

        public DOMAIN.Team Find(int id)
        {
            var teamDTO = _dbContext.Find<TeamDTO>(id);
            _dbContext.Entry(teamDTO).State = EntityState.Detached;
            return _dtoToDomaintranslator.Translate(teamDTO);
        }

        public async Task<DOMAIN.Team> FindAsync(int id)
        {
            var teamDTO = await _dbContext.FindAsync<TeamDTO>(id);
            _dbContext.Entry(teamDTO).State = EntityState.Detached;
            return _dtoToDomaintranslator.Translate(teamDTO);
        }

        public List<DOMAIN.Team> GetAll()
        {
            var teamsDTO = _dbContext.Teams;
            var results = teamsDTO.Select(_dtoToDomaintranslator.Translate);
            return results.ToList();
        }

        public void Remove(int id)
        {
            var teamDTO = _dbContext.Find<TeamDTO>(id);
            _dbContext.Teams.Remove(teamDTO);
            _dbContext.SaveChanges();
        }

        public async void RemoveAsync(int id)
        {
            var teamDTO = await _dbContext.FindAsync<TeamDTO>(id);
            _dbContext.Teams.Remove(teamDTO);
            await _dbContext.SaveChangesAsync();
        }
        public DOMAIN.Team Update(DOMAIN.Team team)
        {
            var teamDTO = _domainToDTOTranslator.Translate(team);
            _dbContext.Teams.Update(teamDTO);
            _dbContext.SaveChanges();

            return team;
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
