using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cerebro.Models.TargetProcess
{
    public class Assignable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Effort { get; set; }
        public ListResponse<Time> Times { get; set; }
    }
}