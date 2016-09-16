using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RowShare.Api
{
    public class RowShareUser
    {
        
        public string Email { get; set; }

        public string NickName { get; set; }
        
        public string RootFolderId { get; set; }
        
        public Folder RootFolder
        {
            get
            {
                return Folder.GetFolderById(RootFolderId).Result;
            }
        }

        public async static Task<RowShareUser> LoadUser(string userName, string password)
        {
            string json = await RowShareCommunication.GetData("user", userName, password);
            var rsUser = JsonConvert.DeserializeObject<RowShareUser>(json);
            return rsUser;
        }

        public async Task<Collection<List>> LoadFavoriteLists()
        {
            string json = await RowShareCommunication.GetData("userlistlink/loadall");
            var favList = JsonConvert.DeserializeObject<Collection<FavoriteList>>(json);
            var toReturn = new Collection<List>();
            foreach (var fav in favList)
            {
                if (fav.IsFavorite)
                    toReturn.Add(fav.List);
            }
            return toReturn;
        }
    }
}
