using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PartyManager : IPartyManager
{
    private readonly AppDbContext _context;

    public PartyManager(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Party>> GetAllPartiesAsync()
    {
        return await _context.Parties.Include(p => p.Invitations).ToListAsync();
    }

    public async Task<Party> GetPartyByIdAsync(int id)
    {
        return await _context.Parties.Include(p => p.Invitations)
            .FirstOrDefaultAsync(p => p.PartyId == id);
    }

    public async Task CreatePartyAsync(Party party)
    {
        _context.Parties.Add(party);
        await _context.SaveChangesAsync();
    }

    public async Task AddInvitationAsync(int partyId, Invitation invitation)
    {
        var party = await _context.Parties.FindAsync(partyId);
        if (party != null)
        {
            party.Invitations.Add(invitation);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SendInvitationsAsync(int partyId)
    {
        var party = await GetPartyByIdAsync(partyId);
        if (party != null)
        {
            foreach (var invitation in party.Invitations.Where(i => i.Status == InvitationStatus.InviteNotSent))
            {
                // Send email logic here...
                invitation.Status = InvitationStatus.InviteSent;
            }
            await _context.SaveChangesAsync();
        }
    }
}
