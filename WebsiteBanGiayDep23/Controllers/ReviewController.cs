using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web.Mvc;
using WebsiteBanGiayDep23.Models;
using WebsiteBanGiayDep23.Models.EF;

namespace WebsiteBanGiayDep23.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Partial_Review(int? productId)
        {
            ViewBag.ProductID = productId;
            var item = new ReviewProduct();
            if (User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindByName(User.Identity.Name);
                if (user != null)
                {
                    item.Email = user.Email;
                    item.UserName = user.UserName;
                    item.FullName = user.FullName;
                }
                return PartialView(item);
            }
            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult Partial_Load_Review(int productId)
        {
            var item = db.Reviews.Where(x => x.ProductId == productId).OrderByDescending(x => x.Id).ToList();
            ViewBag.Count = item.Count;
            return PartialView(item);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostReview(ReviewProduct req)
        {
            if (ModelState.IsValid)
            {
                req.CreatedDate = DateTime.Now;
                db.Reviews.Add(req);
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}