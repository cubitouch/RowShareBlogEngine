using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RowShare.ToDo.Models
{
    public class TodoList
    {
        public List<ToDoItem> Items { get; set; }

        public TodoList()
        {
            Items = new List<ToDoItem>();
        }


    }
}