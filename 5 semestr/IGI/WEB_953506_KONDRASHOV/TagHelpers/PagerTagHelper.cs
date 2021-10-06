using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_953506_KONDRASHOV.TagHelpers
{
    public class PagerTagHelper : TagHelper
    {
        private LinkGenerator linkGenerator;

        public int PageCurrent { get; set; }
        public int PageTotal { get; set; }
        public string PagerClass { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public int? GroupId { get; set; }

        public PagerTagHelper(LinkGenerator linkGenerator)
        {
            this.linkGenerator = linkGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "nav";

            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination");
            ulTag.AddCssClass(PagerClass);

            for (int i = 1; i <= PageTotal; i++)
            {
                var url = linkGenerator.GetPathByAction(Action, Controller, new { pageNo = i, group = GroupId == 0 ? null : GroupId });

                var item = GetPagerItem(url: url, text: i.ToString(), active: i == PageCurrent, disabled: i == PageCurrent);

                ulTag.InnerHtml.AppendHtml(item);
            }

            output.Content.AppendHtml(ulTag);
        }

        /// <summary>
        /// Генерирует разметку одной кнопки пейджера 
        /// </summary>
        /// <param name="url">адрес</param>
        /// <param name="text">текст кнопки пейджера</param>
        /// <param name="active">признак текущей страницы</param>
        /// <param name="disabled">запретить доступ к кнопке</param>
        /// <returns>объект класса TagBuilder</returns>
        /// 

        private TagBuilder GetPagerItem(string url, string text, bool active = false, bool disabled = false)
        {
            var liTag = new TagBuilder("li");
            liTag.AddCssClass("page-item");
            liTag.AddCssClass(active ? "active" : "");

            var aTag = new TagBuilder("a");
            aTag.AddCssClass("page-link");
            aTag.Attributes.Add("href", url);
            aTag.InnerHtml.Append(text);

            liTag.InnerHtml.AppendHtml(aTag);

            return liTag;
        }
    }
}
