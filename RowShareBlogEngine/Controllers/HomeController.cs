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
            return View(blog);
        }
        public ActionResult Article(Guid id)
        {
            BlogModel blog = new BlogModel();
            blog.LoadBlog(BlogId, true);

            ViewBag.BlogTitle = blog.Title;
            ViewBag.BlogDescription = blog.Description;
            return View(blog.Articles.First(a => a.Id == id));
        }
    }
}