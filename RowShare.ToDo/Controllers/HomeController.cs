using Newtonsoft.Json;
using RowShare.ToDo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RowShare.ToDo.Controllers
{
    public class HomeController : Controller
    {
        public string RowShareListId
        {
            get
            {
                return ConfigurationManager.AppSettings["RowShareListId"];
            }
        }

        public ActionResult DeleteItem(string id)
        {
            var currentRow = RowShare.Api.Row.GetRowById(id);
            RowShare.Api.Row.DeleteRow(id, currentRow.Version);

            return RefreshData();
        }

        public ActionResult RefreshData()
        {
            var list = RowShare.Api.List.GetListById(RowShareListId);
            list.LoadRows();
            var todoList = new TodoList();
            foreach (var todoItem in list.Rows)
            {
                var todo = new ToDoItem();
                todo.Item = todoItem.Values["Item"].ToString();
                todo.Done = Boolean.Parse(todoItem.Values["Done"].ToString());
                todo.Id = todoItem.Id;
                todoList.Items.Add(todo);
            }

            var result = JsonConvert.SerializeObject(todoList,
                            Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            });

            return Content(result, "application/json");
        }


        public ActionResult ChangeStatus(string id)
        {
            var currentRow = RowShare.Api.Row.GetRowById(id);
            currentRow.Values["Done"] = !Boolean.Parse(currentRow.Values["Done"].ToString());
            RowShare.Api.Row.UpdateRow(currentRow);

            return RefreshData();
            
        }


        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var list = RowShare.Api.List.GetListById(RowShareListId);
            list.LoadRows();
            var todoList = new TodoList();
            
            foreach(var todoItem in list.Rows)
            {
                var todo = new ToDoItem();
                todo.Init(todoItem);
                todoList.Items.Add(todo);
            }

            return View(todoList);
        }
    }
}
