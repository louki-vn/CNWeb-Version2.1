using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebShop.Common;
using WebShop.Models;

namespace WebShop.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        Shop db = new Shop();
        // GET: Admin/Home
        [HasCredential(RoleID = "VIEW_HOME_ADMIN")]
        public ActionResult Index()
        {
            ViewBag.user_logined = Session["user_logined"];
            ViewBag.is_logined = Session["is_logined"];

            var mem = db.MEMBERs.ToList();
            var order = db.TRANSACTIONs.ToList();
            var item = db.ITEM_SOLD.ToList();
            //  Số lượng thành viên
            ViewBag.Member_count = mem.Count();
            //  Tổng số đơn hàng
            ViewBag.Order_count = order.Count();
            int qty = 0, total = 0;
            foreach(var o in order)
            {
                total += o.amount;
                
            }
            foreach (var o in item)
            {
                qty += o.qty;

            }
            //  Số sản phẩm đã bán và tổng doanh thu
            ViewBag.Amount = qty;
            ViewBag.Total = total;

            var topproduct = db.PRODUCTs.SqlQuery("exec SelectTopProduct").ToList();
            ViewBag.TopProduct = topproduct;

            var category = db.CATEGORies.ToArray();
            Dictionary<int, string> p = new Dictionary<int, string>();
            foreach (var c in category)
            {
                p.Add(c.category_id, c.name);
            }
            ViewBag.Category = p;

            var topmem = db.Database.SqlQuery<Mem_Cart>("exec SelectTopMember").ToList();
            ViewBag.TopMem = topmem;
            return View();
        }

    }
}