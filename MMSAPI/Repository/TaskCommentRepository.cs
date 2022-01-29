using DataLibrary;
using DataLibrary.Models;
namespace MMSAPI.Repository
{
    public class TaskCommentRepository : BaseRepository<TaskComment>, ITaskCommentRepository
    {
        public TaskCommentRepository(MMSContext context) : base(context)
        {
        }
    }
}
