using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptainMao.Models;
using CaptainMao.Areas.Article.Models;
using PagedList.Mvc;
using PagedList;
using Microsoft.Security.Application;

namespace CaptainMao.Areas.Article.Controllers
{
    public class ArticleController : Controller
    {
        MaoEntities db = new MaoEntities();
        // GET: Article/Article
        public ActionResult Index(int? page,int? titleCategoryID, int? boardID)
        {
            var vm=db.Article.Select(a=>a);
            if (titleCategoryID!=null)
            {
                vm = vm.Where(a => a.TitleCategoryID == titleCategoryID);
            }
            if (boardID != null)
            {
                vm = vm.Where(a => a.BoardID == boardID).OrderByDescending(a => a.LastChDateTime);
            }
            vm = vm.OrderByDescending(a => a.LastChDateTime);
            return View(vm.ToList().ToPagedList(page ?? 1,10));
        }
        [ChildActionOnly]
        public ActionResult BoardCategories()
        {
            return PartialView(db.Board.ToList());
        }
        public ActionResult Board()
        {
            BoardViewModel vm = new BoardViewModel();
            vm.article = db.Article.OrderByDescending(a=>a.Number).Take(6);
            return View(vm);
        }
        public ActionResult Create()
        {
            ViewBag.datas = db.TitleCategories.ToList();
            ViewBag.datas2 = db.Board.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CaptainMao.Models.Article article)
        {
            ViewBag.datas = db.TitleCategories.ToList();
            ViewBag.datas2 = db.Board.ToList();

            //string url = Url.Content("~/Images/" + upload.FileName);
            ////string script = $"<script>window.parent.CKEDITOR.tools.callFunction({article},'{url}','')</script>";
            //upload.SaveAs(Server.MapPath("~/Images/" + upload.FileName));
            //return Content(script);
            if (ModelState.IsValid)
            {
                article.CreateDateTime = DateTime.Now;
                article.LastChDateTime = DateTime.Now;
                article.PosterID = "215";
                article.Number = 0;

                db.Article.Add(article);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Show(int? articleID)
        {
            CaptainMao.Areas.Article.Models.CommentViewModel vm = new CommentViewModel();
            var article = db.Article.Find(articleID);
            article.Number++;

            db.Entry(article).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            vm.comment = db.Comment.Where(a=>a.ArticleID==articleID);
            vm.article = db.Article.Where(a => a.ArticleID == articleID);
            return View(vm);
        }
        public ActionResult Comment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Comment(CaptainMao.Models.Comment comment,int id)
        {
            if (ModelState.IsValid)
            {
                comment.ArticleID = id;
                comment.NewDateTime = DateTime.Now;
                comment.PosterID = "215";
                
                var articleDT = db.Article.Find(id);
                articleDT.LastChDateTime = DateTime.Now;
                db.Entry(articleDT).State = System.Data.Entity.EntityState.Modified;

                db.Comment.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Aside()
        {
            return PartialView();
        }
        public ActionResult Poster(int? page,string posterID)
        {
            posterID = "215";
            var vm = db.Article.Where(a => a.PosterID == posterID);
            return View(vm.ToList().ToPagedList(page ?? 1,10));
        }
        public ActionResult Edit(int? articleID)
        {
            ViewBag.datas = db.TitleCategories.ToList();
            ViewBag.datas2 = db.Board.ToList();
            Convert.ToString(HttpUtility.UrlDecode(Request.Form["ContentText"]));
            return View(db.Article.Find(articleID));
        }
        [HttpPost]
        public ActionResult Edit(CaptainMao.Models.Article article)
        {
            ViewBag.datas = db.TitleCategories.ToList();
            ViewBag.datas2 = db.Board.ToList();

            if (ModelState.IsValid)
            {
                Convert.ToString(HttpUtility.UrlDecode(Request.Form["ContentText"]));
                article.LastChDateTime = DateTime.Now;
                db.Entry(article).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Poster");
        }
    }
}