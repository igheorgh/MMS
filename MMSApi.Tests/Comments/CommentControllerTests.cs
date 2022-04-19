using DataLibrary;
using DataLibrary.DTO;
using DataLibrary.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using MMSAPI;
using MMSAPI.Controllers;
using MMSAPI.Models;
using MMSAPI.Repository;
using MMSAPI.Validations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace MMSApi.Tests.Comments
{
    public class CommentControllerTests : IClassFixture<TestFixture<Startup>>
    {
        private CommentController Controller { get; }
        private ITaskRepository _taskRepository;
        private ISprintRepository _sprintRepository;
        private IUserRepository _userRepository;
        private ICommentRepository _commentRepository;
        private TaskController _taskController;

        public IEntityUpdateHandler _entityUpdateHandler { get; }

        private List<Comment> Comments = new List<Comment>();
        private List<User> Users = new List<User>();

        private CommentDTO TestComment = new CommentDTO
        {
            Id = Guid.NewGuid().ToString(),
            Description = "Description",
            Date_Posted = DateTime.UtcNow,
            User_Id = Guid.NewGuid().ToString(),
            Task_Id = Guid.NewGuid().ToString(),
        };

        public CommentControllerTests(TestFixture<Startup> fixture)
        {

            _taskRepository = A.Fake<ITaskRepository>();
            _sprintRepository = A.Fake<ISprintRepository>();
            _userRepository = A.Fake<IUserRepository>();
            _entityUpdateHandler = A.Fake<IEntityUpdateHandler>();
            _taskController = A.Fake<TaskController>();

            var commentRepository = new Mock<ICommentRepository>();
            

            Comments = CommentsHelper.GenerateComments(5);
            Users = UserHelpers.GenerateUsers(6);

            var modelComment = TestComment.ToModel();
          //  commentRepository.Setup()

            commentRepository.Setup(c => c.GetAll()).Returns(Comments);
            commentRepository.Setup(c => c.GetById(It.IsAny<string>())).Returns((string id) => Comments.FirstOrDefault(c => c.Id == id));

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetAll()).Returns(Users);
            userRepository.Setup(x => x.GetById(It.IsAny<string>())).Returns((string id) => Users.FirstOrDefault(u => u.Id == id));
            userRepository.Setup(x => x.Edit(It.IsAny<User>())).Returns((User user) => {
                var usrId = Users.FindIndex(u => u.Id == u.Id);
                Users[usrId] = user;
                return user;
            }
            );

            var dbContext = new Mock<MMSContext>();
            dbContext.Setup(x => x.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            Controller = new CommentController(commentRepository.Object, userRepository.Object, null, null);
        }


        [Fact]
        public void GetAllComments()
        {
            //Act
            var result = (Controller.GetAll() as OkObjectResult).Value as List<CommentDTO>;
            // Assert
            Assert.Equal(result.Count(), Comments.Count);
        }

        [Fact]
        public void CreateComment()
        {
            var beforeCount = Comments.Count;
            var newComment = new CommentDTO
            {
                Description = "Description",
                Date_Posted = DateTime.UtcNow,
                User_Id = Users.FirstOrDefault().Id,
                Task_Id = Guid.NewGuid().ToString(),
            };
            //Act
            //var result = (Controller.Create(newComment) as OkObjectResult).Value as CommentDTO;
            var response = Controller.Create(newComment);
            var httpResponse = response as OkObjectResult;
            Assert.Equal(200, httpResponse.StatusCode);
            // Assert
           /* Assert.Equal(result.Description, newComment.Description);
            Assert.Equal(result.Date_Posted, newComment.Date_Posted);
            Assert.Equal(result.User_Id, newComment.User_Id);
            Assert.Equal(result.Task_Id, newComment.Task_Id);

            Assert.Equal(Comments.Count, beforeCount + 1);*/
        }
    }
}
