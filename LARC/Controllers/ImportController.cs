using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LARC.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string ticker)
        {
            string fileName = System.IO.Path.GetFileName(file.FileName);
            System.IO.Directory.CreateDirectory(Server.MapPath("~/Temp"));
            file.SaveAs(Server.MapPath("~/Temp/" + fileName));

            var fund = Morningstar.Importer.FundFileImporter.Import(Server.MapPath("~/Temp/" + fileName), ticker);
            //TODO: Save this fund to the database!
            return RedirectToAction("Index", "Home");

        }
    }
}