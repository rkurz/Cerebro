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
        private ITargetProcessFactory _targetProcessFactory;

        public StatusBoardService(ITargetProcessFactory targetProcessFactory)
        {
            _targetProcessFactory = targetProcessFactory;
        }

        public StatusBoardViewModel BuildStatusBoardViewModel()
        {
            var model = new StatusBoardViewModel();

            var currentIteration = _targetProcessFactory.GetCurrentIteration();
            if (currentIteration == null)
                return model;

            model.BurndownChart = GetBurndownChart(currentIteration);
            model.TaskList = GetTaskList(currentIteration);
            model.TestCaseSummary = GetTestCaseSummary(currentIteration);
            return model;
        }

        private TestCaseSummary GetTestCaseSummary(Iteration iteration)
        {
            var testCases = _targetProcessFactory.GetTestCases(iteration);
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

        private List<TaskListItem> GetTaskList(Iteration iteration)
        {
            var report = new List<TaskListItem>();
            var userStories = _targetProcessFactory.GetUserStoriesForCurrentIteration(iteration);

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

        private TaskStatus GetTaskStatus(string entityStateName)
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

        private ChartViewModel GetBurndownChart(Iteration iteration)
        {
            var tasks = _targetProcessFactory.GetTasksForCurrentIteration();
            return ChartService.BuildBurndownChart(iteration, tasks);
        }
    }
}