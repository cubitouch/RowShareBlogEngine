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
        public Table Parent;

        public static List<Column> GetColumnsByTable(Table table)
        {
            List<Column> columns = GetColumnsByTableId(table.Id.ToString().Replace("-", ""));
            columns.ForEach(r => r.Parent = table);
            return columns;
        }
        public static List<Column> GetColumnsByTableId(string id)
        {
            string url = string.Format("https://www.rowshare.com/api/column/loadForParent/{0}", id);
            WebClient client = new WebClient();
            string json = client.DownloadString(url);
            return JsonUtilities.Deserialize<List<Column>>(json);
        }
    }
}
