using SmartTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.User = Session["user"];
            return View();
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
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

        // GET: Profile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
