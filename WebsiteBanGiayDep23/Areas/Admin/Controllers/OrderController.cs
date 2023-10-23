﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebsiteBanGiayDep23.Models;
using WebsiteBanGiayDep23.Models.EF;
using WebsiteBanGiayDep23.Models.ViewModels;

namespace WebsiteBanGiayDep23.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(string SearchText, int? page)
        {
            IEnumerable<Order> items = db.Orders.OrderByDescending(x => x.CreatedDate);
            //var items = db.Orders.OrderByDescending(x => x.CreatedDate).ToList();

            if (page == null)
            {
                page = 1;
            }
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.CustomerName.Contains(SearchText) || x.Code.Contains(SearchText) || x.Address.Contains(SearchText));
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }



        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }

        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = trangthai;
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Thành công", Success = true });
            }
            return Json(new { message = "Thất bại", Success = false });
        }

        public void ThongKe(string fromDate, string toDate)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails on o.ID equals od.OrderId
                        join p in db.Products on od.ProductId equals p.ID
                        select new
                        {
                            CreatedDate = o.CreatedDate,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = p.Price
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime start = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.CreatedDate >= start);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.CreatedDate < endDate);
            }
            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(r => new
            {
                Date = r.Key.Value,
                TotalBuy = r.Sum(x => x.OriginalPrice * x.Quantity), // tổng giá bán
                TotalSell = r.Sum(x => x.Price * x.Quantity) // tổng giá mua
            }).Select(x => new RevenueStatisticViewModel
            {
                Date = x.Date,
                Benefit = x.TotalSell - x.TotalBuy,
                Revenues = x.TotalSell
            });
        }

    }
}