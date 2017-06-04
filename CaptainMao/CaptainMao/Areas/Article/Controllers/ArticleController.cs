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
        private Ipository<CaptainMao.Models.Article> articledb = new pository<CaptainMao.Models.Article>();
        private Ipository<CaptainMao.Models.Comment> commentdb = new pository<CaptainMao.Models.Comment>();
        private MaoEntities db = new MaoEntities();
        // GET: Article/Article
        //顯示所有文章
        public ActionResult Index(int? page,int? titleCategoryID, int? boardID)
        {
            var article=db.Article.Select(a=>a);
            if (titleCategoryID != null)
            {
                article = article.Where(a => a.TitleCategoryID == titleCategoryID && a.IsDeleted != true);
            }
            if (boardID != null)
            {
                article = article.Where(a => a.BoardID == boardID && a.IsDeleted != true).OrderByDescending(a => a.LastChDateTime);
            }
            article = article.Where(a=>a.IsDeleted != true).OrderByDescending(a => a.LastChDateTime);
            return View(article.ToList().ToPagedList(page ?? 1,10));
        }
        [ChildActionOnly]
        public ActionResult BoardCategories()
        {
            return PartialView(db.Board.ToList());
        }
        //顯示主版內容
        public ActionResult Board()
        {
            BoardViewModel board = new BoardViewModel();
            board.article = db.Article.Where(a=>a.IsDeleted !=true).OrderByDescending(a=>a.Number).Take(6);
            return View(board);
        }
        public ActionResult Create()
        {
            ViewBag.datas = db.TitleCategories.ToList();
            ViewBag.datas2 = db.Board.ToList();
            return View();
        }
        //建立文章
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

                articledb.Create(article);
                return RedirectToAction("Index");
            }
            return View();
        }
        //秀文章內容
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
        //建置留言
        [HttpPost]
        public ActionResult Comment(CaptainMao.Models.Comment comment,int id)
        {
            if (ModelState.IsValid)
            {
                comment = commentdb.GetID(id);
                comment.NewDateTime = DateTime.Now;
                comment.PosterID = "215";
                
                var articleDT = db.Article.Find(id);
                articleDT.LastChDateTime = DateTime.Now;
                db.Entry(articleDT).State = System.Data.Entity.EntityState.Modified;

                commentdb.Create(comment);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Aside()
        {
            return PartialView();
        }
        //顯示使用者的發佈的文章
        public ActionResult Poster(int? page,string posterID)
        {
            posterID = "215";
            var article = db.Article.Where(a => a.PosterID == posterID && a.IsDeleted != true).OrderByDescending(a=>a.LastChDateTime);
            return View(article.ToList().ToPagedList(page ?? 1, 10));
        }
        //修改文章前用ID找文章
        public ActionResult Edit(int? articleID)
        {
            ViewBag.datas = db.TitleCategories.ToList();
            ViewBag.datas2 = db.Board.ToList();
            Convert.ToString(HttpUtility.UrlDecode(Request.Form["ContentText"]));
            return View(db.Article.Find(articleID));
        }
        //修改文章
        [HttpPost]
        public ActionResult Edit(CaptainMao.Models.Article article)
        {
            ViewBag.datas = db.TitleCategories.ToList();
            ViewBag.datas2 = db.Board.ToList();

            if (ModelState.IsValid)
            {
                Convert.ToString(HttpUtility.UrlDecode(Request.Form["ContentText"]));
                article.LastChDateTime = DateTime.Now;

                articledb.Update(article);
                return RedirectToAction("Poster");
            }
            return View();
        }
        //刪除文章，但只是把文章做隱藏
        public ActionResult Del(int? articleID)
        {
            db.Article.Find(articleID).IsDeleted = true ;
            db.SaveChanges();
            return RedirectToAction("Poster");
        }
    }
}