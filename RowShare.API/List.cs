using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RowShare.Api
{
    public class List
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int ColumnCount { get; set; }
        public DateTime LastUpdateDateUtc { get; set; }
        public Folder Folder { get; set; }
        public Collection<Column> Columns { get; private set; }
        public Collection<Row> Rows { get; private set; }

        public static async Task<List> GetListById(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "list/load/{0}", id);
            string json = await RowShareCommunication.GetData(url);

            return JsonConvert.DeserializeObject<List>(json);
        }

        public async Task LoadRows()
        {
            Rows = await Row.GetRowsByList(this);
        }

        public async void LoadColumns()
        {
            Columns = await Column.GetColumnsByList(this);
        }

        //public static void DeleteList(string id)
        //{
        //    var data = GetListById(id);
        //    DeleteList(data);
        //}
        
        //public static void DeleteList(List data)
        //{
        //    string url = string.Format(CultureInfo.CurrentCulture, "/row/delete/");
        //    RowShareCommunication.DeleteData(url, JsonUtilities.Serialize(data));
        //}
    }
}
