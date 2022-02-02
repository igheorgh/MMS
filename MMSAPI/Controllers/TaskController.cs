using Microsoft.AspNetCore.Mvc;
using MMSAPI.Repository;
using System;
using DataLibrary.Models;
using AutoMapper;
using DataLibrary.DTO;
using System.Linq;
using MMSAPI.Validations;

namespace MMSAPI.Controllers
{
    [Route("api/v1/task")]
    [ApiController]
    public class TaskController : Controller
    {
        private ITaskRepository _taskRepository;
        private ISprintRepository _sprintRepository;
        private IUserRepository _userRepository;

        public IEntityUpdateHandler _entityUpdateHandler { get; }

        public TaskController(ITaskRepository taskRepository, ISprintRepository sprintRepository, IUserRepository userRepository,
            IEntityUpdateHandler entityUpdateHandler)
        {
            _taskRepository = taskRepository;
            _sprintRepository = sprintRepository;
            _userRepository = userRepository;
            _entityUpdateHandler = entityUpdateHandler;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskDTO task)
        {
            try
            {
                return Ok(TaskDTO.FromModel(_taskRepository.Add(GenerateTask(task))));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        private AppTask GenerateTask(TaskDTO task)
        {
            var atra = task.ToModel();
            atra.Id = Guid.NewGuid().ToString();
            atra.User = _userRepository.GetById(atra.User_Id);
            for (int i = 0; i < task.SprintTasks.Count; i++)
            {
                atra.SprintTasks.ElementAt(i).Task = atra;
                atra.SprintTasks.ElementAt(i).Sprint = _sprintRepository.GetById(atra.SprintTasks.ElementAt(i).Sprint_Id);
                atra.SprintTasks.ElementAt(i).Task_Id = atra.Id;
            }
            return atra;
        }

        [HttpPut]
        public IActionResult Update([FromBody] TaskDTO task)
        {
            try
            {
                return _entityUpdateHandler.Update<AppTask>(task.ToModel()).ToHttpResponse();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery] string id)
        {
            try
            {
                return Ok(_taskRepository.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromQuery] string Id)
        {
            try
            {
                return Ok(TaskDTO.FromModel(_taskRepository.GetById(Id)));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_taskRepository.GetAll().Select(s => TaskDTO.FromModel(s)));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
