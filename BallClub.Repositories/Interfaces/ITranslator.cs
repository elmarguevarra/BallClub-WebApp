using BallClub.Domain.Models;
using BallClub.Repositories.Messages;

namespace BallClub.Repositories.Interfaces
{
    public interface ITranslator<Incoming, Outgoing>
        where Incoming : class
        where Outgoing : class
    {
        Outgoing Translate(Incoming team);
    }
}