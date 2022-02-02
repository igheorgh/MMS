using DataLibrary;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MMSAPI.Repository
{
    public class TaskRepository : BaseRepository<AppTask>, ITaskRepository
    {
        public TaskRepository(MMSContext context) : base(context)
        {
        }

    }
}
