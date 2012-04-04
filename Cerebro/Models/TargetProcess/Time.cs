using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cerebro.Models.TargetProcess
{
    public class Time
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Remain { get; set; }
        public double Spent { get; set; }
    }
}