using DataLibrary.DTO;
using DataLibrary.StatePattern;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using MMSAPI.Controllers;
using MMSAPI.Repository;
using MMSAPI.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MMSApi.Tests.Tasks
{
    public class TaskControllerTests
    {
        private ITaskRepository _taskRepository;
        private ISprintRepository _sprintRepository;
        private IUserRepository _userRepository;
        private TaskController _taskController;

        public IEntityUpdateHandler _entityUpdateHandler { get; }

        public TaskControllerTests()
        {
            _taskRepository = A.Fake<ITaskRepository>();
            _sprintRepository = A.Fake<ISprintRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _entityUpdateHandler =A.Fake<IEntityUpdateHandler>();
            _taskController = A.Fake<TaskController>();
        }

        [Fact]
        public async Task Create_Task_Response_Ok()
        {
            TaskDTO task = new TaskDTO
            {
                Id = Guid.NewGuid().ToString(),
                Name = "test",
                Description = "DescriptioTest",
                Status = "Status",
                Username = "UsernameTest",
            };
            var response = _taskController.Create(task);
            var httpResponse = response as OkObjectResult;
            Assert.Equal(200, httpResponse.StatusCode);
        }
    }
}
