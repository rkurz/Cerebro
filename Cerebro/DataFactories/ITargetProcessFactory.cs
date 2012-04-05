using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cerebro.Models.TargetProcess;

namespace Cerebro.DataFactories
{
    /// <summary>
    /// Interface to the TargetProcess Web Service API
    /// </summary>
    public interface ITargetProcessFactory
    {
        Iteration GetCurrentIteration();
        List<UserStory> GetUserStoriesForCurrentIteration(Iteration iteration);
        List<Assignable> GetTasksForCurrentIteration();
        List<TestCase> GetTestCases();
        List<TestCase> GetTestCases(Iteration iteration);
    }
}