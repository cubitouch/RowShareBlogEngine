using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowShare.API
{
    public class File
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public File(IDictionary<string, object> file)
        {
            ContentType = file["ContentType"].ToString();
            FileName = file["FileName"].ToString();
        }
    }
}
