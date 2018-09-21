using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AniBaseFinal.Models;
using PagedList;
using AniBaseFinal.CustomFilter;

namespace AniBaseFinal.Controllers
{
    public class AnimeViewController : Controller
    {
        AniBaseEntities anb;

        public AnimeViewController()
        {
            anb = new AniBaseEntities();
        }

        // GET: /AnimeView/
        public ActionResult Index(string searching, string currentFilter,string sortOn, string orderBy, string
pSortOn, int? page)
        {
            ViewBag.CurrentSort = sortOn;
             if (searching != null)
   {
      page = 1;
   }
   else
   {
      searching = currentFilter;
   }

   ViewBag.CurrentFilter = searching;

            if (!string.IsNullOrWhiteSpace(sortOn) && !sortOn.Equals(pSortOn,
StringComparison.CurrentCultureIgnoreCase))
            {
                orderBy = "asc";
            }

            var anime = from a in anb.Animes
                        select a;
            ViewBag.OrderBy = orderBy;
            ViewBag.SortOn = sortOn;
            ViewBag.Searching = searching;

            switch (sortOn)
            {
                case "TITLE":
                    if (orderBy.Equals("desc"))
                    {
                        anime = anime.OrderByDescending(a => a.TITLE);
                    }
                    else
                    {
                        anime = anime.OrderBy(a => a.TITLE);
                    }
                    break;
                    case "TYPE":
                    if (orderBy.Equals("desc"))
                    {
                         anime = anime.OrderByDescending(a => a.TYPE);
                    }
                    else
                    {
                         anime = anime.OrderBy(a => a.TYPE);
                    }
                    break;
            }

            if(!String.IsNullOrEmpty(searching))
            {
                anime = anime.Where(a => a.TITLE.StartsWith(searching));
            }
            return View(anime);
        }

        // GET: /AnimeView/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = anb.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return View(anime);
        }
        
        [AuthLog(Roles="Admin")]
        // GET: /AnimeView/Create
        public ActionResult Create()
        {
            var Anime = new Anime();
            return View(Anime);
        }

        [AuthLog(Roles = "Viewer")]
        public ActionResult AddingAnime()
        {
            ViewBag.Message = "This View is designed for the Admins only.";
            return RedirectToAction("AuthorizeFailed");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,URL,IMGURL,TITLE,AIRING,DESCRIPTION,TYPE,EPISODES,SCORE,SDATE,EDATE")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                anb.Animes.Add(anime);
                anb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anime);
        }

        

        // POST: /AnimeView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        

        // GET: /AnimeView/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = anb.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return View(anime);
        }

        // POST: /AnimeView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,URL,IMGURL,TITLE,AIRING,DESCRIPTION,TYPE,EPISODES,SCORE,SDATE,EDATE")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                anb.Entry(anime).State = EntityState.Modified;
                anb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anime);
        }

        // GET: /AnimeView/Delete/5
        [Authorize(Roles="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = anb.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return View(anime);
        }

        // POST: /AnimeView/Delete/5

        [Authorize(Roles="Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anime anime = anb.Animes.Find(id);
            anb.Animes.Remove(anime);
            anb.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                anb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
