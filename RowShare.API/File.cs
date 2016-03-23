using System.Collections.Generic;

namespace RowShare.Api
{
    public class File
    {
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public File(IDictionary<string, object> file)
        {
            if (file == null)
                return;

            ContentType = file["ContentType"].ToString();
            FileName = file["FileName"].ToString();
        }
    }
}
