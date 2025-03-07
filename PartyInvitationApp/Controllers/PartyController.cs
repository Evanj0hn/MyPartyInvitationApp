using Microsoft.AspNetCore.Mvc;

namespace PartyInvitationApp.Controllers
{
    public class PartyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
