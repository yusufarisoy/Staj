using System;
using System.Collections.Generic;

namespace ArticleSpiderWeb.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleCategoryRelation = new HashSet<ArticleCategoryRelation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrl { get; set; }
        public string ContentSummary { get; set; }
        public bool VisitCheck { get; set; }
        public string FullContent { get; set; }
        public string ImageSrc { get; set; }

        public virtual ICollection<ArticleCategoryRelation> ArticleCategoryRelation { get; set; }
    }
}
