using System;
using System.Collections.Generic;
using System.Net;
using CodeFluent.Runtime.Utilities;

namespace RowShare.API
{
    public class Row
    {
        public Guid Id { get; set; }
        public string ListId { get; set; }
        public Dictionary<string, object> Values { get; set; }

        public static List<Row> GetRowsByTableId(string id)
        {
            string url = string.Format("https://www.rowshare.com/api/row/loadForParent/{0}", id);
            WebClient client = new WebClient();
            string json = client.DownloadString(url);
            return JsonUtilities.Deserialize<List<Row>>(json);
        }
        public static Row GetRowById(string id)
        {
            string url = string.Format("https://www.rowshare.com/api/row/load/{0}", id);
            WebClient client = new WebClient();
            string json = client.DownloadString(url);
            return JsonUtilities.Deserialize<Row>(json);
        }
    }
}
