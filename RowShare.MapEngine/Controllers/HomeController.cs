using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RowShare.MapEngine.Models;

namespace RowShare.MapEngine.Controllers
{
    public class HomeController : Controller
    {
        public string MapId
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
            MapModel map = new MapModel();
            await map.LoadMap(MapId).ConfigureAwait(false);
            if (map.Id == new Guid())
            {
                return RedirectToAction("Error");
            }

            ViewBag.MapId = MapId;
            ViewBag.MapTitle = map.Title;
            ViewBag.MapDescription = map.Description;
            return View(map);
        }
    }
}