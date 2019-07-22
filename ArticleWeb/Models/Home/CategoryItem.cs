using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleSpiderWeb.Models.Home
{
    public class CategoryItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ArticleCount { get; set; }
        public bool IsSelected { get;  set; }
        public string URL { get; set; }
        public int Page { get; set; }
    }
}
