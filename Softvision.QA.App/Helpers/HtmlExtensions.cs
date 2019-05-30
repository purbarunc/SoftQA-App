using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Softvision.QA.App.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString StripHtml(this HtmlHelper helper, string input)
        {
            var stripedString = Regex.Replace(input, @"<[^>]*>", String.Empty);
            var stripedSpecialChar = Regex.Replace(stripedString, @"[^0-9a-zA-Z\[\]\(\)\""\.]+", " ");
            return MvcHtmlString.Create(stripedSpecialChar);
        }
    }
}