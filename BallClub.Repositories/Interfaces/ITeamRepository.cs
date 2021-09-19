using BallClub.Domain.Models;
using System.Threading.Tasks;

namespace BallClub.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<Team> AddAsync(Team team);
    }
}