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
            List list = List.GetListById(id);

            if (list != null)
            {
                Id = list.Id;
                Title = list.DisplayName;
                Description = list.Description;

                if (withArticles)
                {
                    LoadBlogArticles(id);
                }
            }
        }
        public void LoadBlogArticles(string id)
        {
            List<Row> rows = Row.GetRowsByListId(id);

            foreach (Row row in rows)
            {
                ArticleModel article = new ArticleModel();
                article.Init(row);
                Articles.Add(article);
            }
        }
    }
}