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

        [HttpPost]
        public ActionResult AddToCart(long qnt)
        {
            var product = Session["product"] as product;
            var user = Session["user"] as user;
            cart cart = new cart(product.id, user.id, qnt, product.price);
            db.carts.Add(cart);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = Session["product_id"] });
        }
        // GET: Products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.product_photos).ToList();
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["product_id"] = id;
            Session["product"] = db.products.Find(id);
            var product = db.products
                .Where(p => p.id == id)
                .Select(p => new ProductWithImages
                {
                    Product = p,
                    ProductPhotos = p.product_photos.Select(pp => pp.image).ToList()
                })
                .FirstOrDefault();
            
            System.Diagnostics.Debug.WriteLine(product);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,category_id,name,slugs,short_description,description,discount,price,link,stock_status,featured,popular,status,seo_title,seo_description,seo_tags,created_at,updated_at")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,category_id,name,slugs,short_description,description,discount,price,link,stock_status,featured,popular,status,seo_title,seo_description,seo_tags,created_at,updated_at")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
