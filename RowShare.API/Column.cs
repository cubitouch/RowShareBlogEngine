using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RowShare.Api
{
    public class Column
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
        public string DisplayName { get; set; }
        
        public List Parent { get; private set; }

        public static async Task<Collection<Column>> GetColumnsByList(List list)
        {
            if (list == null)
                return null;

            Collection<Column> columns = await GetColumnsByListId(list.Id.ToString().Replace("-", ""));
            foreach (Column column in columns)
            {
                column.Parent = list;
            }
            return columns;
        }

        public static async Task<Collection<Column>> GetColumnsByListId(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "/column/loadForParent/{0}", id);
            string json = await RowShareCommunication.GetData(url);

            return JsonConvert.DeserializeObject<Collection<Column>>(json);
        }

        public static async Task<Column> GetColumnById(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "/column/load/", id);
            string json = await RowShareCommunication.GetData(url);

            return JsonConvert.DeserializeObject<Column>(json);
        }

        //public static void DeleteColumn(string id)
        //{
        //    var data = GetColumnById(id);
        //    DeleteColumn(data);
        //}

        //public static void DeleteColumn(Column data)
        //{
        //    var url = "/column/delete";
        //    RowShareCommunication.DeleteData(url, JsonUtilities.Serialize(data));
        //}
    }
}
