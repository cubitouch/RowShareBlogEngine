using System;
using System.Collections.Generic;
using System.Net;
using CodeFluent.Runtime.Utilities;

namespace RowShare.API
{
    public class List
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<Column> Columns;
        public List<Row> Rows;

        public static List GetListById(string id)
        {
            string url = string.Format("https://www.rowshare.com/api/list/load/{0}", id);
            WebClient client = new WebClient();
            string json = client.DownloadString(url);
            return JsonUtilities.Deserialize<List>(json);
        }

        public void LoadRows()
        {
            Rows = Row.GetRowsByList(this);
        }
        public void LoadColumns()
        {
            Columns = Column.GetColumnsByList(this);
        }
    }
}
