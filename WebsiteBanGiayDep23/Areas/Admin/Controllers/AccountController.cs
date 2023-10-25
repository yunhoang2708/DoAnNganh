using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebsiteBanGiayDep23.Models;

namespace WebsiteBanGiayDep23.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/Account
        public ActionResult Index()
        {
            var ítems = db.Users.ToList();
            return View(ítems);
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Edit(int id)
        {
            var item = db.Users.Find(id);
            return View(item);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(model);
                db.Entry(model).Property(x => x.UserName).IsModified = true;
                db.Entry(model).Property(x => x.Email).IsModified = true;
                db.Entry(model).Property(x => x.FullName).IsModified = true;
                db.Entry(model).Property(x => x.Phone).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string username)
        {
            var item = db.Users.Find(username);
            if (item != null)
            {
                db.Users.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    Phone = model.Phone
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.Roles != null) // * Nếu có chọn role
                    {
                        foreach (var item in model.Roles)
                        {
                            await UserManager.AddToRoleAsync(user.Id, item);
                        }
                    }
                    return RedirectToAction("Index", "Account"); // * Chuyển về trang Index
                }
                AddErrors(result);
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var item = UserManager.FindById(id);
            var newUser = new EditAccountViewModel();
            if (item != null)
            {
                var rolesForUser = UserManager.GetRoles(id);
                var roles = new List<string>();
                if (rolesForUser != null && rolesForUser.Count() > 0)
                {
                    foreach (var role in rolesForUser)
                    {
                        roles.Add(role);
                    }
                }
                newUser.UserName = item.UserName;
                newUser.Email = item.Email;
                newUser.FullName = item.FullName;
                newUser.Phone = item.Phone;
                newUser.Roles = roles;
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View(newUser);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByName(model.UserName);
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.Phone = model.Phone;
                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var rolesForUser = UserManager.GetRoles(user.Id);
                    if (model.Roles != null) // * Nếu có chọn role
                    {
                        foreach (var item in model.Roles)
                        {
                            var checkRole = rolesForUser.FirstOrDefault(x => x.Equals(item));
                            if (checkRole == null) // * Nếu user chưa có role này thì thêm vào
                            {
                                await UserManager.AddToRoleAsync(user.Id, item);
                            }
                        }
                    }
                    return RedirectToAction("Index", "Account"); // * Chuyển về trang Index
                }
                AddErrors(result);
            }
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAccount(string id, string user)
        {
            var code = new { Success = false }; // * Mặc định xóa không thành công 
            // * Tìm user theo username
            var item = UserManager.FindByName(user);
            if (item != null)
            {
                // * Lấy danh sách role của user
                var rolesForUser = UserManager.GetRoles(id);
                if (rolesForUser != null && rolesForUser.Count() > 0)
                {
                    //TODO: Xóa user khỏi các role
                    foreach (var role in rolesForUser)
                    {
                        await UserManager.RemoveFromRoleAsync(id, role);
                    }
                }

                var res = await UserManager.DeleteAsync(item);
                code = new { Success = res.Succeeded };
            }
            return Json(code);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}