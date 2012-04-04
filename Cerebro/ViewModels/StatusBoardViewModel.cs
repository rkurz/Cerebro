using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dotnetCHARTING;

namespace Cerebro.ViewModels
{
    public class StatusBoardViewModel
    {
        public Chart BurndownChart { get; set; }
        public List<TaskListItem> TaskList { get; set; }
        public TestCaseSummary TestCaseSummary { get; set; }
    }
}