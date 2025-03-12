using System.Collections.Generic;
using PartyInvitationApp.Services;
using System.Threading.Tasks;
using PartyInvitationApp.Models;  

namespace PartyInvitationApp.Services
{
    public interface IPartyManager
    {
        Task<List<Party>> GetAllPartiesAsync();
        Task<Party> GetPartyByIdAsync(int id);
        Task CreatePartyAsync(Party party);
        Task<Invitation> GetInvitationByIdAsync(int invitationId);
        Task UpdateInvitationAsync(Invitation invitation);
        Task AddInvitationAsync(int partyId, Invitation invitation);
        Task RespondToInvitationAsync(int invitationId, InvitationStatus response);
        Task SendInvitationsAsync(int partyId);
        Task UpdatePartyAsync(Party party);

    }
}
