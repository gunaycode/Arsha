using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace Arsha.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
