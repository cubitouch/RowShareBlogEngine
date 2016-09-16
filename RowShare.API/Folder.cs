using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RowShare.Api
{
    public class Folder
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public Collection<List> ContainingLists { get; private set; }

        public Collection<Folder> ContainingFolders { get; private set; }

        public static async Task<Folder> GetFolderById(string id)
        {
            if (id == null)
                return null;

            string url = string.Format(CultureInfo.CurrentCulture, "folder/load/{0}", id.ToString().Replace("-", ""));
            string json = await RowShareCommunication.GetData(url);
            return JsonConvert.DeserializeObject<Folder>(json);
        }

        public async Task LoadContent(bool recursively = false)
        {
            string url = string.Format(CultureInfo.CurrentCulture, "list/loadforparent/{0}", Id.ToString().Replace("-", ""));
            string json = await RowShareCommunication.GetData(url);
            ContainingLists = JsonConvert.DeserializeObject<Collection<List>>(json);

            url = string.Format(CultureInfo.CurrentCulture, "folder/loadforparent/{0}", Id.ToString().Replace("-", ""));
            json = await RowShareCommunication.GetData(url);
            ContainingFolders = JsonConvert.DeserializeObject<Collection<Folder>>(json);
            if (recursively)
            {
                foreach (var folder in ContainingFolders)
                {
                    await folder.LoadContent(recursively);
                }
            }
        }
    }
}
