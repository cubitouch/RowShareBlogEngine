using Newtonsoft.Json;
using RowShare.BrowsingEngine.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using RowShare.Api;
using System.Threading.Tasks;

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

        public async Task<ActionResult> GetFavoriteLists(string login, string password)
        {
            RowShareUser rowShareUser = await RowShareUser.LoadUser(login, password).ConfigureAwait(false);
            Collection<List> favList = await rowShareUser.LoadFavoriteLists().ConfigureAwait(false);

            var result = JsonConvert.SerializeObject(favList,
                    Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

            return Content(result, "application/json");
        }

        public async Task<ActionResult> Authenticate(string login, string password)
        {
            RowShareUser rowShareUser = await RowShareUser.LoadUser(login, password).ConfigureAwait(false);
            var rootFolder = rowShareUser.RootFolder;
            if (rootFolder != null)
                await rootFolder.LoadContent(recursively: true).ConfigureAwait(false);

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
