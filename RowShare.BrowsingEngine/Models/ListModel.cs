using RowShare.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RowShare.BrowsingEngine.Models
{
    public class ListModel
    {
        public Guid Id { get; set; }

        public String Title { get; set; }

        public FolderModel ParentFolder { get; set; }

        public ListModel()
        {
            ParentFolder = new FolderModel();
        }

        public void LoadList(string id)
        {
            var list = List.GetListById(id);

            if (list == null)
                return;
            Id = list.Id;
            Title = list.DisplayName;
            
            ParentFolder.LoadContent(list.Folder.Id.ToString());
            
        }

    }
}