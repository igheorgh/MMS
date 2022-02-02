using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using DataLibrary.Models;

namespace DataLibrary.DTO
{
    [DataContract]
    public class SprintTaskDTO
    {
        [DataMember]
        [JsonProperty("sprint_Id")]
        public string Sprint_Id { get; set; }

        [DataMember]
        [JsonProperty("sprint")]
        public SprintDTO Sprint { get; set; }

        [DataMember]
        [JsonProperty("task_Id")]
        public string Task_Id { get; set; }

        [DataMember]
        [JsonProperty("task")]
        public TaskDTO Task { get; set; }

        public SprintTaskDTO()
        {
            Sprint = new SprintDTO();
            Task = new TaskDTO();
        }

        public static SprintTaskDTO FromModel(SprintTask model)
        {
            if (model == null) return null;
            return new SprintTaskDTO()
            {
                Sprint_Id = model.Sprint_Id, 
                Sprint = SprintDTO.FromModel(model.Sprint), 
                Task_Id = model.Task_Id, 
                Task = TaskDTO.FromModel(model.Task), 
            }; 
        }

        public SprintTask ToModel()
        {
            return new SprintTask()
            {
                Sprint_Id = Sprint_Id, 
                Sprint = Sprint.ToModel(), 
                Task_Id = Task_Id, 
                Task = Task.ToModel(), 
            }; 
        }
    }
}