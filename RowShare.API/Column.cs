using System;
using System.Collections.Generic;
using System.Net;
using CodeFluent.Runtime.Utilities;

namespace RowShare.API
{
    public class Column
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
        public string DisplayName { get; set; }

        [JsonUtilities(IgnoreWhenSerializing = true)]
        public List Parent;

        public static List<Column> GetColumnsByList(List list)
        {
            List<Column> columns = GetColumnsByListId(list.Id.ToString().Replace("-", ""));
            columns.ForEach(r => r.Parent = list);
            return columns;
        }
        public static List<Column> GetColumnsByListId(string id)
        {
            string url = string.Format("https://www.rowshare.com/api/column/loadForParent/{0}", id);
            WebClient client = new WebClient();
            string json = client.DownloadString(url);
            return JsonUtilities.Deserialize<List<Column>>(json);
        }
    }
}
