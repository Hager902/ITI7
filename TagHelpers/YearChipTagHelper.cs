using Microsoft.AspNetCore.Razor.TagHelpers;

namespace StudentPortalWeb.TagHelpers
{
    [HtmlTargetElement("year-chip")]
    public class YearChipTagHelper : TagHelper
    {
        public int For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;

            // Lab ID 15: Chip year = 4, Chip level = Final
            const int CHIP_YEAR = 4;
            const string CHIP_LABEL = "Final";

            string text;
            string colorClass;

            if (For == CHIP_YEAR)
            {
                text = CHIP_LABEL;
                colorClass = "bg-warning text-dark";
            }
            else
            {
                text = $"Year {For}";
                colorClass = "bg-light text-dark";
            }

            output.Attributes.SetAttribute("class", $"badge {colorClass}");
            output.Attributes.SetAttribute("title", "rendered by hager");
            output.Content.SetContent(text);
        }
    }
}