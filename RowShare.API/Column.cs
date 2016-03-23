using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using CodeFluent.Runtime.Utilities;

namespace RowShare.Api
{
    public class Column
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
        public string DisplayName { get; set; }

        private List _parent;
        [JsonUtilities(IgnoreWhenSerializing = true)]
        public List Parent
        {
            get
            {
                return _parent;
            }
        }

        public static Collection<Column> GetColumnsByList(List list)
        {
            if (list == null)
                return null;

            Collection<Column> columns = GetColumnsByListId(list.Id.ToString().Replace("-", ""));
            foreach (Column column in columns)
            {
                column._parent = list;

            }
            return columns;
        }
        public static Collection<Column> GetColumnsByListId(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "https://www.rowshare.com/api/column/loadForParent/{0}", id);
            string json;
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString(url);
            }
            return JsonUtilities.Deserialize<Collection<Column>>(json);
        }
    }
}
