#pragma checksum "D:\BlogProject_Devskill\BlogProject_Devskill.Web\BlogProject_Devskill.Web\Areas\Admin\Views\Shared\_ActionMessage.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e639352670858abe7378c8a1d26f2efd93fd2528"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Shared__ActionMessage), @"mvc.1.0.view", @"/Areas/Admin/Views/Shared/_ActionMessage.cshtml")]
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
#nullable restore
#line 1 "D:\BlogProject_Devskill\BlogProject_Devskill.Web\BlogProject_Devskill.Web\Areas\Admin\Views\_ViewImports.cshtml"
using BlogProject_Devskill.Web.Areas.Admin.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\BlogProject_Devskill\BlogProject_Devskill.Web\BlogProject_Devskill.Web\Areas\Admin\Views\_ViewImports.cshtml"
using BlogProject_Devskill.Web.Areas.Admin.Models.AdminControl;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\BlogProject_Devskill\BlogProject_Devskill.Web\BlogProject_Devskill.Web\Areas\Admin\Views\_ViewImports.cshtml"
using BlogProject_Devskill.Web.Areas.Admin.Models.BlogPostModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e639352670858abe7378c8a1d26f2efd93fd2528", @"/Areas/Admin/Views/Shared/_ActionMessage.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5ffc6283e0567e73773e8013c878906e1e7983ff", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Shared__ActionMessage : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BlogProject_Devskill.Web.Areas.Admin.Models.ResponseModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\BlogProject_Devskill\BlogProject_Devskill.Web\BlogProject_Devskill.Web\Areas\Admin\Views\Shared\_ActionMessage.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div");
            BeginWriteAttribute("class", " class=\"", 100, "\"", 152, 3);
            WriteAttributeValue("", 108, "alert", 108, 5, true);
            WriteAttributeValue(" ", 113, "alert-dismissible", 114, 18, true);
#nullable restore
#line 5 "D:\BlogProject_Devskill\BlogProject_Devskill.Web\BlogProject_Devskill.Web\Areas\Admin\Views\Shared\_ActionMessage.cshtml"
WriteAttributeValue(" ", 131, Model.StyleCssClass, 132, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n        <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>\r\n        <h5><i");
            BeginWriteAttribute("class", " class=\"", 272, "\"", 308, 3);
            WriteAttributeValue("", 280, "icon", 280, 4, true);
            WriteAttributeValue(" ", 284, "fas", 285, 4, true);
#nullable restore
#line 7 "D:\BlogProject_Devskill\BlogProject_Devskill.Web\BlogProject_Devskill.Web\Areas\Admin\Views\Shared\_ActionMessage.cshtml"
WriteAttributeValue(" ", 288, Model.IconCssClass, 289, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></i> ");
#nullable restore
#line 7 "D:\BlogProject_Devskill\BlogProject_Devskill.Web\BlogProject_Devskill.Web\Areas\Admin\Views\Shared\_ActionMessage.cshtml"
                                                    Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        ");
#nullable restore
#line 8 "D:\BlogProject_Devskill\BlogProject_Devskill.Web\BlogProject_Devskill.Web\Areas\Admin\Views\Shared\_ActionMessage.cshtml"
   Write(Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n");
#nullable restore
#line 10 "D:\BlogProject_Devskill\BlogProject_Devskill.Web\BlogProject_Devskill.Web\Areas\Admin\Views\Shared\_ActionMessage.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BlogProject_Devskill.Web.Areas.Admin.Models.ResponseModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
