using System;
using System.Collections.Generic;

namespace ArticleSpiderWeb.Models
{
    public partial class ArticleCategoryRelation
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }

        public virtual Article Article { get; set; }
        public virtual ArticleCategory Category { get; set; }
    }
}
