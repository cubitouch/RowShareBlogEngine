﻿using RowShare.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RowShare.BrowsingEngine.Models
{
    public class FolderModel
    {
        public Guid Id { get; set; }

        public string Title { get; set;  }

        public List<FolderModel> ContainingFolders { get; set; }

        public List<ListModel> ContainingLists { get; set; }

        public void LoadContent(string id)
        {

            var folder = Folder.GetFolderById(id);
            if (folder == null)
                return;

            folder.LoadContent();

            foreach(var f in folder.ContainingFolders)
            {
                ContainingFolders.Add(new FolderModel() { Id = f.Id, Title = f.DisplayName });
            }
            foreach(var l in folder.ContainingLists)
            {
                ContainingLists.Add(new ListModel() { Id = l.Id, Title = l.DisplayName });
            }

        }

    }
}