using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArticleSpiderWeb.Models;
using ArticleSpiderWeb.Models.Home;
using System.Text;
using ArticleSpiderWeb.Common;

namespace ArticleSpiderWeb
{
    public class HomeController : Controller
    {
        private const int PageSize = 15;

        public IActionResult Index(int? id, int pageNumber = 1)
        {
            int totalPageSize = 0;
            IndexModel model = new IndexModel();
            model.Articles  = ArticleBussiness.GetArticles(pageNumber, PageSize, null, out totalPageSize);
            model.Buttons = ArticleBussiness.GetPageButtons(totalPageSize, pageNumber, "/makaleler/sayfa-{0}");

            if(totalPageSize > 0 && pageNumber > 1)
            {
                model.HasPreviousPage = true;
                model.PreviousURL = "/makaleler/sayfa-{pageNumber-1}";
            }
            if(totalPageSize > 0 && pageNumber != totalPageSize)
            {
                model.HasNextPage = true;
                model.NextPageURL = "/makaleler/sayfa-{pageNumber+1}";
            }

            return View(model);
        }

        public IActionResult FilteredCategory(string Title, int id, int pageNumber)
        {
            int totalPageSize = 0;
            IndexModel model = new IndexModel();
            model.Articles = ArticleBussiness.GetArticles(pageNumber, PageSize, id, out totalPageSize);
            model.Buttons = ArticleBussiness.GetPageButtons(totalPageSize, pageNumber, "/makaleler/"+ Title +"/"+id +"/sayfa-{0}");

            if (totalPageSize > 0 && pageNumber > 1)
            {
                model.HasPreviousPage = true;
                model.PreviousURL = "/makaleler/sayfa-{pageNumber-1}";
            }
            if (totalPageSize > 0 && pageNumber != totalPageSize)
            {
                model.HasNextPage = true;
                model.NextPageURL = "/makaleler/sayfa-{pageNumber+1}";
            }

            return View(model);
        }
       
        public IActionResult ArticleDetail(int id)
        {
            IndexModel model = new IndexModel();
            model.Articles = ArticleBussiness.GetArticleDetail(id);

            return View(model);
        }
        
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
