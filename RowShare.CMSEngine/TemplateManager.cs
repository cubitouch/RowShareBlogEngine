using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RowShare.Api;
using System.Collections.ObjectModel;

namespace RowShare.CMSEngine
{
    public class Resource
    {
        public string Slug { get; set; }
        public string FileUrl { get; set; }
    }
    public class TemplateManager
    {
        public string ResourceId { get; set; }
        public string ContentId { get; set; }
        public string Slug { get; set; }

        public string Template { get; set; }
        public List<Resource> Resources { get; set; }

        public TemplateManager(string resourceId, string contentId)
        {
            ResourceId = resourceId;
            ContentId = contentId;
        }

        public async Task<string> Load(string slug)
        {
            Resources = new List<Resource>();

            List list = await List.GetListById(ContentId);
            Collection<Row> rows = await Row.GetRowsByListId(ResourceId);

            // init template
            foreach (Row row in rows)
            {
                if (row.Values["Slug"].ToString() == "index.html")
                {
                    WebClient client = new WebClient();
                    Template = client.DownloadString(row.GetResourceLink("File", false));
                }
            }

            // init resources
            foreach (Row row in rows)
            {
                if (row.Values["Slug"].ToString() != "index.html")
                {
                    Resource resource = new Resource();

                    resource.Slug = row.Values["Slug"].ToString();
                    resource.FileUrl = row.GetResourceLink("File", false);

                    Resources.Add(resource);
                }
            }

            // replace template resources moustaches
            foreach (Resource resource in Resources)
            {
                Template = Template.Replace(string.Format("{{{{resource/{0}}}}}", resource.Slug), resource.FileUrl.ToString());
            }

            // replace system moustaches
            Template = Template.Replace("{{CMSEngine.Title}}", list.DisplayName);
            Template = Template.Replace("{{CMSEngine.RSContentId}}", ContentId);

            Collection<Row> contents = await Row.GetRowsByListId(ContentId);
            foreach (Row content in contents)
            {
                if (content.Values["Slug"].ToString() == slug + "")
                {
                    Template = Template.Replace("{{CMSEngine.Content}}", content.Values["Content"].ToString());
                }
            }

            return Template;
        }
    }
}
