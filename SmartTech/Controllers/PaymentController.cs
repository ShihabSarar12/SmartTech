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
        private readonly SmartTechEntities db = new SmartTechEntities();
        [HttpPost]
        public ActionResult Checkout(CartToOrder cartToOrder)
        {
            var cartWithImages = Session["cart_with_images"] as List<CartWithImages>;
            var user = Session["user"] as user;
            cartToOrder.OrderID += (" " + DateTime.Now.ToString("HH:mm:ss tt"));
            cartToOrder.OrderID.PadRight(64, '0');
            foreach (var cart in cartWithImages)
            {
                order_products order_Product = new order_products(cart.ProductId, cart.Price, cart.Quantity, cartToOrder.OrderID);
                db.order_products.Add(order_Product);
                db.SaveChanges();
            }
            string details = user.name + " " + user.number + " " + cartToOrder.Address;
            shipping shipProduct = new shipping(details, cartToOrder.TotalPrice);
            db.shippings.Add(shipProduct);
            db.SaveChanges();
            order orderProducts = new order(cartToOrder.Address, shipProduct.id, cartToOrder.TotalPrice, "shipping", user.id, cartToOrder.OrderID);
            db.orders.Add(orderProducts);
            db.SaveChanges();
            foreach(var cart in cartWithImages)
            {
                cart cartProduct = db.carts.Find(cart.CartId);
                db.carts.Remove(cartProduct);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}