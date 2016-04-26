using RowShare.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RowShare.ToDo.Models
{
    public class ToDoItem
    {
        public string Item { get; set; }

        public bool Done { get; set; }

        public Guid Id { get; set; }

        public void Init(Row row)
        {
            this.Item = row.Values["Item"].ToString();
            this.Done = Boolean.Parse(row.Values["Done"].ToString());
            this.Id = row.Id;
        }

    }
}