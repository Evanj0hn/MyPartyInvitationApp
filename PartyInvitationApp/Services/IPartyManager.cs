namespace PartyInvitationApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPartyManager
{
    Task<List<Party>> GetAllPartiesAsync();
    Task<Party> GetPartyByIdAsync(int id);
    Task CreatePartyAsync(Party party);
    Task AddInvitationAsync(int partyId, Invitation invitation);
    Task SendInvitationsAsync(int partyId);
}
