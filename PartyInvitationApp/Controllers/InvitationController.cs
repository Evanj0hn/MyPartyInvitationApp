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
            var invitation = await _partyManager.GetInvitationByIdAsync(id);  // FIXED: Get the invitation, not the party
            if (invitation == null)
            {
                return NotFound();
            }
            return View(invitation);
        }

        // POST: /Invitation/Respond
        [HttpPost]
        public async Task<IActionResult> Respond(int invitationId, string response)
        {
            var invitation = await _partyManager.GetInvitationByIdAsync(invitationId);  // FIXED
            if (invitation == null)
            {
                return NotFound();
            }

            if (response == "Accept")
            {
                invitation.Status = InvitationStatus.Accepted;  // Matching UI response
            }
            else if (response == "Decline")
            {
                invitation.Status = InvitationStatus.Declined;
            }

            await _partyManager.UpdateInvitationAsync(invitation);  // Save the response
            return RedirectToAction("Details", "Party", new { id = invitation.PartyId });
        }
    }
}
