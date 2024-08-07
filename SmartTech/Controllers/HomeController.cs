﻿using SmartTech.Models;
using SmartTech.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SmartTech.Controllers
{
    public class HomeController : Controller
    {
        private readonly SmartTechEntities db = new SmartTechEntities();
        
        public ActionResult Index()
        {
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
            var topCategoriesBySales = db.product_categories
                .Select(category => new TopCategories
                {
                    Category = category,
                    TotalSales = category.products
                    .SelectMany(p => p.order_products)
                    .Select(op => (decimal?)op.qnt * (decimal?)op.price)
                    .DefaultIfEmpty(0)
                    .Sum()
                })
                .OrderByDescending(x => x.TotalSales)
                .Take(5)
                .ToList();
            Session["top_categories"] = topCategoriesBySales;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signin_Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signin_Register(user user)
        {
            if (user.password != user.confirm_password) 
            {
                ViewBag.Error = "Password doesn\'t match!!";
                return View();
            }
            user.password = PasswordHasher.HashPassword(user.password);
            db.users.Add(user);
            db.SaveChanges();
            ViewBag.Error = "Signed up successfully";
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var user = db.users.FirstOrDefault(u => u.email.Equals(email));

            bool isValidUser = false;
            if (user != null)
                isValidUser = PasswordHasher.ValidatePassword(password, user.password);
            if (!isValidUser)
            {
                ViewBag.IsValid = "Credentials don't match!";
                return View();
            }
            Session["user"] = user;
            return RedirectToAction("Index");
        }


    }
}