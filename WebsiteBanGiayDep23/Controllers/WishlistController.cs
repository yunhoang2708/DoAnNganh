using System;
using System.Web.Mvc;
using WebsiteBanGiayDep23.Models;
using WebsiteBanGiayDep23.Models.EF;

namespace WebsiteBanGiayDep23.Controllers
{
    public class WishlistController : Controller
    {
        // GET: Wishlist
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Wishlist> items = db.Wishlist.Where(x => x.UserName == User.Identity.Name).OrderByDescending(x => x.CreatedDate).ToPagedList(page.Value, pageSize);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.pageSize = pageSize;
            ViewBag.page = page;
            return View(items);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostWishlist (int ProductId)
        {
            if (Request.IsAuthenticated == false)
            {
                return Json(new { Success = false, Message = "Bạn chưa đăng nhập!" });
            }
            var checkItem = db.Wishlist.FirstOrDefault(x => x.ProductId == ProductId && x.UserName == User.Identity.Name);
            if (checkItem != null)
            {
                return Json(new { Success = false, Message ="Sản phẩm có trong danh sách yêu thích"})
            }
            var item = new Wishlist();
            item.ProductId = ProductId;
            item.UserName = User.Identity.Name;
            item.CreatedDate = DateTime.Now;
            db.Wishlists.Add(item);
            db.SaveChanges();
            return Json(new { Success = true });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult DeleteWishlist(int ProductId)
        {
            var item = db.Wishlist.FirstOrDefault(x => x.ProductId == ProductId && x.UserName == User.Identity.Name);
            if (item != null)
            {
                db.Wishlist.Remove(item);
                db.SaveChanges();
                return Json(new { Success = true, Message = "Xóa thành công" });
            }
            return Json(new { Success = false, Message = "Xóa thất bại" });
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}