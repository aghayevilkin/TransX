#pragma checksum "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8502f7ba2dc44dc162710cb96d6174f5d967de33"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Index), @"mvc.1.0.view", @"/Views/Account/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8502f7ba2dc44dc162710cb96d6174f5d967de33", @"/Views/Account/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ac2758f4210ec4c22698ca1d316fa9c8c097c1d", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<VmProfile>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_DashboardNavbar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\ASUS\source\repos\TransX\TransX\Views\Account\Index.cshtml"
  
    ViewData["Title"] = "Index";
    ViewBag.Section = "dashboard";

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

<section class=""bg-light"">
    <div class=""container-fluid"">

        <div class=""row"">
            <div class=""col-lg-12 col-md-12"">
                <div class=""filter_search_opt"">
                    <a href=""javascript:void(0);"" onclick=""openFilterSearch()"">Dashboard Navigation<i class=""ml-2 ti-menu""></i></a>
                </div>
            </div>
        </div>

        <div class=""row"">

            <div class=""col-lg-3 col-md-12"">

                <div class=""simple-sidebar sm-sidebar"" id=""filter_search"">

                    <div class=""search-sidebar_header"">
                        <h4 class=""ssh_heading"">Close Filter</h4>
                        <button onclick=""");
            WriteLiteral("closeFilterSearch()\" class=\"w3-bar-item w3-button w3-large\"><i class=\"ti-close\"></i></button>\r\n                    </div>\r\n\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8502f7ba2dc44dc162710cb96d6174f5d967de335167", async() => {
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

                <div class=""row"">
                    <div class=""col-lg-12 col-md-12 col-sm-12"">
                        <br />
                    </div>
                </div>

                <div class=""row"">

                    <div class=""col-lg-4 col-md-6 col-sm-12"">
                        <div class=""dashboard-stat widget-1"">
                            <div class=""dashboard-stat-content""><h4>607</h4> <span>Listings Included</span></div>
                            <div class=""dashboard-stat-icon""><i class=""ti-location-pin""></i></div>
                        </div>
                    </div>

                    <div class=""col-lg-4 col-md-6 col-sm-12"">
                        <div class=""dashboard-stat widget-2"">
                            <div class=""dashboard-stat-content""><h4>102</h4> <span>Listings Remaining</span></div>
                            <div class=""dashboard-stat-ico");
            WriteLiteral(@"n""><i class=""ti-pie-chart""></i></div>
                        </div>
                    </div>

                    <div class=""col-lg-4 col-md-6 col-sm-12"">
                        <div class=""dashboard-stat widget-3"">
                            <div class=""dashboard-stat-content""><h4>70</h4> <span>Featured Included</span></div>
                            <div class=""dashboard-stat-icon""><i class=""ti-user""></i></div>
                        </div>
                    </div>

                    <div class=""col-lg-4 col-md-6 col-sm-12"">
                        <div class=""dashboard-stat widget-4"">
                            <div class=""dashboard-stat-content""><h4>30</h4> <span>Featured Remaining</span></div>
                            <div class=""dashboard-stat-icon""><i class=""ti-location-pin""></i></div>
                        </div>
                    </div>

                    <div class=""col-lg-4 col-md-6 col-sm-12"">
                        <div class=""dashboard-stat widget-5"">
");
            WriteLiteral(@"                            <div class=""dashboard-stat-content""><h4>Unlimited</h4> <span>Images / per listing</span></div>
                            <div class=""dashboard-stat-icon""><i class=""ti-pie-chart""></i></div>
                        </div>
                    </div>

                    <div class=""col-lg-4 col-md-6 col-sm-12"">
                        <div class=""dashboard-stat widget-6"">
                            <div class=""dashboard-stat-content""><h4>2021-02-26</h4> <span>Ends On</span></div>
                            <div class=""dashboard-stat-icon""><i class=""ti-user""></i></div>
                        </div>
                    </div>

                </div>

                <div class=""dashboard-wraper"">

                    <!-- Basic Information -->
                    <div class=""form-submit"">
                        <h4>My Account</h4>
                        <div class=""submit-section"">
                            <div class=""row"">

                              ");
            WriteLiteral(@"  <div class=""form-group col-md-6"">
                                    <label>Your Name</label>
                                    <input type=""text"" class=""form-control"" value=""Shaurya Preet"">
                                </div>

                                <div class=""form-group col-md-6"">
                                    <label>Email</label>
                                    <input type=""email"" class=""form-control"" value=""preet77@gmail.com"">
                                </div>

                                <div class=""form-group col-md-6"">
                                    <label>Your Title</label>
                                    <input type=""text"" class=""form-control"" value=""Web Designer"">
                                </div>

                                <div class=""form-group col-md-6"">
                                    <label>Phone</label>
                                    <input type=""text"" class=""form-control"" value=""123 456 5847"">
                ");
            WriteLiteral(@"                </div>

                                <div class=""form-group col-md-6"">
                                    <label>Address</label>
                                    <input type=""text"" class=""form-control"" value=""522, Arizona, Canada"">
                                </div>

                                <div class=""form-group col-md-6"">
                                    <label>City</label>
                                    <input type=""text"" class=""form-control"" value=""Montquebe"">
                                </div>

                                <div class=""form-group col-md-6"">
                                    <label>State</label>
                                    <input type=""text"" class=""form-control"" value=""Canada"">
                                </div>

                                <div class=""form-group col-md-6"">
                                    <label>Zip</label>
                                    <input type=""text"" class=""form-control"" v");
            WriteLiteral(@"alue=""160052"">
                                </div>

                                <div class=""form-group col-md-12"">
                                    <label>About</label>
                                    <textarea class=""form-control form-control-text "">Maecenas quis consequat libero, a feugiat eros. Nunc ut lacinia tortor morbi ultricies laoreet ullamcorper phasellus semper</textarea>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class=""form-submit"">
                        <h4>Social Accounts</h4>
                        <div class=""submit-section"">
                            <div class=""row"">

                                <div class=""form-group col-md-6"">
                                    <label>Facebook</label>
                                    <input type=""text"" class=""form-control"" value=""https://facebook.com/"">
                                </div>

");
            WriteLiteral(@"                                <div class=""form-group col-md-6"">
                                    <label>Twitter</label>
                                    <input type=""email"" class=""form-control"" value=""https://twitter.com/"">
                                </div>

                                <div class=""form-group col-md-6"">
                                    <label>Google Plus</label>
                                    <input type=""text"" class=""form-control"" value=""https://googleplus.com/"">
                                </div>

                                <div class=""form-group col-md-6"">
                                    <label>LinkedIn</label>
                                    <input type=""text"" class=""form-control"" value=""https://linkedin.com/"">
                                </div>

                                <div class=""form-group col-lg-12 col-md-12"">
                                    <button class=""btn button-style-one"" type=""submit"">Save Changes</button>");
            WriteLiteral("\r\n                                </div>\r\n\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n</section>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<IdentityUser> UserManager { get; private set; }
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
