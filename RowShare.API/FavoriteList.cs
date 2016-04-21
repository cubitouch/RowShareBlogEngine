using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowShare.Api
{
    public class FavoriteList
    {
        public string ListId { get; set; }

        public int Types { get; set; }

        public bool IsFavorite { get { return this.Types == 7; } }

        public List List
        {
            get
            {
                return List.GetListById(ListId);
            }
        }

    }
}
