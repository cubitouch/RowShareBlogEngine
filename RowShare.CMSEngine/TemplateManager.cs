using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RowShare.API;

namespace RowShare.CMSEngine
{
    public class TemplateManager
    {
        public string ResourceId { get; set; }
        public string ContentId { get; set; }
        public string Slug { get; set; }

        public string Template { get; set; }
        public List<Resource> Resources { get; set; }

        public TemplateManager(string resourceId, string contentId, string slug)
        {
            ResourceId = resourceId;
            ContentId = contentId;

            Resources = new List<Resource>();

            Table table = Table.GetTableById(ContentId);
            List<Row> rows = Row.GetRowsByTableId(ResourceId);

            // init template
            foreach (Row row in rows)
            {
                if (row.Values["Slug"].ToString() == "index.html")
                {
                    WebClient client = new WebClient();
                    Template = client.DownloadString(GetResourceLink(row));
                }
            }

            // init resources
            foreach (Row row in rows)
            {
                if (row.Values["Slug"].ToString() != "index.html")
                {
                    Resource resource = new Resource();

                    resource.Slug = row.Values["Slug"].ToString();
                    resource.FileUrl = GetResourceLink(row);

                    Resources.Add(resource);
                }
            }


            // replace template resources moustaches
            foreach (Resource resource in Resources)
            {
                Template = Template.Replace(string.Format("{{{{resource/{0}}}}}", resource.Slug), resource.FileUrl);
            }

            // replace system moustaches
            Template = Template.Replace("{{CMSEngine.Title}}", table.DisplayName);

            List<Row> contents = Row.GetRowsByTableId(ContentId);
            foreach (Row content in contents)
            {
                if (content.Values["Slug"].ToString() == slug + "")
                {
                    Template = Template.Replace("{{CMSEngine.Content}}", content.Values["Content"].ToString());
                }
            }
        }


        private string GetResourceLink(Row row)
        {
            File file = new File((Dictionary<string, object>)row.Values["File"]);
            return string.Format("https://www.rowshare.com/blob/{0}1/4/{1}", row.Id.ToString().Replace("-", ""), file.FileName);
        }
    }

    public class Resource
    {
        public string FileUrl { get; set; }
        public string Slug { get; set; }

    }
    public class File
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public File(Dictionary<string, object> file)
        {
            ContentType = file["ContentType"].ToString();
            FileName = file["FileName"].ToString();
        }
    }
}
