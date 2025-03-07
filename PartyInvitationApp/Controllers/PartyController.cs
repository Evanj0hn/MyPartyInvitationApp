using Microsoft.AspNetCore.Mvc;
using PartyInvitationApp.Models;
using PartyInvitationApp.Services;
using System.Threading.Tasks;

namespace PartyInvitationApp.Controllers
{
    public class PartyController : Controller
    {
        private readonly IPartyManager _partyManager;

        public PartyController(IPartyManager partyManager)
        {
            _partyManager = partyManager;
        }

        // GET: /Party/
        public async Task<IActionResult> Index()
        {
            var parties = await _partyManager.GetAllPartiesAsync();
            return View(parties);
        }

        // GET: /Party/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Party/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Party party)
        {
            if (ModelState.IsValid)
            {
                await _partyManager.CreatePartyAsync(party);
                return RedirectToAction(nameof(Index));
            }
            return View(party);
        }

        // GET: /Party/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var party = await _partyManager.GetPartyByIdAsync(id);
            if (party == null)
            {
                return NotFound();
            }
            return View(party);
        }
    }
}
