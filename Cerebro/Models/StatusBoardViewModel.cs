using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dotnetCHARTING;

namespace Cerebro.Models
{
    public class StatusBoardViewModel
    {
        public Chart BurndownChart { get; set; }
        public List<TaskListItem> TaskList { get; set; }
        public TestCaseSummary TestCaseSummary { get; set; }
    }

    public class TaskListItem
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
    }

    public class TestCaseSummary
    {
        public int PassedCount { get; set; }
        public int FailedCount { get; set; }
        public int NotRunCount { get; set; }
    }

    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Done
    }
}