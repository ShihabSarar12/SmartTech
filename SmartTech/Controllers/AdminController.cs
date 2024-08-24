using SmartTech.Models;
using System.Data.Entity;
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
            // Example of logging
            System.Diagnostics.Debug.WriteLine("Category: " + banner.banner_category);
            System.Diagnostics.Debug.WriteLine("Title: " + banner.banner_title);
            System.Diagnostics.Debug.WriteLine("Description: " + banner.banner_description);

            if (ModelState.IsValid)
            {
                // Fetch the existing banner from the database
                var existingBanner = db.banners.Find(banner.id);
                if (existingBanner == null)
                {
                    return HttpNotFound();
                }

                // Update the banner details
                existingBanner.banner_category = banner.banner_category;
                existingBanner.banner_title = banner.banner_title;
                existingBanner.banner_description = banner.banner_description;

                // Handle the image upload
                if (bannerImage != null && bannerImage.ContentLength > 0)
                {
                    // Delete the old image file if it exists
                    var oldImagePath = Server.MapPath("~/Content/uploads/frontend/banners/" + existingBanner.banner_image);
                    if (System.IO.File.Exists(oldImagePath) && !string.IsNullOrEmpty(existingBanner.banner_image))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    // Save the new image
                    string newImageFileName = System.IO.Path.GetFileName(bannerImage.FileName);
                    string newImagePath = Server.MapPath("~/Content/uploads/frontend/banners/" + newImageFileName);
                    bannerImage.SaveAs(newImagePath);
                    existingBanner.banner_image = newImageFileName;
                }

                // Mark the entity as modified and save changes
                db.Entry(existingBanner).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Banner");
            }

            // If we got this far, something failed; redisplay form
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

        public ActionResult AddCategories()
        {
            return View();
        }

        public ActionResult AddProducts()
        {
            return View();
        }
    }
}
