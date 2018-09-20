using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AniBaseFinal.Models;

namespace AniBaseFinal.Controllers
{
    public class AnimeViewController : Controller
    {
        private AniBaseEntities db = new AniBaseEntities();

        // GET: /AnimeView/
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortPram = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.TypeSortPram = sortOrder == "TYPE" ? "type_desc" : "TYPE";
            var anime = from a in db.Animes
                        select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                anime = anime.Where(s => s.TITLE.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    anime = anime.OrderByDescending(a => a.TITLE);
                    break;
                case"TYPE":
                    anime = anime.OrderBy(a => a.TYPE);
                    break;
                case "type_desc":
                    anime = anime.OrderByDescending(a => a.TYPE);
                    break;
                default:
                    anime = anime.OrderBy(a => a.TITLE);
                    break;
            }
            return View(db.Animes.ToList());
        }

        // GET: /AnimeView/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return View(anime);
        }

        // GET: /AnimeView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AnimeView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,URL,IMGURL,TITLE,AIRING,DESCRIPTION,TYPE,EPISODES,SCORE,SDATE,EDATE")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                db.Animes.Add(anime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anime);
        }

        // GET: /AnimeView/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return View(anime);
        }

        // POST: /AnimeView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,URL,IMGURL,TITLE,AIRING,DESCRIPTION,TYPE,EPISODES,SCORE,SDATE,EDATE")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anime);
        }

        // GET: /AnimeView/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return View(anime);
        }

        // POST: /AnimeView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anime anime = db.Animes.Find(id);
            db.Animes.Remove(anime);
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
