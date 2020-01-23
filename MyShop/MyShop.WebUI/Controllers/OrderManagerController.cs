using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderManagerController : Controller
    {
        IOrderService OrderService;
        public OrderManagerController(IOrderService orderservice)
        {
            this.OrderService = orderservice;
        }
        // GET: OrderManager
        public ActionResult Index()
        {
            List<Order> orders = OrderService.GetOrderList();

            return View(orders);
        }

        public ActionResult UpdateOrder(string Id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };
            Order order = OrderService.GetOrder(Id);

            return View(order);
        }
        [HttpPost]
        public ActionResult UpdateOrder(Order updateorder, string Id)
        {
            Order order = OrderService.GetOrder(Id);

            order.OrderStatus = updateorder.OrderStatus;
            OrderService.UpdateOrder(order);

            return RedirectToAction("Index");
        }
    }
}