using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cerebro.ViewModels
{
    public class StatusBoardViewModel
    {
        public ChartViewModel BurndownChart { get; set; }
        public List<TaskListItem> TaskList { get; set; }
        public TestCaseSummary TestCaseSummary { get; set; }
    }
}