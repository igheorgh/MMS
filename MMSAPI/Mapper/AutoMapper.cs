using AutoMapper;
using DataLibrary.DTO;
using DataLibrary.Models;

namespace Identity.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Comment, CommentDTO>();
            CreateMap<Sprint, SprintDTO>();
            CreateMap<Task, TaskDTO>();
            CreateMap<TaskComment, TaskCommentDTO>();
            CreateMap<TaskSprint, TaskSprintDTO>();
            CreateMap<UserTask, UserTaskDTO>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<SprintDTO, Sprint>();
            CreateMap<TaskDTO, Task>();
            CreateMap<TaskCommentDTO, TaskComment>();
            CreateMap<TaskSprintDTO, TaskSprint>();
            CreateMap<UserTaskDTO, UserTask>();
        }
    }
}
