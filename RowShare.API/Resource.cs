using System;
using System.Collections.Generic;
using System.Globalization;

namespace RowShare.Api
{
    public class Resource
    {
        public Uri FileUrl { get; set; }
        public string Slug { get; set; }

        public static string GetResourceLink(Row row)
        {
            if (row == null)
                return string.Empty;

            File file = new File((IDictionary<string, object>)row.Values["File"]);
            return string.Format(CultureInfo.CurrentCulture, "https://www.rowshare.com/blob/{0}1/4/{1}", row.Id.ToString().Replace("-", ""), file.FileName);
        }
    }
}
