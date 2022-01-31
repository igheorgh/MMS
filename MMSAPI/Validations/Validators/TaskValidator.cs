using DataLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using MMSAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMSAPI.Validations.Validators
{
    public class TaskValidator : AbstractEntityHandler<AppTask>
    {
        public TaskValidator(AppTask newEntity, ITaskRepository taskRepository, IServiceProvider serviceProvider) : 
            base(newEntity, taskRepository)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }

        protected override void applyChanges()
        {
            var userRepo = (IUserRepository)ServiceProvider.GetService(typeof(IUserRepository));
            var sprintRepo = (ISprintRepository)ServiceProvider.GetService(typeof(ISprintRepository));

            dbEntity.Description = newEntity.Description;
            dbEntity.Name = newEntity.Name;
            dbEntity.Status = newEntity.Status;
            dbEntity.User_Id = newEntity.User_Id;
            dbEntity.User = userRepo.GetById(newEntity.User_Id);

            dbEntity.SprintTasks = new HashSet<SprintTask>();
            for (int i = 0; i < newEntity.SprintTasks.Count; i++)
            {
                dbEntity.SprintTasks.Add(new SprintTask
                {
                    Task = dbEntity,
                    Sprint = sprintRepo.GetById(newEntity.SprintTasks.ElementAt(i).Sprint_Id),
                    Task_Id = dbEntity.Id,
                    Sprint_Id = newEntity.SprintTasks.ElementAt(i).Sprint_Id
                });
            }
        }

    }
}
