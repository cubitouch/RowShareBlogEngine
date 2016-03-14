using System;
using System.Collections.Generic;
using RowShare.API;

namespace RowShare.BlogEngine.Models
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
            Table table = Table.GetTableById(id);

            if (table != null)
            {
                Id = table.Id;
                Title = table.DisplayName;
                Description = table.Description;

                if (withArticles)
                {
                    LoadBlogArticles(id);
                }
            }
        }
        public void LoadBlogArticles(string id)
        {
            List<Row> rows = Row.GetRowsByTableId(id);

            foreach (Row row in rows)
            {
                ArticleModel article = new ArticleModel();
                article.Id = row.Id;
                article.Title = row.Values["Title"];
                article.Category = row.Values["Category"];
                DateTime date;
                DateTime.TryParse(row.Values["Date"], out date);
                article.Date = date;
                article.IsPublished = bool.Parse(row.Values["Published"]);
                article.Keywords = row.Values["Keywords"];
                article.Content = row.Values["Content"];

                Articles.Add(article);
            }
        }
    }
}