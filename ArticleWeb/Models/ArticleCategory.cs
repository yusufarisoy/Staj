using System;
using System.Collections.Generic;

namespace ArticleSpiderWeb.Models
{
    public partial class ArticleCategory
    {
        public ArticleCategory()
        {
            ArticleCategoryRelation = new HashSet<ArticleCategoryRelation>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<ArticleCategoryRelation> ArticleCategoryRelation { get; set; }
    }
}
