using CodeFluent.Runtime.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowShare.Api
{
    public class RowShareUser
    {
        
        public string Email { get; set; }

        public string NickName { get; set; }
        
        public string RootFolderId { get; set; }

        [JsonUtilities(IgnoreWhenSerializing =true)]
        public Folder RootFolder
        {
            get
            {
                return Folder.GetFolderById(RootFolderId);
            }
        }

        public static RowShareUser LoadUser(string userName, string password)
        {
            var json = RowShareCommunication.GetData("user", userName, password);
            var rsUser = JsonUtilities.Deserialize<RowShareUser>(json, Utility.DefaultOptions);
            return rsUser;
        }

        public Collection<List> LoadFavoriteLists()
        {
            var json = RowShareCommunication.GetData("userlistlink/loadall");
            var favList = JsonUtilities.Deserialize<Collection<FavoriteList>>(json, Utility.DefaultOptions);
            var toReturn = new Collection<List>();
            foreach(var fav in favList)
            {
                if(fav.IsFavorite)
                    toReturn.Add(fav.List);
            }
            return toReturn;

        }

    }
}
