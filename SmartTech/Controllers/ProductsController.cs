﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartTech.Models;

namespace SmartTech.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SmartTechEntities db = new SmartTechEntities();

        public ActionResult DeleteCart(long id)
        {
            var cartItem = db.carts.FirstOrDefault(c => c.id == id);
            db.carts.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Search(string SearchQuery)
        {
            Session["search_query"] = SearchQuery;
            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        public ActionResult AddToCart(long qnt)
        {
            if (Session["user"] != null)
            {
                var product = Session["product"] as product;
                var user = Session["user"] as user;
                cart cart = new cart(product.id, user.id, qnt, product.price);
                db.carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = Session["product_id"] });
            }
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Index(string sortOption, decimal? minPrice, decimal? maxPrice, string category)
        {
            var SearchQuery = Session["search_query"] as string;
            var user = Session["user"] as user;
            if (user != null)
            {
                var query = from cart in db.carts
                            where cart.user_id == user.id
                            join product in db.products on cart.product_id equals product.id
                            join photo in db.product_photos on product.id equals photo.product_id into photos
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

            IQueryable<product> productsQuery = db.products.Include(p => p.product_photos);

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                string lowerCaseQuery = SearchQuery.ToLower();
                productsQuery = productsQuery.Where(p => p.name.ToLower().Contains(lowerCaseQuery));
            }

            if (minPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.price <= maxPrice.Value);
            }

            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery.Where(p => p.product_categories.category_name == category);
            }

            switch (sortOption)
            {
                case "2":
                    productsQuery = productsQuery.OrderBy(p => p.price);
                    break;
                case "3":
                    productsQuery = productsQuery.OrderByDescending(p => p.price);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.name);
                    break;
            }

            List<product> products = productsQuery.ToList();
            Session["search_query"] = null;
            List<product_categories> categories = db.product_categories.ToList();
            Session["categories"] = categories;
            return View(products);
        }

        public ActionResult Details(long? id)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["product_id"] = id;
            Session["product"] = db.products.Find(id);
            var products = db.products
                .Where(p => p.id == id)
                .Select(p => new ProductWithImages
                {
                    Product = p,
                    ProductPhotos = p.product_photos.Select(pp => pp.image).ToList()
                })
                .FirstOrDefault();
            
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }
    }
}
