using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dotnetCHARTING;
using System.Web.UI;

namespace Cerebro.Helpers
{
    public static class DotnetchartingHelper
    {
        public static string DotNetChart(this HtmlHelper helper, Chart chart)
        {
            var sb = new System.Text.StringBuilder();
            using (var sw = new System.IO.StringWriter(sb))
            {
                using (var tw = new HtmlTextWriter(sw))
                {
                    chart.RenderControl(tw);
                }
            }
            return sb.ToString();
        }
    }
}