using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RowShare.CMSEngine.Controllers
{
    public class HomeController : Controller
    {
        public string ResourceId
        {
            get
            {
                return ConfigurationManager.AppSettings["RowShareResourceId"];
            }
        }
        public string ContentId
        {
            get
            {
                return ConfigurationManager.AppSettings["RowShareContentId"];
            }
        }

        public async Task<string> Index(string slug)
        {
            return  await GetTemplate(slug).ConfigureAwait(false);
        }
        private async Task<string> GetTemplate(string slug)
        {
            var template = new TemplateManager(ResourceId, ContentId);
            return await template.Load(slug);
        }
    }
}