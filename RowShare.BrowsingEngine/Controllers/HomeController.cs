using Newtonsoft.Json;
using RowShare.BrowsingEngine.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RowShare.BrowsingEngine.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetFavoriteLists(string login, string password)
        {
            var rowShareUser = RowShare.Api.RowShareUser.LoadUser(login, password);
            var favList = rowShareUser.LoadFavoriteLists();

            var result = JsonConvert.SerializeObject(favList,
                    Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

            return Content(result, "application/json");
        }

        public ActionResult Authenticate(string login, string password)
        {

            var rowShareUser = RowShare.Api.RowShareUser.LoadUser(login, password);
            var rootFolder = rowShareUser.RootFolder;
            rootFolder.LoadContent(recursively: true);
            
            var result = JsonConvert.SerializeObject(rootFolder,
                    Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

            return Content(result, "application/json");
        }
    }
}
