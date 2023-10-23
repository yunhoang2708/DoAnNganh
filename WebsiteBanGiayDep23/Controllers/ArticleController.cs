using System.Linq;
using System.Web.Mvc;
using WebsiteBanGiayDep23.Models;

namespace WebsiteBanGiayDep23.Controllers
{
    public class ArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Article
        public ActionResult Index(string alias)
        {
            var item = db.Posts.FirstOrDefault(x => x.Alias == alias);
            return View(item);
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}