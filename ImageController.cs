using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadImagesInMVC.Models;

namespace UploadImagesInMVC.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Image ImageModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(ImageModel.ImageFile.FileName);
            string extension = Path.GetExtension(ImageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            ImageModel.ImagePath = "~/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
            ImageModel.ImageFile.SaveAs(fileName);
            using (Entities db = new Entities())
            {
                db.Images.Add(ImageModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }
        public ActionResult View(int Id)
        {
            Image imageModel = new Image();
            using(Entities db = new Entities())
            {
                imageModel = db.Images.Where(x => x.ImageId == Id).FirstOrDefault();
            }
            return View(imageModel);
        }
    }
}