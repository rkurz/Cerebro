using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cerebro.Models;
using Cerebro.Models.TargetProcess;
using Cerebro.DataFactories;
using Cerebro.ViewModels;

namespace Cerebro.Services
{
    public class StatusBoardService
    {
        public static StatusBoardViewModel BuildStatusBoardViewModel()
        {
            var model = new StatusBoardViewModel();

            var currentIteration = TargetProcessFactory.GetCurrentIteration();
            if (currentIteration == null)
                return model;

            model.BurndownChart = GetBurndownChart(currentIteration);
            model.TaskList = GetTaskList(currentIteration);
            model.TestCaseSummary = GetTestCaseSummary(currentIteration);
            return model;
        }

        private static TestCaseSummary GetTestCaseSummary(Iteration iteration)
        {
            var testCases = TargetProcessFactory.GetTestCases(iteration);
            var passedCount = testCases.Count(tc => tc.LastStatus == "True");
            var failedCount = testCases.Count(tc => tc.LastStatus == "False");
            var notRunCount = testCases.Count(tc => tc.LastStatus == null);
            return new TestCaseSummary
                        {
                            PassedCount = passedCount,
                            FailedCount = failedCount,
                            NotRunCount = notRunCount
                        };
        }

        private static List<TaskListItem> GetTaskList(Iteration iteration)
        {
            var report = new List<TaskListItem>();
            var userStories = TargetProcessFactory.GetUserStoriesForCurrentIteration(iteration);

            foreach (var story in userStories)
            {
                report.Add(new TaskListItem
                                {
                                    TaskId = story.Id,
                                    Name = story.Name,
                                    Status = GetTaskStatus(story.EntityState.Name)
                                });
            }
            return report;
        }

        private static TaskStatus GetTaskStatus(string entityStateName)
        {
            switch (entityStateName)
            {
                case "Pending":
                    return TaskStatus.NotStarted;
                case "Complete":
                    return TaskStatus.Done;
                default:
                    return TaskStatus.InProgress;
            }
        }

        private static ChartViewModel GetBurndownChart(Iteration iteration)
        {
            var tasks = TargetProcessFactory.GetTasksForCurrentIteration();
            return ChartService.BuildBurndownChart(iteration, tasks);
        }
    }
}