using System.Threading.Tasks;
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
        public async Task<ContentResult> LoadGraphData(string id)
        {
            List list = await List.GetListById(id).ConfigureAwait(false);
            list.LoadColumns();
            await list.LoadRows().ConfigureAwait(false);

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