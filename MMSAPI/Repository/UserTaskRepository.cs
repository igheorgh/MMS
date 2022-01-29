using DataLibrary;
using DataLibrary.Models;
namespace MMSAPI.Repository
{
    public class UserTaskRepository : BaseRepository<UserTask>, IUserTaskRepository
    {
        public UserTaskRepository(MMSContext context) : base(context)
        {
        }
    }
}
