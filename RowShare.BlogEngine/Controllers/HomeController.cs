using RowShare.BlogEngine.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RowShare.BlogEngine.Controllers
{
    public class HomeController : Controller
    {
        public string BlogId
        {
            get
            {
                return ConfigurationManager.AppSettings["RowShareTableId"];
            }
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Index()
        {
            BlogModel blog = new BlogModel();
            blog.LoadBlog(BlogId, true);
            if (blog.Id == new Guid())
            {
                return RedirectToAction("Error");
            }

            ViewBag.BlogId = BlogId;
            ViewBag.BlogTitle = blog.Title;
            ViewBag.BlogDescription = blog.Description;
            ViewBag.BlogKeywords = blog.Title;
            return View(blog);
        }
        public ActionResult Article(string id)
        {
            BlogModel blog = new BlogModel();
            blog.LoadBlog(BlogId);
            if (blog.Id == Guid.NewGuid())
            {
                return RedirectToAction("Error");
            }

            ArticleModel article = new ArticleModel();
            article.LoadArticle(id);

            ViewBag.BlogId = BlogId;
            ViewBag.BlogTitle = blog.Title;
            ViewBag.BlogDescription = blog.Description;
            ViewBag.BlogKeywords = article.Keywords;
            return View(article);
        }
    }
}