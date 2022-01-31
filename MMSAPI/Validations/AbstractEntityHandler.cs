using DataLibrary.Models;
using MMSAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MMSAPI.Validations
{
    public abstract class AbstractEntityHandler<T> where T: IEntity
    {
        private object repository { get; set; }
        protected T dbEntity { get; set; }
        protected T newEntity { get; set; }
        public AbstractEntityHandler(T newEntity, IRepository repository)
        {
            this.repository = repository;

            this.newEntity = newEntity;
            this.dbEntity = get();
        }



        protected X GetRepository<X>()
        {
            return (X)repository;
        }


        private Type GetRepoType()
        {
            var @switch = new Dictionary<Type, Type> {
                { typeof(AppTask), typeof(TaskRepository)},
            };

            return @switch[typeof(T)];
        }

        protected virtual T get()
        {
            MethodInfo methodDefinition = GetRepoType().GetMethod("GetById");
            var result = methodDefinition.Invoke(repository, new object[] { (newEntity.GetType().GetProperty("Id").GetValue(newEntity)) });
            return (T)result;
        }

        protected abstract void applyChanges();

        //ValidationResult validate();

        public virtual bool update()
        {
            applyChanges();

            MethodInfo methodDefinition = GetRepoType().GetMethod("Edit");

            return (bool)methodDefinition.Invoke(repository, new object[] { dbEntity });
        }
    }
}
