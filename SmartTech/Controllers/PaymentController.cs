using SmartTech.Models;
using SmartTech.Utilities;
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
            if(cartWithImages.Count == 0)
            {
                Session["cart_empty"] = "The Cart is Empty!";
                return RedirectToAction("Index", "Home");
            }
            Session["cart_empty"] = null;
            var user = Session["user"] as user;
            string orderID;
            while (true)
            {
                orderID = KeyGenerator.GetUniqueKey(10);
                orderID += (DateTime.Now.ToString(" yyyy-MM-dd") + " | " + DateTime.Now.ToString("HH:mm:ss tt"));
                var query = from order in db.orders
                            where order.order_id == orderID
                            select order;
                var orderCheck = query.ToList();
                if (orderCheck.Count == 0)
                    break;
            }
            foreach (var cart in cartWithImages)
            {
                order_products order_Product = new order_products(cart.ProductId, cart.Price, cart.Quantity, orderID);
                db.order_products.Add(order_Product);
                db.SaveChanges();
            }
            string details = user.name + " " + user.number + " " + cartToOrder.Address;
            shipping shipProduct = new shipping(details, cartToOrder.TotalPrice);
            db.shippings.Add(shipProduct);
            db.SaveChanges();
            order orderProducts = new order(cartToOrder.Address, shipProduct.id, cartToOrder.TotalPrice, "shipping", user.id, orderID);
            db.orders.Add(orderProducts);
            db.SaveChanges();
            foreach (var cart in cartWithImages)
            {
                cart cartProduct = db.carts.Find(cart.CartId);
                db.carts.Remove(cartProduct);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}