using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Rhino.Mocks;
using TaskList.Core.Concrete;
using TaskList.Core.Entities;
using TaskList.Web.Controllers;
using TaskList.Web.Helpers;

namespace TaskList.Tests.Web.Controllers
{
    [TestFixture]
    public class TaskListControllerTests
    {
        private ITaskRepository _taskRepository;
        private TaskListController _controller;
        private static readonly DateTime _today = new DateTime(2012, 5, 12);

        [SetUp]
        public void SetUp()
        {
            _taskRepository = MockRepository.GenerateStub<ITaskRepository>();
            _controller = new TaskListController(_taskRepository);
        }

        [Test]
        public void Index_NoTaskInRepository_ShouldReturnEmptyModel()
        {
            // Arrange
            _taskRepository
                .Stub(r => r.Tasks)
                .Return(new List<Task>());

            // Act
            var viewResult = _controller.Index();

            // Assert
            Assert.That(viewResult.Model, Is.Empty);
        }

        [Test]
        public void Index_TasksInRepository_ShouldReturnModelWithTasks()
        {
            // Arrange
            var tasks = new List<Task>(3)
                            {
                                new Task{RecordId = 1, Name = "Task1", DueDate = _today, Priority = Priority.High},
                                new Task{RecordId = 33, Name = "Task2", DueDate = _today, Priority = Priority.Low},
                                new Task{RecordId = 44, Name = "Task3", DueDate = _today, Priority = Priority.Normal}
                            };
            _taskRepository
                .Stub(r => r.Tasks)
                .Return(tasks);

            // Act
            var viewResult = (IEnumerable<Task>)_controller.Index().Model;

            // Assert
            Assert.That(viewResult.SequenceEqual(tasks, TasksComparer.Instance), Is.True);
        }

        [Test]
        public void NewTask_HttpGet_ShouldCreateModelWithEmptyTask()
        {
            // Arrange
            DateTimeUtils.Today = () => _today;
            // Act
            var model = (Task)_controller.NewTask().Model;

            // Assert
            Assert.That(model.RecordId, Is.EqualTo(0));
            Assert.That(model.Name, Is.Null);
            Assert.That(model.Priority, Is.EqualTo(Priority.Normal));
            Assert.That(model.DueDate, Is.EqualTo(_today));
        }

        [Test]
        public void NewTask_HttpPostAndModelIsValid_ShouldAddNewTaskIntoRepository()
        {
            // Arrange
            var task = new Task { RecordId = 33, Name = "Task2", DueDate = _today, Priority = Priority.Low };
            // Act
            _controller.NewTask(task);

            // Assert
            _taskRepository.AssertWasCalled(r => r.AddNewTask(task));
        }

        [Test]
        public void NewTask_HttpPostAndModelIsValid_ShouldRedirectToActionIndex()
        {
            // Arrange
            var task = new Task { RecordId = 33, Name = "Task2", DueDate = _today, Priority = Priority.Low };
            // Act
            var result = _controller.NewTask(task);

            // Assert
            var redirectToRouteResult = (RedirectToRouteResult)result;
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues.Values.Single());
        }

        [Test]
        public void NewTask_HttpPostAndModelIsInvalid_ShouldReturnViewResultWithSameTask()
        {
            // Arrange
            var task = new Task { RecordId = 0, Name = "Task2", DueDate = _today, Priority = Priority.Low };
            _controller.ModelState.AddModelError("blah", new Exception());
            // Act
            var result = (ViewResult)_controller.NewTask(task);

            // Assert
            Assert.AreEqual("Task2", ((Task)result.Model).Name);
        }

        [Test]
        public void OnException_WhenExceptionIsThrown_ShouldBeHandledProperly()
        {
            // Arrange
            _taskRepository.Stub(r => r.AddNewTask(null)).IgnoreArguments().Throw(new Exception());
            // Act
            var exceptionContext = new ExceptionContext();
            ((IExceptionFilter)_controller).OnException(exceptionContext);

            // Assert
            Assert.IsTrue(exceptionContext.ExceptionHandled);
            var viewResult = (ViewResult) exceptionContext.Result;
            Assert.AreEqual("ErrorPage", viewResult.ViewName);
        }
    }

    public class TasksComparer : IEqualityComparer<Task>
    {
        public static readonly TasksComparer Instance = new TasksComparer();
        public bool Equals(Task x, Task y)
        {
            if (ReferenceEquals(null, y)) return false;
            if (ReferenceEquals(x, y)) return true;
            return y.RecordId == x.RecordId && Equals(y.Name, x.Name) && y.DueDate.Equals(x.DueDate) && y.PriorityValue == x.PriorityValue;
        }

        public int GetHashCode(Task obj)
        {
            return 0;
        }
    }
}