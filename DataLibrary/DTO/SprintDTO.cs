using System;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using DataLibrary.Models;
using System.Collections.Generic;

namespace DataLibrary.DTO
{
    [DataContract]
    public class SprintDTO
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty("start_Date")]
        public DateTime Start_Date { get; set; }

        [DataMember]
        [JsonProperty("end_Date")]
        public DateTime End_Date { get; set; }

        [DataMember]
        [JsonProperty("goal")]
        public string Goal { get; set; }

        [DataMember]
        [JsonProperty("sprintTasks")]
        public ICollection<SprintTaskDTO> SprintTasks { get; set; }
        public SprintDTO()
        {
            SprintTasks = new HashSet<SprintTaskDTO>();
        }

        public static SprintDTO FromModel(Sprint model)
        {
            if (model == null) return null;
            return new SprintDTO()
            {
                Id = model.Id, 
                Name = model.Name, 
                Start_Date = model.Start_Date, 
                End_Date = model.End_Date, 
                Goal = model.Goal, 
                SprintTasks = model.SprintTasks.Select(st => SprintTaskDTO.FromModel(st)).ToList(), 
            }; 
        }

        public Sprint ToModel()
        {
            return new Sprint()
            {
                Id = Id, 
                Name = Name, 
                Start_Date = Start_Date, 
                End_Date = End_Date, 
                Goal = Goal, 
                SprintTasks = SprintTasks.Select(st => st.ToModel()).ToList(), 
            }; 
        }
    }
}