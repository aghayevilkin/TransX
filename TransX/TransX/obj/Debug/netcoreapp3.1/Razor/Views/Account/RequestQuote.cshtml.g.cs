#pragma checksum "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c3a1367620de77aed281bb098fef2a9fb639b3f5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_RequestQuote), @"mvc.1.0.view", @"/Views/Account/RequestQuote.cshtml")]
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
#line 1 "C:\Users\ASUS\source\repos\TransX\TransX\Views\_ViewImports.cshtml"
using TransX;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ASUS\source\repos\TransX\TransX\Views\_ViewImports.cshtml"
using TransX.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ASUS\source\repos\TransX\TransX\Views\_ViewImports.cshtml"
using TransX.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ASUS\source\repos\TransX\TransX\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c3a1367620de77aed281bb098fef2a9fb639b3f5", @"/Views/Account/RequestQuote.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ac2758f4210ec4c22698ca1d316fa9c8c097c1d", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_RequestQuote : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<VmProfile>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_DashboardNavbar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onclick", new global::Microsoft.AspNetCore.Html.HtmlString("return confirm(\'Are you sure?\')"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RequestQuoteDelete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("delete"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
  
    ViewData["Title"] = "My Request a Quote";
    ViewBag.Section = "requestquote";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""page-title"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-12 col-md-12"">

                <h2 class=""ipt-title"">Welcome!</h2>
                <span class=""ipn-subtitle"">Welcome To Your Account</span>

            </div>
        </div>
    </div>
</div>
<section class=""bg-light my-5"">
    <div class=""container-fluid"">

        <div class=""row"">

            <div class=""col-lg-3 col-md-12"">

                <div class=""simple-sidebar sm-sidebar"" id=""filter_search"">

                    <div class=""search-sidebar_header"">
                        <h4 class=""ssh_heading"">Close Filter</h4>
                        <button onclick=""closeFilterSearch()"" class=""w3-bar-item w3-button w3-large""><i class=""ti-close""></i></button>
                    </div>

                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c3a1367620de77aed281bb098fef2a9fb639b3f56333", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                </div>
            </div>

            <div class=""col-lg-9 col-md-12"">
                <div class=""dashboard-wraper"">

                    <!-- Bookmark Property -->
                    <div class=""form-submit"">
                    </div>

                    <table class=""property-table-wrap responsive-table bkmark"">

                        <tbody>
                            <tr>
                                <th><i class=""fa fa-file-text""></i> My List of Request a Quote</th>
                                <th></th>
                            </tr>

");
#nullable restore
#line 53 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
                             if (Model.RequestQuotes.Count != 0)
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 55 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
                                 foreach (var item in Model.RequestQuotes)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                    <tr>
                                        <td class=""property-container"">
                                            <div class=""title"">
                                                <svg class=""icon request-icon"">
                                                    <use xlink:href=""#pin""></use>
                                                </svg>
                                                <h4 class=""request-h4"">From: ");
#nullable restore
#line 63 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
                                                                        Write(item.From);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
                                                <br />
                                                <svg class=""icon request-icon"">
                                                    <use xlink:href=""#pin""></use>
                                                </svg>
                                                <h4 class=""request-h4"">To: ");
#nullable restore
#line 68 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
                                                                      Write(item.To);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h4>
                                                <br />
                                                <svg class=""icon request-icon"">
                                                    <use xlink:href=""#calendar_2""></use>
                                                </svg>
                                                <h4 class=""request-h4"">To: ");
#nullable restore
#line 73 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
                                                                      Write(item.When.ToString("dd MMMM yyyy | H:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                                                <br />\r\n                                                <span class=\"table-property-price\">Transport: ");
#nullable restore
#line 75 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
                                                                                         Write(item.Service.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                            </div>\r\n                                        </td>\r\n                                        <td class=\"action\">\r\n                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c3a1367620de77aed281bb098fef2a9fb639b3f511418", async() => {
                WriteLiteral("<i class=\"ti-close\"></i> Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 79 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
                                                                                                                                                    WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                                        </td>\r\n                                    </tr>\r\n");
#nullable restore
#line 83 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 83 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
                                 
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td>List of request a quote is empty</td>\r\n                                </tr>\r\n");
#nullable restore
#line 90 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\RequestQuote.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                        </tbody>\r\n                    </table>\r\n\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<VmProfile> Html { get; private set; }
    }
}
#pragma warning restore 1591
