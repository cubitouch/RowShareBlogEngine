using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowShare.API
{
    public class Resource
    {
        public string FileUrl { get; set; }
        public string Slug { get; set; }

        public static string GetResourceLink(Row row)
        {
            File file = new File((IDictionary<string, object>)row.Values["File"]);
            return string.Format("https://www.rowshare.com/blob/{0}1/4/{1}", row.Id.ToString().Replace("-", ""), file.FileName);
        }
    }
}
