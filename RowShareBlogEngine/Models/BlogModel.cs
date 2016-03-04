using CodeFluent.Runtime.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace RowShareBlogEngine.Models
{
    public class BlogModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ArticleModel> Articles { get; set; }


        public BlogModel()
        {
            Articles = new List<ArticleModel>();
        }

        public void LoadBlog(string id, bool withArticles = false)
        {
            string url = string.Format("https://www.rowshare.com/api/list/load/{0}", id);
            WebClient client = new WebClient();
            string result = client.DownloadString(url);
            DeserializeBlog(result);

            if (withArticles)
            {
                LoadBlogArticles(id);
            }
        }
        public void LoadBlogArticles(string id)
        {
            string url = string.Format("https://www.rowshare.com/api/row/loadForParent/{0}", id);
            WebClient client = new WebClient();
            string result = client.DownloadString(url);
            DeserializeArticle(result);
        }

        private void DeserializeBlog(string json)
        {
            TableSerialized table = JsonUtilities.Deserialize<TableSerialized>(json);

            Id = table.Id;
            Title = table.DisplayName;
            Description = table.Description;
        }
        private void DeserializeArticle(string json)
        {
            List<RowSerialized> rows = JsonUtilities.Deserialize<List<RowSerialized>>(json);

            foreach (RowSerialized row in rows)
            {
                ArticleModel article = new ArticleModel();
                article.Id = row.Id;
                article.Title = row.Values["Title"];
                article.Category = row.Values["Category"];
                article.Date = DateTime.Parse(row.Values["Date"]);
                article.IsPublished = bool.Parse(row.Values["Published"]);
                article.Keywords = row.Values["Keywords"];
                article.Content = row.Values["Content"];

                Articles.Add(article);
            }
        }
    }
}