using System.Collections.Generic;

namespace DataLibrary.Models
{
    public class AppTask: IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public virtual ICollection<SprintTask> SprintTasks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public string User_Id { get; set; }
        public virtual User User { get; set; }

        public AppTask()
        {
            SprintTasks = new HashSet<SprintTask>();
            Comments = new HashSet<Comment>();
        }
    }
}
