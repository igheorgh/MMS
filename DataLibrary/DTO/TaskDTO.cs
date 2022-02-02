using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using DataLibrary.Models;
using System.Collections.Generic;

namespace DataLibrary.DTO
{
    [DataContract]
    public class TaskDTO
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }

        [DataMember]
        [JsonProperty("status")]
        public string Status { get; set; }

        [DataMember]
        [JsonProperty("sprintTasks")]
        public ICollection<SprintTaskDTO> SprintTasks { get; set; }

        [DataMember]
        [JsonProperty("comments")]
        public ICollection<CommentDTO> Comments { get; set; }

        [DataMember]
        [JsonProperty("user_Id")]
        public string User_Id { get; set; }

        [DataMember]
        [JsonProperty("user")]
        public UserDTO User { get; set; }
        public TaskDTO()
        {
            User = new UserDTO();
            Comments = new HashSet<CommentDTO>();
            SprintTasks = new HashSet<SprintTaskDTO>();
        }

        public static TaskDTO FromModel(AppTask model)
        {
            if (model == null) return null;
            return new TaskDTO()
            {
                Id = model.Id, 
                Name = model.Name, 
                Description = model.Description, 
                Status = model.Status, 
                SprintTasks = model.SprintTasks.Select(st => SprintTaskDTO.FromModel(st)).ToList(), 
                Comments = model.Comments.Select(c => CommentDTO.FromModel(c)).ToList(), 
                User_Id = model.User_Id, 
                User = UserDTO.FromModel(model.User), 
            }; 
        }

        public AppTask ToModel()
        {
            return new AppTask()
            {
                Id = Id, 
                Name = Name, 
                Description = Description, 
                Status = Status, 
                SprintTasks = SprintTasks.Select(st => st.ToModel()).ToList(), 
                Comments = Comments.Select(c => c.ToModel()).ToList(), 
                User_Id = User_Id, 
                User = User.ToModel(), 
            }; 
        }
    }
}