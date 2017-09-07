using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LARC.Models;

namespace LARC.Controllers
{
    public class ClientDBsController : Controller
    {
        private LARC_DBEntities db = new LARC_DBEntities();

        // GET: ClientDBs
        public ActionResult Index()
        {
            var clientDBs = db.ClientDBs.Include(c => c.AccountDB).Include(c => c.PortfolioDB);
            return View(clientDBs.ToList());
        }

        // GET: ClientDBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDB clientDB = db.ClientDBs.Find(id);
            if (clientDB == null)
            {
                return HttpNotFound();
            }
            return View(clientDB);
        }

        // GET: ClientDBs/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.AccountDBs, "ID", "LastName");
            ViewBag.PortfolioID = new SelectList(db.PortfolioDBs, "ID", "Name");
            return View();
        }

        // POST: ClientDBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AccountID,PortfolioID")] ClientDB clientDB)
        {
            if (ModelState.IsValid)
            {
                db.ClientDBs.Add(clientDB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.AccountDBs, "ID", "LastName", clientDB.AccountID);
            ViewBag.PortfolioID = new SelectList(db.PortfolioDBs, "ID", "Name", clientDB.PortfolioID);
            return View(clientDB);
        }

        // GET: ClientDBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDB clientDB = db.ClientDBs.Find(id);
            if (clientDB == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.AccountDBs, "ID", "LastName", clientDB.AccountID);
            ViewBag.PortfolioID = new SelectList(db.PortfolioDBs, "ID", "Name", clientDB.PortfolioID);
            return View(clientDB);
        }

        // POST: ClientDBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AccountID,PortfolioID")] ClientDB clientDB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientDB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.AccountDBs, "ID", "LastName", clientDB.AccountID);
            ViewBag.PortfolioID = new SelectList(db.PortfolioDBs, "ID", "Name", clientDB.PortfolioID);
            return View(clientDB);
        }

        // GET: ClientDBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientDB clientDB = db.ClientDBs.Find(id);
            if (clientDB == null)
            {
                return HttpNotFound();
            }
            return View(clientDB);
        }

        // POST: ClientDBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientDB clientDB = db.ClientDBs.Find(id);
            db.ClientDBs.Remove(clientDB);
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
