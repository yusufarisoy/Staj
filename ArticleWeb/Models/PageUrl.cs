using System;
using System.Collections.Generic;

namespace ArticleSpiderWeb.Models
{
    public partial class PageUrl
    {
        public int Id { get; set; }
        public string LinkFormat { get; set; }
        public int LastPage { get; set; }
        public int TotalPage { get; set; }
    }
}
