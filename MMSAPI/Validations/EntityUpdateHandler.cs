using DataLibrary.Models;
using MMSAPI.Repository;
using MMSAPI.Validations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMSAPI.Validations
{
    public class EntityUpdateHandler : IEntityUpdateHandler
    {
        public IServiceProvider _serviceProvider { get; }
        public ITaskRepository TaskRepository { get; }

        public EntityUpdateHandler(IServiceProvider serviceProvider, ITaskRepository taskRepository)
        {
            _serviceProvider = serviceProvider;
            TaskRepository = taskRepository;
        }

        public bool Update(IEntity entity)
        {
            var @switch = new Dictionary<Type, Func<bool>> {
                { typeof(AppTask), () => new TaskValidator((entity as AppTask), TaskRepository, _serviceProvider).update()},
            };

            return @switch[entity.GetType()]();
        }
    }
}
