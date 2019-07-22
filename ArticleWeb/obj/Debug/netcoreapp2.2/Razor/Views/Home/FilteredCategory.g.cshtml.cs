#pragma checksum "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e2712d38b5f4b28ba9c386e774680d97b032c556"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_FilteredCategory), @"mvc.1.0.view", @"/Views/Home/FilteredCategory.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/FilteredCategory.cshtml", typeof(AspNetCore.Views_Home_FilteredCategory))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e2712d38b5f4b28ba9c386e774680d97b032c556", @"/Views/Home/FilteredCategory.cshtml")]
    public class Views_Home_FilteredCategory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ArticleSpiderWeb.Models.Home.IndexModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
  
    Layout = "/Views/Shared/_Layout.cshtml";


#line default
#line hidden
            BeginContext(103, 35, true);
            WriteLiteral("\r\n    <div class=\"row\">\r\n\r\n        ");
            EndContext();
            BeginContext(139, 46, false);
#line 9 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
   Write(await Component.InvokeAsync("CategorySideBar"));

#line default
#line hidden
            EndContext();
            BeginContext(185, 65, true);
            WriteLiteral("\r\n\r\n        <div class=\"col-8 border-left-0 border-right pb-3\">\r\n");
            EndContext();
#line 12 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
             foreach (ArticleSpiderWeb.Models.Home.ArticleItem article in Model.Articles)
            {

#line default
#line hidden
            BeginContext(356, 105, true);
            WriteLiteral("                <div class=\"row\">\r\n                    <div class=\"col-12\">\r\n                        <h3>");
            EndContext();
            BeginContext(462, 13, false);
#line 16 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
                       Write(article.Title);

#line default
#line hidden
            EndContext();
            BeginContext(475, 34, true);
            WriteLiteral("</h3>\r\n                        <p>");
            EndContext();
            BeginContext(510, 25, false);
#line 17 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
                      Write(Html.Raw(article.Summary));

#line default
#line hidden
            EndContext();
            BeginContext(535, 206, true);
            WriteLiteral("</p>\r\n                    </div>\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col-12 text-right\">\r\n                        <a class=\"btn btn-outline-dark my-3\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 741, "\"", 779, 2);
            WriteAttributeValue("", 748, "/Home/ArticleDetail/", 748, 20, true);
#line 22 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
WriteAttributeValue("", 768, article.ID, 768, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(780, 132, true);
            WriteLiteral("><i class=\"fas fa-angle-double-right\"></i></a>\r\n                        <hr />\r\n                    </div>\r\n                </div>\r\n");
            EndContext();
#line 26 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
            }

#line default
#line hidden
            BeginContext(927, 283, true);
            WriteLiteral(@"        </div>
    </div>
    <div class=""row"">
        <div class=""col""></div>
        <div class=""col-8"">
            <div class=""col-4""></div>
            <div class=""col-6"">
                <div class=""input-group"">
                    <div class=""input-group-prepend"">
");
            EndContext();
#line 36 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
                         if (Model.HasPreviousPage)
                        {

#line default
#line hidden
            BeginContext(1290, 59, true);
            WriteLiteral("                            <a class=\"btn btn-outline-dark\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 1349, "\"", 1374, 1);
#line 38 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
WriteAttributeValue("", 1356, Model.PreviousURL, 1356, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1375, 15, true);
            WriteLiteral(">Previous</a>\r\n");
            EndContext();
#line 39 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
                        }

#line default
#line hidden
            BeginContext(1417, 130, true);
            WriteLiteral("                    </div>\r\n                    <select class=\"custom-select mr-2 ml-2\" onchange=\"top.location.href=this.value\">\r\n");
            EndContext();
#line 42 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
                         foreach(ArticleSpiderWeb.Models.Home.ButtonItem button in Model.Buttons)
                        {

#line default
#line hidden
            BeginContext(1673, 35, true);
            WriteLiteral("                            <option");
            EndContext();
            BeginWriteAttribute("value", " value=\"", 1708, "\"", 1727, 1);
#line 44 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
WriteAttributeValue("", 1716, button.URL, 1716, 11, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1728, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(1731, 35, false);
#line 44 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
                                                    Write(button.IsSelected ? "selected" : "");

#line default
#line hidden
            EndContext();
            BeginContext(1767, 1, true);
            WriteLiteral(">");
            EndContext();
            BeginContext(1769, 11, false);
#line 44 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
                                                                                          Write(button.Text);

#line default
#line hidden
            EndContext();
            BeginContext(1780, 11, true);
            WriteLiteral("</option>\r\n");
            EndContext();
#line 45 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
                        }

#line default
#line hidden
            BeginContext(1818, 85, true);
            WriteLiteral("                    </select>\r\n                    <div class=\"input-group-append\">\r\n");
            EndContext();
#line 48 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
                        if (Model.HasNextPage)
                       {

#line default
#line hidden
            BeginContext(1977, 58, true);
            WriteLiteral("                           <a class=\"btn btn-outline-dark\"");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2035, "\"", 2060, 1);
#line 50 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
WriteAttributeValue("", 2042, Model.NextPageURL, 2042, 18, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2061, 11, true);
            WriteLiteral(">Next</a>\r\n");
            EndContext();
#line 51 "D:\C#\EFGetStarted.AspNetCore.ExistingDb\EFGetStarted.AspNetCore.ExistingDb\Views\Home\FilteredCategory.cshtml"
                       }

#line default
#line hidden
            BeginContext(2098, 100, true);
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ArticleSpiderWeb.Models.Home.IndexModel> Html { get; private set; }
    }
}
#pragma warning restore 1591