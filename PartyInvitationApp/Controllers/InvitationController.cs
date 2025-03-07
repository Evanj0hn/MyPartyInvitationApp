using Microsoft.AspNetCore.Mvc;

namespace PartyInvitationApp.Controllers
{
    public class InvitationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
