using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities.Models;

namespace HairArt.Infrastructure.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "user-name")]
    public class UserRoleTagHelper : TagHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        [HtmlAttributeName("user-name")]
        public string? UserName { get; set; }

        public UserRoleTagHelper(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                output.Content.SetContent("No user specified");
                return;
            }

            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                output.Content.SetContent("User not found");
                return;
            }

            var roles = await _userManager.GetRolesAsync(user);
            TagBuilder ul = new TagBuilder("ul");

            foreach (var role in roles)
            {
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml.Append(role);
                ul.InnerHtml.AppendHtml(li);
            }

            output.Content.AppendHtml(ul);
        }
    }
}
