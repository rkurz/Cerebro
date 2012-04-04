using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cerebro.Models.TargetProcess
{
    public class TestCase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastStatus { get; set; }
        public DateTime? LastRunDate { get; set; }
    }
}