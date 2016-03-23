using System.Web.Mvc;
using Newtonsoft.Json;
using RowShare.Api;

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
            List list = List.GetListById(id);
            list.LoadColumns();
            list.LoadRows();

            var result = JsonConvert.SerializeObject(list,
                Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

            return Content(result, "application/json");
        }
    }
}