using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cerebro.ViewModels
{
    public class TestCaseSummary
    {
        public int PassedCount { get; set; }
        public int FailedCount { get; set; }
        public int NotRunCount { get; set; }
    }
}