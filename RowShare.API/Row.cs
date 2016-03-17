using System;
using System.Collections.Generic;
using System.Net;
using CodeFluent.Runtime.Utilities;
using Newtonsoft.Json;

namespace RowShare.API
{
    public class Row
    {
        public Guid Id { get; set; }
        public string ListId { get; set; }
        public Dictionary<string, object> Values { get; set; }

        //public Table Parent;
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

        //public static List<Row> GetRowsByTable(Table table)
        //{
        //    List<Row> rows = GetRowsByTableId(table.Id.ToString().Replace("-", ""));
        //    //rows.ForEach(r => r.Parent = table);
        //    return rows;
        //}
        public static List<Row> GetRowsByTableId(string id)
        {
            string url = string.Format("https://www.rowshare.com/api/row/loadForParent/{0}", id);
            WebClient client = new WebClient();
            string json = client.DownloadString(url);

            // use of JsonConvert (Newtonsoft) instead on JsonUtilities (CodeFluent.Runtime) due to TOO SMART conversions... !
            return JsonConvert.DeserializeObject<List<Row>>(json);
            //return JsonUtilities.Deserialize<List<Row>>(json);
        }
        public static Row GetRowById(string id)
        {
            string url = string.Format("https://www.rowshare.com/api/row/load/{0}", id);
            WebClient client = new WebClient();
            string json = client.DownloadString(url);
            // use of JsonConvert (Newtonsoft) instead on JsonUtilities (CodeFluent.Runtime) due to TOO SMART conversions... !
            return JsonConvert.DeserializeObject<Row>(json);
            //return JsonUtilities.Deserialize<Row>(json);
        }
    }
}
