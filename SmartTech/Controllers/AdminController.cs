using SmartTech.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartTech.Controllers
{
    public class AdminController : Controller
    {
        private SmartTechEntities db = new SmartTechEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //=================Banner Start===========================

        public ActionResult Banner()
        {
            return View(db.banners.ToList());
        }

        [HttpGet]
        public ActionResult AddBanner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBanner(banner banner, HttpPostedFileBase bannerImage)
        {
            string[] validImageTypes = { "image/jpeg", "image/jpg", "image/png" };

            if (bannerImage == null || !validImageTypes.Contains(bannerImage.ContentType))
            {
                ModelState.AddModelError("banner_image", "Please select a valid image file (jpg, jpeg, png).");
            }

            if (ModelState.IsValid)
            {
                if (bannerImage != null)
                {
                    string path = Server.MapPath("~/Content/uploads/frontend/banners/" + bannerImage.FileName);
                    bannerImage.SaveAs(path);
                    banner.banner_image = bannerImage.FileName;
                }
                db.banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Banner");
            }
            return View(banner);
        }

        [HttpGet]
        public ActionResult EditBanner(long id)
        {
            var banner = db.banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        [HttpPost]
        public ActionResult EditBanner(banner banner, HttpPostedFileBase bannerImage)
        {
            System.Diagnostics.Debug.WriteLine("Category: " + banner.banner_category);
            System.Diagnostics.Debug.WriteLine("Title: " + banner.banner_title);
            System.Diagnostics.Debug.WriteLine("Description: " + banner.banner_description);

            if (ModelState.IsValid)
            {
                var existingBanner = db.banners.Find(banner.id);
                if (existingBanner == null)
                {
                    return HttpNotFound();
                }

                existingBanner.banner_category = banner.banner_category;
                existingBanner.banner_title = banner.banner_title;
                existingBanner.banner_description = banner.banner_description;

                if (bannerImage != null && bannerImage.ContentLength > 0)
                {
                    var oldImagePath = Server.MapPath("~/Content/uploads/frontend/banners/" + existingBanner.banner_image);
                    if (System.IO.File.Exists(oldImagePath) && !string.IsNullOrEmpty(existingBanner.banner_image))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    string newImageFileName = System.IO.Path.GetFileName(bannerImage.FileName);
                    string newImagePath = Server.MapPath("~/Content/uploads/frontend/banners/" + newImageFileName);
                    bannerImage.SaveAs(newImagePath);
                    existingBanner.banner_image = newImageFileName;
                }
                db.Entry(existingBanner).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Banner");
            }
            return View(banner);
        }

        public ActionResult DeleteBanner(int id)
        {
            var banner = db.banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            db.banners.Remove(banner);
            db.SaveChanges();
            return RedirectToAction("Banner");
        }

        //=================Banner End===========================

        //=================Shipping Start===========================

        public ActionResult Shipping()
        {
            var shippings = db.shippings.ToList();
            return View(shippings);
        }

        [HttpGet]
        public ActionResult AddShipping()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddShipping(shipping shipping)
        {
            if (ModelState.IsValid)
            {
                db.shippings.Add(shipping);
                db.SaveChanges();
                return RedirectToAction("Shipping");
            }
            return View();
        }


        public ActionResult DeleteShipping(long id)
        {
            var shipping = db.shippings.Find(id);
            if (shipping == null)
            {
                return HttpNotFound();
            }
            db.shippings.Remove(shipping);
            db.SaveChanges();
            return RedirectToAction("Shipping");
        }

        //=================Shipping End===========================

        //=================Category Start===========================

        public ActionResult Categories()
        {
            var categories = db.product_categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(product_categories categories, HttpPostedFileBase category_image)
        {
            // Validate Category Name
            if (string.IsNullOrWhiteSpace(categories.category_name))
            {
                ModelState.AddModelError("category_name", "Category name is required.");
            }

            // Validate Category Icon
            if (string.IsNullOrWhiteSpace(categories.category_icon))
            {
                ModelState.AddModelError("category_icon", "Category icon is required.");
            }

            // Validate Image
            string[] validImageTypes = { "image/jpeg", "image/jpg", "image/png" };
            if (category_image == null || !validImageTypes.Contains(category_image.ContentType))
            {
                ModelState.AddModelError("category_image", "Please select a valid image file (jpg, jpeg, png).");
            }

            if (ModelState.IsValid)
            {
                // Save image if provided
                if (category_image != null)
                {
                    string path = Server.MapPath("~/Content/uploads/frontend/categoryImages/" + category_image.FileName);
                    category_image.SaveAs(path);
                    categories.category_image = category_image.FileName;
                }

                // Add category to the database
                db.product_categories.Add(categories);
                db.SaveChanges();
                return RedirectToAction("Categories");
            }

            return View(categories);
        }




        public ActionResult DeleteCategory(long id) // Change from DeleteCategories to DeleteCategory
        {
            var categories = db.product_categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            db.product_categories.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("Categories"); // Redirect to Categories instead of AddCategory
        }



        //=================Products Start===========================

        [HttpGet]
        public ActionResult AddProducts()
        {
            var categories = db.product_categories.ToList();
            ViewBag.CategoryList = new SelectList(categories, "id", "category_name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProducts(product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("AddProducts");
            }

            ViewBag.CategoryList = new SelectList(db.product_categories, "id", "category_name", product.category_id);

            return View(product);
        }

        public ActionResult ViewProducts()
        {
            var products = db.products.Include("product_categories").ToList();

            return View(db.products.ToList());
        }

        public ActionResult Photos(long id)
        {
            var productPhotos = db.product_photos.Where(p => p.product_id == id).ToList();
            ViewBag.ProductId = id; // Pass the product ID to the view
            return View(productPhotos);
        }


        [HttpPost]
        public ActionResult AddPhoto(long productId, HttpPostedFileBase productImage)
        {
            if (productImage != null && productImage.ContentLength > 0)
            {
                var fileName = Path.GetFileName(productImage.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/uploads/frontend/productImages"), fileName);

                productImage.SaveAs(path);

                var productPhoto = new product_photos
                {
                    product_id = productId,
                    image = fileName
                };

                db.product_photos.Add(productPhoto);
                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "Please select a valid image file.");
            }


            return RedirectToAction("Photos", new { id = productId });
        }


        [HttpGet]
        public ActionResult DeletePhoto(long id)
        {
            var photo = db.product_photos.FirstOrDefault(p => p.id == id);

            if (photo == null)
            {
                return HttpNotFound();
            }

            var filePath = Server.MapPath("~/Content/uploads/frontend/productImages/" + photo.image);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            db.product_photos.Remove(photo);
            db.SaveChanges();

            return RedirectToAction("Photos", new { id = photo.product_id });
        }




        [HttpGet]
        public ActionResult EditProduct(long id)
        {

            var product = db.products.FirstOrDefault(p => p.id == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryList = new SelectList(db.product_categories, "id", "category_name", product.category_id);

            return View(product);
        }


        [HttpPost]
        public ActionResult EditProduct(product product)
        {
            var products = db.products.FirstOrDefault(p => p.id == product.id);

            if (products == null)
            {
                return HttpNotFound();
            }

            products.name = product.name;
            products.description = product.description;
            products.price = product.price;
            products.discount = product.discount;
            products.category_id = product.category_id;
            products.stock_status = product.stock_status;
            products.status = product.status;

            db.SaveChanges();

            return RedirectToAction("ViewProducts");

        }



        public ActionResult DeleteProduct(long id)
        {

            var product = db.products.FirstOrDefault(p => p.id == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            db.products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("ViewProducts");

        }

        //=================Products End===========================


        //=================Orders Start===========================


        public ActionResult Orders()
        {

            var orders = db.orders.Include(o => o.shipping).ToList();
            return View(orders);
        }
        public ActionResult ViewOrder(long id)
        {

            var viewOrders = db.order_products.Where(o => o.order_id.Equals(id));
            ViewBag.viewOrderId = id;
            return View("ViewOrder");
        }


        //=================Orders End===========================

    }
}
