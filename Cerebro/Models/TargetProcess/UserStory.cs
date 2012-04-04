using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cerebro.Models.TargetProcess
{
    public class UserStory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Effort { get; set; }
        public double EffortCompleted { get; set; }
        public double EffortToDo { get; set; }
        public EntityState EntityState { get; set; }
    }
}