using BallClub.Domain.Models;

namespace BallClub.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Team Add(Team team);
    }
}