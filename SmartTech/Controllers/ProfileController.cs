using SmartTech.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmartTech.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SmartTechEntities db = new SmartTechEntities();
        // GET: Profile
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = Session["user"] as user;
            if (user != null)
            {
                var query = from cart in db.carts
                            where cart.user_id == user.id
                            join product in db.products
                            on cart.product_id equals product.id
                            join photo in db.product_photos
                            on product.id equals photo.product_id into photos
                            from photo in photos
                            .GroupBy(p => p.product_id)
                            .Select(g => g.FirstOrDefault())
                            .DefaultIfEmpty()
                            select new CartWithImages
                            {
                                CartId = cart.id,
                                Quantity = cart.qnt,
                                ProductId = product.id,
                                Name = product.name,
                                Price = product.price,
                                Image = photo != null ? photo.image : null
                            };

                var cartWithImages = query.ToList();
                Session["cart_with_images"] = cartWithImages;
            }
            else
            {
                Session["user"] = null;
                Session["cart_with_images"] = null;
            }
            ViewBag.User = Session["user"];
            return View();
        }
        public ActionResult Wishlist()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.User = Session["user"];
            return View();
        }
        public ActionResult Orders()
        {
            var user = Session["user"] as user;
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.User = user;
            var ordersWithProducts = from order in db.orders
                                     where order.user_id == user.id
                                     join order_product in db.order_products on order.order_id equals order_product.order_id
                                     join product in db.products on order_product.product_id equals product.id
                                     join product_photo in db.product_photos on product.id equals product_photo.product_id into productPhotosGroup
                                     from productPhoto in productPhotosGroup
                                         .GroupBy(p => p.product_id)
                                         .Select(g => g.FirstOrDefault())
                                         .DefaultIfEmpty()
                                     group new { order, product, productPhoto, order_product } by order.order_id into grouped
                                     select new OrderedItems
                                     {
                                         OrderID = grouped.Key.Length > 10 ? grouped.Key.Substring(0, 10) : grouped.Key,
                                         OrderTime = grouped.Key.Length > 10 ? grouped.Key.Substring(10) : string.Empty,
                                         OrderStatus = grouped.Select(g => g.order.status).FirstOrDefault(),
                                         OrderPrice = grouped.Select(g => g.order.price).FirstOrDefault(),
                                         Products = grouped
                                             .Select(g => new OrderedProductWithImages
                                             {
                                                 ProductName = g.product.name,
                                                 ProductPrice = g.product.price,
                                                 ProductImage = g.productPhoto != null ? g.productPhoto.image : null,
                                                 Quantity = g.order_product.qnt
                                             })
                                             .ToList()
                                     };

            var groupedOrders = ordersWithProducts.ToList();
            if (groupedOrders.Count == 0)
                ViewBag.EmptyMsg = "You haven't ordered yet!";


            return View(groupedOrders);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Edit(user user)
        {
            var prevUser = Session["user"] as user;
            if (prevUser == null)
                return RedirectToAction("Login", "Home");
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            string relativePath = "";
            if (user.uploaded_file != null && user.uploaded_file.ContentLength > 0)
            {
                var targetFolder = Server.MapPath("~/Content/frontend/img/user");
                if (!Directory.Exists(targetFolder))
                    Directory.CreateDirectory(targetFolder);

                var fileName = Path.GetFileNameWithoutExtension(user.uploaded_file.FileName);
                var fileExtension = Path.GetExtension(user.uploaded_file.FileName);
                var uniqueFileName = $"{fileName}_{DateTime.Now:yyyyMMddHHmmssfff}{fileExtension}";

                var fullPath = Path.Combine(targetFolder, uniqueFileName);
                relativePath = $"~/Content/frontend/img/user/{uniqueFileName}";

                user.uploaded_file.SaveAs(fullPath);

                ViewBag.Message = "File uploaded successfully.";
            }
            else
                ViewBag.Message = "No file selected.";
            
            var checkUser = db.users.Find(prevUser.id);
            if (checkUser == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (!string.IsNullOrEmpty(user?.confirm_password))
            {
                if (!user.confirm_password.Equals(checkUser.password))
                {
                    Session["match"] = "Current Password does not match!";
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    Session["match"] = null;
                }
            }

            checkUser.email = string.IsNullOrEmpty(user.email) ? checkUser.email : user.email;
            checkUser.password = string.IsNullOrEmpty(user?.password) ? checkUser.password : user.password;
            checkUser.number = string.IsNullOrEmpty(user.number) ? checkUser.number : user.number;
            checkUser.name = string.IsNullOrEmpty(user.name) ? checkUser.name : user.name;
            checkUser.image_url = string.IsNullOrWhiteSpace(relativePath) ? checkUser.image_url : relativePath;
            db.SaveChanges();

            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
