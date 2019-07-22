using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using log4net;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.ComponentModel;

namespace ConsoleApp1
{
    class Program
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            DownloadImages();

            CreatePdf();
            
            string line;
            do
            {
                line = Console.ReadLine();
                log.Info(line);
            }while (line != "q");

            string type = args == null ? string.Empty : args.FirstOrDefault();
            if (type == "categories")
                GetPageURL();
            else if (type == "articles")
                GetContents();
            else if (type == "links")
                TakeCategoriesLinks();
            else
                Console.WriteLine("not defined");
        }

        static void GetPageContent(int pageID)
        {
            using (var db = new nesneProjeEntities1())
            {
                var visitor = db.PageURLs.SingleOrDefault(c => c.ID == pageID);

                while (visitor.LastPage != visitor.TotalPage)
                {
                        var pageURL = visitor.LinkFormat + visitor.LastPage;
                        var web = new HtmlWeb();
                        var doc = web.Load(pageURL);

                        HtmlNodeCollection articles = doc.DocumentNode.SelectNodes("//div[@class='block-article bdaiaFadeIn']");

                        List<ArticleContainer> articleList = new List<ArticleContainer>();
      
                        var i = visitor.LastPage;
                        Console.WriteLine("\r\nCurrent Page: "+i);
                        foreach (HtmlNode article in articles)
                        {
                            HtmlNode title = article.SelectSingleNode("article/header/h2[@class='entry-title']/a");
                            string titleText = title.InnerText.Trim();
                            string titleUrl = title.Attributes["href"].Value;

                            HtmlNode p = article.SelectSingleNode("article/div[@class='block-article-content-wrapper']/p");
                            string contentText = p.InnerText.Trim();

                            HtmlNode div = article.SelectSingleNode("article/footer/div[@class='bdaia-post-cat-list']");
                            List<string> category = div.InnerText.Trim().Replace("Kategori:&nbsp;: &nbsp;", "").Split(',').ToList();
                          
                            var container = new ArticleContainer
                            {
                                Article = new Article
                                {
                                    Title = titleText,
                                    TitleUrl = titleUrl,
                                    ContentSummary = contentText,
                                },
                                Categories = category
                            };

                            Console.WriteLine("\r\n" + container.Article.Title + "\r\n" + container.Article.ContentSummary + "\r\n" + string.Join(",", container.Categories) + "\r\n");
                            if(!db.Articles.Any(c => c.Title == container.Article.Title))
                            {
                                articleList.Add(container);
                            }

                        }
                        DBSaver(articleList);
                    if (visitor.LastPage < visitor.TotalPage)
                    {
                        visitor.LastPage = visitor.LastPage + 1;
                        db.SaveChanges();
                    }
                }
            }
        }


        static void DBSaver(List<ArticleContainer> articles)
        {

            using(var DB = new nesneProjeEntities1())
            {

                foreach (ArticleContainer articleObject in articles)
                {
                    foreach (string category in articleObject.Categories)
                    {
                        int categoryID = GetCategoryOrInsert(DB, category);

                        articleObject.Article.ArticleCategoryRelations.Add(new ArticleCategoryRelation { CategoryID = categoryID });
                    }
                    DB.Articles.Add(articleObject.Article);
                }
                DB.SaveChanges();
            }
        }

        public static int GetCategoryOrInsert(nesneProjeEntities1 DB, string categoryName)
        {
            ArticleCategory articleCategory = DB.ArticleCategories.SingleOrDefault(c => c.CategoryName == categoryName.Trim());
            if (articleCategory == null)
            {
                articleCategory = new ArticleCategory();
                articleCategory.CategoryName = categoryName.Trim();
                DB.ArticleCategories.Add(articleCategory);
                DB.SaveChanges();
            }
            return articleCategory.ID;
        }

        public static void GetPageURL()
        {
            using(var DB = new nesneProjeEntities1())
            {
                List<int> pageIDs = DB.PageURLs.Where(a => a.LastPage != a.TotalPage).Select(c => c.ID).ToList();

                List<Action> actions = new List<Action>();
                foreach (int id in pageIDs)
                {
                    actions.Add(() => { GetPageContent(id); });
                }
                Parallel.ForEach(actions, new ParallelOptions { MaxDegreeOfParallelism = 4 }, x => x());
            }
        }

        public static void GetContents()
        {
            using (var DB = new nesneProjeEntities1())
            {
                List<int> articleIDs = DB.Articles.Where(c => !c.VisitCheck).Select(c => c.ID).ToList();

                List<Action> actions = new List<Action>();
                foreach (int id in articleIDs)
                {
                    actions.Add(() => { GetArticleContent(id); });
                }
                Parallel.ForEach(actions, new ParallelOptions { MaxDegreeOfParallelism = 4 }, x => x());
            }
        }

        public static void GetArticleContent(int articleId)
        {
            try { 
                using (var DB = new nesneProjeEntities1())
                {
                    var article = DB.Articles.SingleOrDefault(c => c.ID == articleId);
                    Console.WriteLine(articleId);

                    if (article != null)
                    {
                        var web = new HtmlWeb();
                        var doc = web.Load(article.TitleUrl);

                        HtmlNodeCollection contentNoddes = doc.DocumentNode.SelectNodes("//div[@class='bdaia-post-content']/p");
                        HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//figure/img");
                        if(contentNoddes == null)
                        {
                            Console.WriteLine("Press ENTER For Exit");
                            Console.ReadLine();
                        }

                        List<string> content = new List<string>();
                        foreach (HtmlNode htmlNode in contentNoddes)
                        {
                            content.Add(htmlNode.InnerText);
                        }
                        article.FullContent = string.Join("\n", content);
                        article.VisitCheck = true;
                        if (imageNode != null)
                        {
                            article.ImageSrc = imageNode.Attributes["src"].Value;
                        }
                        DB.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("Program Finished");
                        Environment.Exit(0);
                    }
                }
            }
            catch(ArgumentNullException)
            {
                Console.WriteLine("NULL Error!");
            }
        }

        public static void TakeCategoriesLinks()
        {
            using(var db = new nesneProjeEntities1())
            {
                var linkForms = "";
                var web = new HtmlWeb();
                var doc = web.Load(linkForms);

                HtmlNodeCollection categories = doc.DocumentNode.SelectNodes("//li[contains(@class, 'cat-item cat-item-')]/a[not (property[@href=''])]");
           
                    foreach (HtmlNode links in categories)
                    {
                        var href = links.Attributes["href"].Value;
                                
                        var linkOfCategory = href;
                        var web2 = new HtmlWeb();
                        var doc2 = web2.Load(linkOfCategory);

                        HtmlNode pageCount = doc2.DocumentNode.SelectSingleNode("//span[@class='pages']");
                        if (pageCount != null)
                        {
                            var pages = pageCount.InnerText;

                            Regex regex = new Regex(@"Page (?<start>\d+) of (?<end>\d+)");
                            Match match = regex.Match(pages);
                            //if category has many pages
                            if (match.Success)
                            {
                                var url = new PageURL()
                                {
                                    LinkFormat = href + "page/",
                                    LastPage = Convert.ToInt32(match.Groups["start"].Value),
                                    TotalPage = Convert.ToInt32(match.Groups["end"].Value)
                                };
                                db.PageURLs.Add(url);
                            }
                            //if category has only one page
                            else
                            {
                                var url = new PageURL()
                                {
                                    LinkFormat = href + "page/",
                                    LastPage = 1,
                                    TotalPage = 1
                                };
                                db.PageURLs.Add(url);
                            }
                        
                        db.SaveChanges();
                        }
                        else
                        {
                            var url = new PageURL()
                            {
                                LinkFormat = href+"page/",
                                LastPage = 1,
                                TotalPage =1
                            };
                            db.PageURLs.Add(url);
                        }
                    db.SaveChanges();
                    }
            }
        }

        //-------PDF---------\\
        public static void DownloadImages()
        {
            using (var db = new nesneProjeEntities1())
            {
                var imageLinks = db.Articles.ToList();

                foreach(var links in imageLinks)
                {
                    if(links.ImageSrc != null)
                    {
                        string str = links.ImageSrc;
                        int last = str.LastIndexOf("/");
                        string str2 = str.Substring(last);

                        WebClient web = new WebClient();
                        web.DownloadFile(str, @"D:\images\" + str2);
                    }

                }
            }
        }

        public static void CreatePdf()
        {
            using(var db = new nesneProjeEntities1())
            {
                for (int i = 242; i < 259; i++)
                {
                    var categories = db.ArticleCategories.ToList();
                    string cat = db.ArticleCategories.Where(c => c.ID == i).Select(c => c.CategoryName).First();
                    cat = cat.Replace("&nbsp;", "");
                    cat = cat.Replace("&#8217", "'");
                    cat = cat.Replace("&#8230", "");
                    cat = cat.Replace("&amp;", "");

                    FileStream fs = new FileStream(cat + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None);
                    Document doc = new Document(PageSize.A4, 60f, 60f, 10f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                    var imageLinks = db.Articles.ToList();
                    doc.Open();

                    for(int i2 = 242; i2 < 259; i2++)
                    {
                        var categoryMenu = db.ArticleCategories.Where(c => c.ID == i2).Select(c => c.CategoryName).First();
                        int categoryId = db.ArticleCategories.Where(c => c.ID == i2).Select(c => c.ID).First();
                        int count = db.ArticleCategoryRelations.Where(c => c.CategoryID == categoryId).Select(c => c.ArticleID).Count();

                        string trueTitle = categoryMenu;
                        trueTitle = trueTitle.Replace("&amp;", "");
                        char pad = '*';
                        doc.Add(new Paragraph("- " + trueTitle + count.ToString().PadLeft(45, pad) + " Articles"));
                    }
                    doc.NewPage();

                    BaseFont bf = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "windows-1254", true);
                    Font font = new Font(bf, 12, Font.NORMAL);
                    Font boldFont = new Font(bf, 18, Font.BOLD);

                    int id = db.ArticleCategories.Where(c => c.ID == i).Select(c => c.ID).First();
                    List<int> relation = db.ArticleCategoryRelations.Where(c => c.CategoryID == id).Select(c => c.ArticleID).ToList();

                    foreach (var rel in relation)
                    {
                        var articles = db.Articles.Where(c => c.ID == rel).ToList();
                        foreach (var article in articles)
                        {
                            if (article.ImageSrc != null)
                            {
                                string fixedContent = article.FullContent;
                                fixedContent = fixedContent.Replace("&nbsp;", "");
                                fixedContent = fixedContent.Replace("&#8217;", "'");
                                fixedContent = fixedContent.Replace("&#8220;", "");
                                fixedContent = fixedContent.Replace("&#8221;", "");

                                string fixedTitle = article.Title;
                                fixedTitle = fixedTitle.Replace("&#8217;", "'");
                                fixedTitle = fixedTitle.Replace("&#8230;", "");
                                fixedTitle = fixedTitle.Replace("&nbsp;", "");
                                fixedTitle = fixedTitle.Replace("&#038;", "");

                                string str = article.ImageSrc;
                                int last = str.LastIndexOf("/");
                                string str2 = str.Substring(last);
                                var image = iTextSharp.text.Image.GetInstance(@"D:\images\" + str2);

                                Chunk c = new Chunk(fixedTitle, boldFont);

                                var para = new Paragraph(c);
                                para.Alignment = Element.ALIGN_CENTER;

                                doc.Add(para);
                                doc.Add(new Paragraph("\n"));
                                image.ScaleAbsolute(450f, 300f);
                                image.Alignment = Element.ALIGN_CENTER;
                                doc.Add(image);
                                doc.Add(new Paragraph("\n"));
                                Chunk c2 = new Chunk(fixedContent, font);
                                doc.Add(new Paragraph(c2));
                                doc.Add(new Paragraph("\n\n"));
                                doc.NewPage();
                            }
                        }
                    }
                    doc.Close();
                    fs.Close();
                }
            }
        }
    }
}