using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace RowShareBlogEngine.Models
{
    public class ArticleModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public bool IsPublished { get; set; }
        public string Keywords { get; set; }
        public string Content { get; set; }
        public string TruncatedContent
        {
            get
            {
                var truncateLength = 120;
                var realContent = Regex.Replace(Content, "<.*?>", string.Empty);
                return realContent.Substring(0, (realContent.Length < truncateLength ? realContent.Length : truncateLength)) + (realContent.Length > truncateLength ? "..." : "");
            }
        }
    }
}