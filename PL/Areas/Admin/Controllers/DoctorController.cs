using Microsoft.AspNetCore.Mvc;

namespace PL.Areas.Admin.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
