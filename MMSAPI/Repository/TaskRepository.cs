using DataLibrary;
using DataLibrary.Models;
namespace MMSAPI.Repository
{
    public class TaskRepository : BaseRepository<Task>, ITaskRepository
    {
        public TaskRepository(MMSContext context) : base(context)
        {
        }
    }
}
