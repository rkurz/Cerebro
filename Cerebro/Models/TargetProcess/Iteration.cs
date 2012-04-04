using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cerebro.Models.TargetProcess
{
    public class Iteration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ListResponse<UserStory> UserStories { get; set; }
        public ListResponse<Bug> Bugs { get; set; }
        public ListResponse<Task> Tasks { get; set; }
    }
}