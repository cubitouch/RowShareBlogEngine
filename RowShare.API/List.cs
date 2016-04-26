using System;
using System.Collections.ObjectModel;
using System.Globalization;
using CodeFluent.Runtime.Utilities;

namespace RowShare.Api
{
    public class List
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int ColumnCount { get; set; }
        public Folder Folder { get; set; }
        public Collection<Column> Columns { get; private set; }
        public Collection<Row> Rows { get; private set; }

        public static List GetListById(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "list/load/{0}", id);
            string json = RowShareCommunication.GetData(url);

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

        public static void DeleteList(string id)
        {
            var data = GetListById(id);
            DeleteList(data);
        }
        
        public static void DeleteList(List data)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "/row/delete/");
            RowShareCommunication.DeleteData(url, JsonUtilities.Serialize(data));
        }
    }
}
