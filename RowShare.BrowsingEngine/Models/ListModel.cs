using RowShare.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

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

        public async Task LoadList(string id)
        {
            List list = await List.GetListById(id);

            if (list == null)
                return;
            Id = list.Id;
            Title = list.DisplayName;
            
            await ParentFolder.LoadContent(list.Folder.Id.ToString());            
        }

    }
}