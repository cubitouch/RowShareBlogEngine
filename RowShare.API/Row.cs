using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using CodeFluent.Runtime.Utilities;

namespace RowShare.Api
{
    public class Row
    {
        public Guid Id { get; set; }
        public string ListId { get; set; }
        public Dictionary<string, object> Values { get; private set; }

        public string Version { get; set; }

        [JsonUtilities(IgnoreWhenSerializing = true)]
        public List Parent { get; private set; }

        //public Dictionary<string, object> ValuesObject
        //{
        //    get
        //    {
        //        Dictionary<string, object> _valuesObject = new Dictionary<string, object>();

        //        if (Parent != null)
        //        {
        //            var cols = Parent.Columns;
        //            if (cols != null)
        //            {
        //                foreach (Column column in cols)
        //                {
        //                    _valuesObject.Add(column.DisplayName, Values.GetValue(column.DisplayName, column.Type, null));
        //                }
        //            }
        //        }
        //        return _valuesObject;
        //    }
        //}

        public Row()
        {
            Values = new Dictionary<string, object>();
        }

        public static Collection<Row> GetRowsByList(List list)
        {
            if (list == null)
                return null;

            Collection<Row> rows = GetRowsByListId(list.Id.ToString().Replace("-", ""));
            foreach (Row row in rows)
            {
                row.Parent = list;
            }
            return rows;
        }
        public static Collection<Row> GetRowsByListId(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "row/loadForParent/{0}", id);
            string json = RowShareCommunication.GetData(url);
            return JsonUtilities.Deserialize<Collection<Row>>(json, Utility.DefaultOptions);

        }
        public static Row GetRowById(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "/row/load/{0}", id);
            string json = RowShareCommunication.GetData(url);

            return JsonUtilities.Deserialize<Row>(json, Utility.DefaultOptions);
        }

        public static void DeleteRow(string id, string version = null)
        {
           
            var data = GetRowById(id);
            if(version != null && version != data.Version)
            {
                return;
            }
            DeleteRow(data);
        }

        public static void DeleteRow(Row data)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "/row/delete/");
            RowShareCommunication.DeleteData(url, JsonUtilities.Serialize(data));
        }

        public static Row UpdateRow(Row currentRow)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "/row/save/");
            string json = RowShareCommunication.PostData(url, currentRow);

            return JsonUtilities.Deserialize<Row>(json, Utility.DefaultOptions);
        }
    }
}
