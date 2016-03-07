using RowShareBlogEngine.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RowShareBlogEngine.Controllers
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
        public ActionResult Index()
        {
            BlogModel blog = new BlogModel();
            blog.LoadBlog(BlogId, true);

            ViewBag.BlogTitle = blog.Title;
            ViewBag.BlogDescription = blog.Description;
            ViewBag.BlogKeywords = blog.Title;
            return View(blog);
        }
        public ActionResult Article(string id)
        {
            BlogModel blog = new BlogModel();
            blog.LoadBlog(BlogId);
            ArticleModel article = new ArticleModel();
            article.LoadArticle(id);

            ViewBag.BlogTitle = blog.Title;
            ViewBag.BlogDescription = blog.Description;
            ViewBag.BlogKeywords = article.Keywords;
            return View(article);
        }
    }
}