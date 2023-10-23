using System.Web.Mvc;

namespace WebsiteBanGiayDep23.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}