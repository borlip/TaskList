using System;
using System.Data.Entity.Infrastructure;
using NUnit.Framework;
using TaskList.Core.Concrete;
using TaskList.Core.Entities;
using System.Linq;

namespace TaskList.Tests.Core.Concrete
{
    [TestFixture]
    [Category("IntegrationTests")]
    public class TaskRepositoryTests
    {
        private TaskListDbContext _context;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            _context = new TaskListDbContext();
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            _context.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            ClearDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            ClearDatabase();
        }

        private void ClearDatabase()
        {
            var objectContext = ((IObjectContextAdapter)_context).ObjectContext;
            objectContext.ExecuteStoreCommand("TRUNCATE TABLE Tasks");
        }

        [Test]
        public void ShouldAddAndRetrieveTask()
        {
            var repository = new TaskRepository(_context);

            repository.AddNewTask(new Task { Name = "Task1", DueDate = DateTime.Today, Priority = Priority.High });

            var task = repository.Tasks.Single(t => t.Name == "Task1");
            Assert.AreNotEqual(0, task.RecordId);
        }
    }
}