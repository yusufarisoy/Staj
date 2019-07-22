using ArticleSpiderWeb.Models;
using ArticleSpiderWeb.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace ArticleSpiderWeb.Common
{
    public class ArticleBussiness
    {
        public static IEnumerable<ArticleItem> GetArticles(int pageNumber, int pageSize, int? categoryID, out int totalPageSize)
        {
            using (nesneProjeContext db = new nesneProjeContext())
            {
                List<ArticleCategoryRelation> relations = db.ArticleCategoryRelation.ToList();
                IEnumerable<Article> articles = db.Article.ToList();

                if (categoryID.HasValue)
                {
                    List<int> filteredArticleIDList = relations.Where(x => x.CategoryId == categoryID.Value).Select(w => w.ArticleId).ToList();
                    articles = articles.Where(c => filteredArticleIDList.Contains(c.Id));
                }
                int totalRowCount = articles.Count();
                totalPageSize = totalRowCount / pageSize;
                articles = articles.Skip((pageNumber - 1) * pageSize).Take(pageSize);

                return articles.Select(c => new ArticleItem
                {
                    ID = c.Id,
                    Title = c.Title,
                    Summary = c.ContentSummary
                });
            }
        }
        public static List<ButtonItem> GetPageButtons(int totalPageSize, int currentPage, string urlFormat)
        {
            List<ButtonItem> buttons = new List<ButtonItem>();
            for (int i = 1; i <= totalPageSize; i++)
            {
                buttons.Add(new ButtonItem
                {
                    Text = i.ToString(),
                    IsSelected = currentPage == i,
                    URL = string.Format(urlFormat, i)
                }
                );
            }

          
            return buttons;
        }

        public static IEnumerable<ArticleItem> GetArticleDetail(int id)
        {
            using(nesneProjeContext db = new nesneProjeContext())
            {
                List<ArticleCategoryRelation> relations = db.ArticleCategoryRelation.ToList();
                IEnumerable<Article> article = db.Article.Where(c => c.Id == id).ToList();

                return article.Select(c => new ArticleItem
                {
                    ID = c.Id,
                    Title = c.Title,
                    Content = c.FullContent,
                    ImageSrc = c.ImageSrc
                });

            }
        }

    }
}
