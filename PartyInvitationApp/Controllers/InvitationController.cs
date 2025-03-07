using Microsoft.AspNetCore.Mvc;
using PartyInvitationApp.Models;
using PartyInvitationApp.Services;
using System.Threading.Tasks;

namespace PartyInvitationApp.Controllers
{
    public class InvitationController : Controller
    {
        private readonly IPartyManager _partyManager;

        public InvitationController(IPartyManager partyManager)
        {
            _partyManager = partyManager;
        }

        // GET: /Invitation/Respond/{id}
        public async Task<IActionResult> Respond(int id)
        {
            var party = await _partyManager.GetPartyByIdAsync(id);
            if (party == null)
            {
                return NotFound();
            }
            return View(party);
        }

        // POST: /Invitation/Respond/{id}
        [HttpPost]
        public async Task<IActionResult> Respond(int id, string response)
        {
            var party = await _partyManager.GetPartyByIdAsync(id);
            if (party == null)
            {
                return NotFound();
            }

            var invitation = party.Invitations.Find(i => i.InvitationId == id);
            if (invitation == null)
            {
                return NotFound();
            }

            if (response == "yes")
            {
                invitation.Status = InvitationStatus.RespondedYes;
            }
            else if (response == "no")
            {
                invitation.Status = InvitationStatus.RespondedNo;
            }

            await _partyManager.SendInvitationsAsync(id);
            return RedirectToAction("Details", "Party", new { id = invitation.PartyId });
        }
    }
}
