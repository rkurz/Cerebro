using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cerebro.Models
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

    public class UserStory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Effort { get; set; }
        public double EffortCompleted { get; set; }
        public double EffortToDo { get; set; }
        public EntityState EntityState { get; set; }
    }

    public class Bug
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Effort { get; set; }
        public double EffortCompleted { get; set; }
        public double EffortToDo { get; set; }
    }

    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Effort { get; set; }
        public double EffortCompleted { get; set; }
        public double EffortToDo { get; set; }
    }

    public class EntityState
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TestCase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastStatus { get; set; }
        public DateTime? LastRunDate { get; set; }
    }

    public class ListResponse<T>
    {
        public List<T> Items { get; set; }
    }

    public class Assignable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Effort { get; set; }
        public ListResponse<Time> Times { get; set; }
    }

    public class Time
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Remain { get; set; }
        public double Spent { get; set; }
    }

    public class TaskListItem
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
    }

    public class TestCaseSummary
    {
        public int PassedCount { get; set; }
        public int FailedCount { get; set; }
        public int NotRunCount { get; set; }
    }

    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Done
    }
}