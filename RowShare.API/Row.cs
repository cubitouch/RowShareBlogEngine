using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RowShare.Api
{
    public class Row
    {
        public Guid Id { get; set; }
        public string ListId { get; set; }
        public Dictionary<string, object> Values { get; private set; }

        public string GetResourceLink(string key, bool isImage = true)
        {
            if (Values[key] == null)
                return string.Empty;

            File file = new File(JsonConvert.DeserializeObject<IDictionary<string, object>>(Values[key].ToString()));
            if (isImage)
                return string.Format(CultureInfo.CurrentCulture, "https://www.rowshare.com/{0}", file.ImageUrl);
            else
                return string.Format(CultureInfo.CurrentCulture, "https://www.rowshare.com/{0}", file.ImageUrl.Replace(Path.GetExtension(file.ImageUrl), file.FileName));
        }

        public string Version { get; set; }

        public List Parent { get; private set; }

        public Row()
        {
            Values = new Dictionary<string, object>();
        }

        public static async Task<Collection<Row>> GetRowsByList(List list)
        {
            if (list == null)
                return null;

            Collection<Row> rows = await GetRowsByListId(list.Id.ToString().Replace("-", ""));
            foreach (Row row in rows)
            {
                row.Parent = list;
            }
            return rows;
        }

        public static async Task<Collection<Row>> GetRowsByListId(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "row/loadForParent/{0}", id);
            string json = await RowShareCommunication.GetData(url);
            return JsonConvert.DeserializeObject<Collection<Row>>(json);
        }

        public static async Task<Row> GetRowById(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "/row/load/{0}", id);
            string json = await RowShareCommunication.GetData(url);
            return JsonConvert.DeserializeObject<Row>(json);
        }

        //public static void DeleteRow(string id, string version = null)
        //{

        //    var data = GetRowById(id);
        //    if(version != null && version != data.Version)
        //    {
        //        return;
        //    }
        //    DeleteRow(data);
        //}

        //public static void DeleteRow(Row data)
        //{
        //    string url = string.Format(CultureInfo.CurrentCulture, "/row/delete/");
        //    RowShareCommunication.DeleteData(url, JsonUtilities.Serialize(data));
        //}

        //public static Row UpdateRow(Row currentRow)
        //{
        //    string url = string.Format(CultureInfo.CurrentCulture, "/row/save/");
        //    string json = RowShareCommunication.PostData(url, currentRow);

        //    return JsonUtilities.Deserialize<Row>(json, Utility.DefaultOptions);
        //}
    }
}
