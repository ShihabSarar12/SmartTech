using SmartTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartTech.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        [HttpPost]
        public ActionResult Checkout(CartToOrder cartToOrder)
        {
            System.Diagnostics.Debug.WriteLine(cartToOrder.OrderID);
            System.Diagnostics.Debug.WriteLine(cartToOrder.TotalPrice);
            System.Diagnostics.Debug.WriteLine(cartToOrder.Address);
            System.Diagnostics.Debug.WriteLine(cartToOrder.ProductToOrders.Count);
            return RedirectToAction("Index", "Home");
        }
    }
}