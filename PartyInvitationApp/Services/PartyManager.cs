using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Data;    
using PartyInvitationApp.Models;  
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using PartyInvitationApp.Models; 


namespace PartyInvitationApp.Services
{
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
            var party = await _context.Parties.Include(p => p.Invitations).FirstOrDefaultAsync(p => p.PartyId == partyId);

            if (party != null)
            {
                party.Invitations.Add(invitation);  
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Invitation> GetInvitationByIdAsync(int invitationId)
        {
            return await _context.Invitations.FindAsync(invitationId);
        }

        public async Task UpdateInvitationAsync(Invitation invitation)
        {
            _context.Invitations.Update(invitation);
            await _context.SaveChangesAsync();
        }


        public async Task UpdatePartyAsync(Party party)
        {
            _context.Parties.Update(party);
            await _context.SaveChangesAsync();
        }

        public async Task RespondToInvitationAsync(int invitationId, InvitationStatus response)
        {
            var invitation = await _context.Invitations.FindAsync(invitationId);
            if (invitation != null)
            {
                invitation.Status = response;
                await _context.SaveChangesAsync();
            }
        }


        public async Task SendInvitationsAsync(int partyId)
        {
            var party = await GetPartyByIdAsync(partyId);
            if (party != null)
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("your-email@gmail.com", "your-app-password"),
                    EnableSsl = true,
                };

                foreach (var invitation in party.Invitations.Where(i => i.Status == InvitationStatus.InviteNotSent))
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("your-email@gmail.com"),
                        Subject = "You're Invited!",
                        Body = $"Click here to respond: <a href='http://localhost:5000/Invitation/Respond/{invitation.InvitationId}'>Respond</a>",
                        IsBodyHtml = true,
                    };
                    mailMessage.To.Add(invitation.GuestEmail);

                    smtpClient.Send(mailMessage);
                    invitation.Status = InvitationStatus.InviteSent;
                }
                await _context.SaveChangesAsync();
            }
        }

    
    }
}
