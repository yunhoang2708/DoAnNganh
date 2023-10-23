using System;
using System.Web.Mvc;
using WebsiteBanGiayDep23.Models;
using WebsiteBanGiayDep23.Models.EF;

namespace WebsiteBanGiayDep23.Controllers
{
    public class WishlistController : Controller
    {
        // GET: Wishlist
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostWishlist (int ProductId)
        {
            if (Request.IsAuthenticated == false)
            {
                return Json(new { Success = false, Message = "Bạn chưa đăng nhập!" });
            }
            else
            {
                var item = new Wishlist();
                item.ProductId = ProductId;
                item.UserName = User.Identity.Name;
                item.CreatedDate = DateTime.Now;
                db.Wishlists.Add(item);
                db.SaveChanges();
                return Json(new { Success = true });
            }
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}