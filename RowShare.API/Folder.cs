using CodeFluent.Runtime.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace RowShare.Api
{
    public class Folder
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public Collection<List> ContainingLists { get; private set; }

        public Collection<Folder> ContainingFolders { get; private set; }

        public static Folder GetFolderById(string id)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "folder/load/{0}", id.ToString().Replace("-", ""));
            string json = RowShareCommunication.GetData(url);
            return JsonUtilities.Deserialize<Folder>(json, Utility.DefaultOptions);
        }

        public void LoadContent(bool recursively = false)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "list/loadforparent/{0}", Id.ToString().Replace("-", ""));
            string json = RowShareCommunication.GetData(url);
            ContainingLists = JsonUtilities.Deserialize<Collection<List>>(json, Utility.DefaultOptions);

            url = string.Format(CultureInfo.CurrentCulture, "folder/loadforparent/{0}", Id.ToString().Replace("-", ""));
            json = RowShareCommunication.GetData(url);
            ContainingFolders = JsonUtilities.Deserialize<Collection<Folder>>(json, Utility.DefaultOptions);
            if(recursively)
            {
                foreach(var folder in ContainingFolders)
                {
                    folder.LoadContent(recursively);
                }
            }
        }

    }
}
