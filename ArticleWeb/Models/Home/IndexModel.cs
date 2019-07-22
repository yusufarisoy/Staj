using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleSpiderWeb.Models.Home
{
    public class IndexModel
    {
        public IEnumerable<ArticleItem> Articles { get; set; }
        public List<ArticleCategoryRelation> Relations { get; set; }
        public List<ButtonItem> Buttons { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public string NextPageURL { get; set; }
        public string PreviousURL { get; set; }
        public int SelectedCategoryID { get; set; }
    }
}
