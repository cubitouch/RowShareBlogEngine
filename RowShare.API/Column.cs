﻿using System;
using System.Collections.ObjectModel;
using System.Globalization;
using CodeFluent.Runtime.Utilities;

namespace RowShare.Api
{
    public class Column
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
        public string DisplayName { get; set; }

        [JsonUtilities(IgnoreWhenSerializing = true)]
        public List Parent { get; private set; }

        public static Collection<Column> GetColumnsByList(List list)
        {
            if (list == null)
                return null;

            Collection<Column> columns = GetColumnsByListId(list.Id.ToString().Replace("-", ""));
            foreach (Column column in columns)
            {
                column.Parent = list;
            }
            return columns;
        }

        public static Collection<Column> GetColumnsByListId(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "https://www.rowshare.com/api/column/loadForParent/{0}", id);
            string json = RowShareCommunication.GetData(url); 

            return JsonUtilities.Deserialize<Collection<Column>>(json);
        }
    }
}
