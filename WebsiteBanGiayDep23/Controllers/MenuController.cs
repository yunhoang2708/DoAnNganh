using System.Linq;
using System.Web.Mvc;
using WebsiteBanGiayDep23.Models;

namespace WebsiteBanGiayDep23.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuTop()
        {
            var items = db.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);
        }
        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCatgories.ToList();
            return PartialView("_MenuProductCategory", items);
        }
        public ActionResult MenuArrivals()
        {
            var items = db.ProductCatgories.ToList();
            return PartialView("_MenuArrivals", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = db.ProductCatgories.ToList();
            return PartialView("_MenuLeft", items);
        }
    }
}