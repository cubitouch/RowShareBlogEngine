using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using CodeFluent.Runtime.Utilities;
using System.Configuration;

namespace RowShare.Api
{
    public class List
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        private Collection<Column> _columns;
        public Collection<Column> Columns
        {
            get
            {
                return _columns;
            }
        }
        private Collection<Row> _rows;
        public Collection<Row> Rows
        {
            get
            {
                return _rows;
            }
        }



        public static List GetListById(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "https://www.rowshare.com/api/list/load/{0}", id);
            string json = RowShareCommunication.GetData(url);

            return JsonUtilities.Deserialize<List>(json);
        }

        public void LoadRows()
        {
            _rows = Row.GetRowsByList(this);
        }
        public void LoadColumns()
        {
            _columns = Column.GetColumnsByList(this);
        }
        
    }
}
