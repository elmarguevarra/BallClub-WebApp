using BallClub.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BallClub.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Team Add(Team team);
        Team Find(int id);
        List<Team> GetAll();
        void Remove(int id);
        Team Update(Team team);
    }
}