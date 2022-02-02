using System;
using System.Collections.Generic;

namespace DataLibrary.Models
{
    public  class Sprint: IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Start_Date  { get; set; }
        public DateTime End_Date { get; set; } 
        public string Goal { get;set; }
        public virtual ICollection<SprintTask> SprintTasks { get; set; }
        public Sprint()
        {
            SprintTasks = new HashSet<SprintTask>();
        }
    }
}
