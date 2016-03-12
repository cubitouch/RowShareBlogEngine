using System;
using System.Text.RegularExpressions;
using RowShare.API;

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

        public void LoadArticle(string id)
        {
            Row row = Row.GetRowById(id);
            
            Id = row.Id;
            Title = row.Values["Title"];
            Category = row.Values["Category"];
            Date = DateTime.Parse(row.Values["Date"]);
            IsPublished = bool.Parse(row.Values["Published"]);
            Keywords = row.Values["Keywords"];
            Content = row.Values["Content"];
        }
    }
}