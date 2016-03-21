using System.Web.Mvc;
using Newtonsoft.Json;
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
        public ContentResult LoadGraphData(string id)
        {
            Table table = Table.GetTableById(id);
            table.LoadColumns();
            table.LoadRows();

            var list = JsonConvert.SerializeObject(table,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

            return Content(list, "application/json");
        }
    }
}