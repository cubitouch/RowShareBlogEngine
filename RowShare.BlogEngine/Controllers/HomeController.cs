using RowShare.BlogEngine.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace RowShare.BlogEngine.Controllers
{
    public class HomeController : Controller
    {
        public string BlogId
        {
            get
            {
                return ConfigurationManager.AppSettings["RowShareListId"];
            }
        }
        public ActionResult Error()
        {
            return View();
        }
        public async Task<ActionResult> Index()
        {
            BlogModel blog = new BlogModel();
            await blog.LoadBlog(BlogId, true).ConfigureAwait(false);
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
        public async Task<ActionResult> Article(string id)
        {
            BlogModel blog = new BlogModel();
            await blog.LoadBlog(BlogId).ConfigureAwait(false);
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