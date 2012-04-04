using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cerebro.ViewModels
{
    public class TaskListItem
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
    }
}