using System;
using System.Collections.Generic;
using RowShare.Api;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        public async Task LoadBlog(string id, bool withArticles = false)
        {
            List list = await List.GetListById(id).ConfigureAwait(false);

            if (list != null)
            {
                Id = list.Id;
                Title = list.DisplayName;
                Description = list.Description;

                if (withArticles)
                {
                    await LoadBlogArticles(id).ConfigureAwait(false);
                }
            }
        }
        public async Task LoadBlogArticles(string id)
        {
            Collection<Row> rows = await Row.GetRowsByListId(id);

            foreach (Row row in rows)
            {
                ArticleModel article = new ArticleModel();
                article.Init(row);
                Articles.Add(article);
            }
        }
    }
}