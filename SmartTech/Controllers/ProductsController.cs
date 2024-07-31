using System;
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
            var product = Session["product"] as product;
            var user = Session["user"] as user;
            cart cart = new cart(product.id, user.id, qnt, product.price);
            db.carts.Add(cart);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = Session["product_id"] });
        }
        public ActionResult Index(string sortOption, decimal? minPrice, decimal? maxPrice)
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
            switch (sortOption)
            {
                case "2":
                    productsQuery = productsQuery.OrderBy(p => p.price);
                    break;
                case "3":
                    productsQuery = productsQuery.OrderByDescending(p => p.price);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.name); // Assuming default is sort by name
                    break;
            }
            List<product> products = productsQuery.ToList();
            Session["search_query"] = null;
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
