using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cerebro.Models.TargetProcess
{
    public class ListResponse<T>
    {
        public List<T> Items { get; set; }
    }
}