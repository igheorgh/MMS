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
        [JsonProperty("comments")]
        public ICollection<CommentDTO> Comments { get; set; }

        [DataMember]
        [JsonProperty("username")]
        public string Username { get; set; }

        [DataMember]
        [JsonProperty("user_id")]
        public string User_Id { get; set; }

        [DataMember]
        [JsonProperty("sprint_id")]
        public string Sprint_id { get; set; }

        public TaskDTO()
        {
            Comments = new HashSet<CommentDTO>();
        }

        public static TaskDTO FromModel(AppTask model)
        {
            if (model == null) return null;
            var firstSprint = model.SprintTasks.FirstOrDefault();
            return new TaskDTO()
            {
                Id = model.Id, 
                Name = model.Name, 
                Description = model.Description, 
                Status = model.Status, 
                Sprint_id = firstSprint != null ? firstSprint.Sprint_Id : string.Empty, 
                Comments = model.Comments.Select(c => CommentDTO.FromModel(c)).ToList(), 
                User_Id = model.User_Id, 
                Username = model.User.UserName
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
                SprintTasks = new HashSet<SprintTask>() { new SprintTask {
                    Task = new AppTask { Id = this.Id}, Task_Id = this.Id,
                    Sprint = new Sprint { Id = this.Sprint_id}, Sprint_Id = this.Sprint_id
                } }, 
                Comments = Comments.Select(c => c.ToModel()).ToList(), 
                User_Id = User_Id,
            }; 
        }
    }
}