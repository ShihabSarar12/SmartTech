using SmartTech.Models;
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
            if (ModelState.IsValid)
            {
                if (bannerImage != null)
                {
                    string path = Server.MapPath("~/Content/uploads/frontend/banners/" + bannerImage.FileName);
                    bannerImage.SaveAs(path);
                    banner.banner_image = bannerImage.FileName;
                }

                /*db.Entry(banner).State = EntityState.Modified;
                db.banners.Add(banner);*/
                db.banners.
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

        public ActionResult Shipping()
        {
            return View();
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
