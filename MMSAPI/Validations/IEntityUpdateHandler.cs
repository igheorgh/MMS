using DataLibrary.Models;
using System;

namespace MMSAPI.Validations
{
    public interface IEntityUpdateHandler
    {
        IServiceProvider _serviceProvider { get; }

        bool Update(IEntity entity);
    }
}