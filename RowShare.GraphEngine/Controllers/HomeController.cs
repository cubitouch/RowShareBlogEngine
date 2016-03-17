using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RowShare.API;

namespace RowShare.GraphEngine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadGraphData(string id)
        {
            Table table = Table.GetTableById(id);
            table.LoadRows();

            return Json(table,JsonRequestBehavior.AllowGet);
        }
    }
}