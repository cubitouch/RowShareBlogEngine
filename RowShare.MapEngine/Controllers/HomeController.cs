using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
                return ConfigurationManager.AppSettings["RowShareTableId"];
            }
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Index()
        {
            MapModel map = new MapModel();
            map.LoadMap(MapId);
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