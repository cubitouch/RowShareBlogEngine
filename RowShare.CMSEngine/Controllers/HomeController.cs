using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        public string Index(string slug)
        {
            TemplateManager template = new TemplateManager(ResourceId, ContentId, slug);
            return template.Template;
        }
    }
}