namespace DataLibrary.Models
{
    public class SprintTask: IEntity
    {
        public string Sprint_Id { get; set; }
        public virtual Sprint Sprint { get; set; }

        public string Task_Id { get; set; }
        public virtual AppTask Task { get; set; }
    }
}
