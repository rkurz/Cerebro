using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Cerebro.Models;
using Cerebro.Utilities;

namespace Cerebro.DataFactories
{
    public class TargetProcessFactory
    {
        public static Iteration GetCurrentIteration()
        {
            var serializer = new JavaScriptSerializer();
            var url = string.Format("http://creativeop.tpondemand.com/api/v1/Projects/2648/Iterations/?where=(StartDate lte '{0:s}') and (EndDate gte '{0:s}')&include=[Name, StartDate, EndDate, UserStories[Name, EntityState]]", DateTime.Now.ToString("MM/dd/yyyy"));
            //var response = HttpUtilities.HttpGet("http://creativeop.tpondemand.com/api/v1/Projects/2648/Iterations/?where=(StartDate lte '2012-2-25') and (EndDate gte '2012-2-25')&include=[Name, StartDate, EndDate, UserStories[Name, EntityState]]");
            var response = HttpUtilities.HttpGet(url);
            try
            {
                var iterations = serializer.Deserialize<ListResponse<Iteration>>(response);
                return iterations.Items.Count > 0 ? iterations.Items[0] : null;
            }
            catch
            {
                return null;
            }
        }

        public static List<Assignable> GetTasksForCurrentIteration()
        {
            var serializer = new JavaScriptSerializer();
            var url = string.Format("http://creativeop.tpondemand.com/api/v1/Assignables?include=[Times[Remain,Spent,Date],Id,Name,Description,Effort,EffortCompleted,EffortToDo,EntityType,Iteration[Name,StartDate,EndDate]]&where=(Project.Id eq 2648) and (Iteration.StartDate lte '{0:s}') and (Iteration.EndDate gte '{0:s}')", DateTime.Now.ToString("MM/dd/yyyy"));
            //var response = HttpUtilities.HttpGet("http://creativeop.tpondemand.com/api/v1/Assignables?include=[Times[Remain,Spent,Date],Id,Name,Description,Effort,EffortCompleted,EffortToDo,EntityType,Iteration[Name,StartDate,EndDate]]&where=(Project.Id eq 2648) and (Iteration.StartDate lte '2012-2-25') and (Iteration.EndDate gte '2012-2-25')");
            var response = HttpUtilities.HttpGet(url);
            try
            {
                var tasks = serializer.Deserialize<ListResponse<Assignable>>(response);
                return tasks.Items;
            }
            catch
            {
                return new List<Assignable>();
            }
        }

        public static List<TestCase> GetTestCases()
        {
            var serializer = new JavaScriptSerializer();
            var response = HttpUtilities.HttpGet("http://creativeop.tpondemand.com/api/v1/Projects/2648/TestCases?include=[Name,LastStatus,LastRunDate]&take=10000");
            try
            {
                var testCases = serializer.Deserialize<ListResponse<TestCase>>(response);
                return testCases.Items;
            }
            catch
            {
                return new List<TestCase>();
            }
        }
    }
}