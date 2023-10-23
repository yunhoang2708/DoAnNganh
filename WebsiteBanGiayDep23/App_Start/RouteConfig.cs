using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteBanGiayDep23
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Products",
                url: "san-pham",
                defaults: new { controller = "Products", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
            );

            routes.MapRoute(
                name: "CategoryProduct",
                url: "danh-muc-san-pham/{alias}-{id}",
                defaults: new { controller = "Products", action = "ProductCategory", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
            );

            routes.MapRoute(
                name: "detailProduct",
                url: "chi-tiet/{alias}-p{id}",
                defaults: new { controller = "Products", action = "Detail", alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
            );

            routes.MapRoute(
                name: "BaiViet",
                url: "post/{alias}",
                defaults: new { controller = "Article", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteBanHang23.Controllers" }
            );

            routes.MapRoute(
                 name: "Checkout",
                 url: "thanh-toan",
                 defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
                 namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
            );

            routes.MapRoute(
                 name: "vnpay_return",
                 url: "vnpay_return",
                 defaults: new { controller = "ShoppingCart", action = "VnpayReturn", alias = UrlParameter.Optional },
                 namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
            );

            routes.MapRoute(
                 name: "ShoppingCart",
                 url: "gio-hang",
                 defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
                 namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
            );

            routes.MapRoute(
                name: "DetailNew",
                url: "{alias}-n{id}",
                defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
            );

            routes.MapRoute(
                name: "NewsList",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
            );

            routes.MapRoute(
               name: "ChatGPT",
               url: "chat-bot",
               defaults: new { controller = "ChatGPT", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
           );

            routes.MapRoute(
               name: "Login",
               url: "login",
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
           );

            routes.MapRoute(
               name: "Register",
               url: "register",
               defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
               namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteBanGiayDep23.Controllers" }
            );

        }
    }
}
