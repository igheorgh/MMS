using DataLibrary;
using DataLibrary.Models;
namespace MMSAPI.Repository
{
    public class TaskSprintRepository : BaseRepository<TaskSprint>, ITaskSprintRepository
    {
        public TaskSprintRepository(MMSContext context) : base(context)
        {
        }
    }
}
