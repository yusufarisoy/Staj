using ArticleSpiderWeb.Models;
using ArticleSpiderWeb.Models.Home;
using ArticleSpiderWeb.Models.Home.CategorySideBar;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleSpiderWeb.Common;

namespace ArticleSpiderWeb.Controllers
{
    [ViewComponent(Name = "CategorySideBar")]
    public class CategorySideBar : ViewComponent
    {
        public IViewComponentResult Invoke(int selectedCategoryID)
        {
            using(nesneProjeContext db = new nesneProjeContext())
            {
                List<ArticleCategoryRelation> relations = db.ArticleCategoryRelation.ToList();

                DefaultModel model = new DefaultModel
                {
                    Categories = db.ArticleCategory.ToList().Select(c => new CategoryItem
                    {
                        ID = c.Id,
                        Name = c.CategoryName,
                        ArticleCount = relations.Count(x => x.CategoryId == c.Id),
                        IsSelected = selectedCategoryID == c.Id,
                        URL = TextUtilities.URLFriendly(c.CategoryName)
                    })
                };

                return View(model);
            }
        }
    }
}
