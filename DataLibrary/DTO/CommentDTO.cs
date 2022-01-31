using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using DataLibrary.Models;

namespace DataLibrary.DTO
{
    [DataContract]
    public class CommentDTO
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; }

        [DataMember]
        [JsonProperty("date_Posted")]
        public DateTime Date_Posted { get; set; }

        [DataMember]
        [JsonProperty("user_Id")]
        public string User_Id { get; set; }

        [DataMember]
        [JsonProperty("user")]
        public UserDTO User { get; set; }

        [DataMember]
        [JsonProperty("task_Id")]
        public string Task_Id { get; set; }

        [DataMember]
        [JsonProperty("task")]
        public TaskDTO Task { get; set; }

        public CommentDTO()
        {
            Task = new TaskDTO();
            User = new UserDTO();
        }

        public static CommentDTO FromModel(Comment model)
        {
            if (model == null) return null;
            return new CommentDTO()
            {
                Id = model.Id, 
                Description = model.Description, 
                Date_Posted = model.Date_Posted, 
                User_Id = model.User_Id, 
                User = UserDTO.FromModel(model.User), 
                Task_Id = model.Task_Id, 
                Task = TaskDTO.FromModel(model.Task), 
            }; 
        }

        public Comment ToModel()
        {
            return new Comment()
            {
                Id = Id, 
                Description = Description, 
                Date_Posted = Date_Posted, 
                User_Id = User_Id, 
                User = User.ToModel(), 
                Task_Id = Task_Id, 
                Task = Task.ToModel(), 
            }; 
        }
    }
}