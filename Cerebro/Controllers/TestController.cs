using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Cerebro.Models;
using Cerebro.Models.TargetProcess;
using dotnetCHARTING;
using Cerebro.Utilities;
using Cerebro.DataFactories;
using Cerebro.Services;

namespace Cerebro.Controllers
{
    public class TestController : Controller
    {

        //public ActionResult Index()
        //{
        //    //Iteration iteration;
        //    List<Assignable> assignables;
        //    var serializer = new JavaScriptSerializer();
            
        //    //var test = HttpGet("http://creativeop.tpondemand.com/api/v1/Userstories/");
        //    //var test = HttpGet("http://creativeop.tpondemand.com/api/v1/Projects/2648/?include=[Iterations[StartDate, EndDate]]&where=(Iterations[Items[StartDate]] gt '2012-04-04')");
        //    //var test = HttpGet("http://creativeop.tpondemand.com/api/v1/Projects/2648/Iterations/?where=(StartDate lte '2012-2-25') and (EndDate gte '2012-2-25')&include=[Name, StartDate, EndDate, UserStories[Name, Description, Effort, EffortCompleted, EffortToDo, RoleEfforts], Bugs[Name, Description, Effort, EffortCompleted, EffortToDo], Tasks[Name, Description, Effort, EffortCompleted, EffortToDo]]");
        //    var test = HttpUtilities.HttpGet("http://creativeop.tpondemand.com/api/v1/Assignables?include=[Times[Remain,Spent,Date],Id,Name,Description,Effort,EffortCompleted,EffortToDo,EntityType,Iteration[Name,StartDate,EndDate]]&where=(Project.Id eq 2648) and (Iteration.StartDate lte '2012-2-25') and (Iteration.EndDate gte '2012-2-25')");

        //    try
        //    {
        //        var response = serializer.Deserialize<ListResponse<Assignable>>(test);
        //        //iteration = response.Items[0];
        //        assignables = response.Items;
        //    }
        //    catch
        //    {
        //    }

        //    ViewBag.ResponseText = test;
        //    //ViewBag.ResponseText = "Hello World";

        //    //var currentIteration = TargetProcessFactory.GetCurrentIteration();
        //    //var tasks = TargetProcessFactory.GetTasksForCurrentIteration();
        //    var model = StatusBoardService.BuildStatusBoardViewModel();
        //    //model.BurndownChart = GetChart();

        //    return View(model);
        //}

        //private Chart BuildBurndownChart(Iteration iteration, List<Assignable> tasks)
        //{
        //    SeriesCollection sc;
        //    Series s;

        //    sc = new SeriesCollection();

        //    s = new Series();
        //    s.Name = "Effort";
        //    s.Type = SeriesType.Bar;

        //    for (var day = iteration.StartDate.Date; day.Date <= iteration.EndDate.Date; day = day.AddDays(1))
        //    {
        //        var remainingTime = 0.0;
        //        foreach (var task in tasks)
        //        {
        //            if (task.Times.Items.Count == 0)
        //            {
        //                remainingTime += task.Effort;
        //            }
        //            else
        //            {
        //                //var timeEntry = task.Times.Items.Where(t => t.Date.Date == day).OrderByDescending(t => t.Date).FirstOrDefault();
        //                var timeEntry = task.Times.Items.Where(t => t.Date.Date <= day).OrderByDescending(t => t.Date).FirstOrDefault();
        //                if (timeEntry == null)
        //                {
        //                    remainingTime += task.Effort;
        //                }
        //                else
        //                {
        //                    remainingTime += timeEntry.Remain;
        //                }

        //            }
        //        }
        //        s.AddElements(new Element(day.ToShortDateString(), remainingTime));
        //    }

        //    sc.Add(s);

        //    var chart = new Chart
        //                {
        //                    Title = "Burndown",
        //                    Type = ChartType.Combo,
        //                    TempDirectory = "../file/chart"
        //                };
        //    chart.SeriesCollection.Add(sc);
        //    chart.YAxis.Label.Text = "Time Remaining";
        //    chart.LegendBox.Visible = false;
        //    chart.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
        //    chart.TitleBox.Visible = false;
        //    return chart;
        //}

        //TODO - move this to a library somewhere.
        //public static string HttpGet(string url)
        //{
        //    HttpWebRequest request;
        //    HttpWebResponse response;
        //    StreamReader reader;
        //    string data;

        //    request = WebRequest.Create(url) as HttpWebRequest;
        //    request.Credentials = new NetworkCredential("rkurz", "password");
        //    //request.Headers.Add(HttpRequestHeader.Accept, "application/json");
        //    request.Accept = "application/json";
        //    //request.Headers.Add(HttpRequestHeader.Authorization, "Basic");
        //    response = request.GetResponse() as HttpWebResponse;
        //    reader = new StreamReader(response.GetResponseStream());
        //    data = reader.ReadToEnd();
        //    reader.Close();
        //    response.Close();

        //    return data;
        //}

        //public class TargetProcessFactory
        //{
        //    public static Iteration GetCurrentIteration()
        //    {
        //        var serializer = new JavaScriptSerializer();
        //        var response = HttpGet("http://creativeop.tpondemand.com/api/v1/Projects/2648/Iterations/?where=(StartDate lte '2012-2-25') and (EndDate gte '2012-2-25')&include=[Name, StartDate, EndDate]");
        //        try
        //        {
        //            var iterations = serializer.Deserialize<ListResponse<Iteration>>(response);
        //            return iterations.Items.Count > 0 ? iterations.Items[0] : null;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }

        //    public static List<Assignable> GetTasksForCurrentIteration()
        //    {
        //        var serializer = new JavaScriptSerializer();
        //        var response = HttpGet("http://creativeop.tpondemand.com/api/v1/Assignables?include=[Times[Remain,Spent,Date],Id,Name,Description,Effort,EffortCompleted,EffortToDo,EntityType,Iteration[Name,StartDate,EndDate]]&where=(Project.Id eq 2648) and (Iteration.StartDate lte '2012-2-25') and (Iteration.EndDate gte '2012-2-25')");
        //        try
        //        {
        //            var tasks = serializer.Deserialize<ListResponse<Assignable>>(response);
        //            return tasks.Items;
        //        }
        //        catch
        //        {
        //            return new List<Assignable>();
        //        }
        //    }
        //}
    }
}
