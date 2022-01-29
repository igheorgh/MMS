using Microsoft.AspNetCore.Mvc;
using MMSAPI.Repository;
using System;
using DataLibrary.Models;
using AutoMapper;
using DataLibrary.DTO;

namespace MMSAPI.Controllers
{
    [Route("taskSprint")]
    [ApiController]
    public class TasksprintController : Controller
    {
        private ITaskSprintRepository _tasksprintRepository;
        private IMapper _autoMapper;
        public TasksprintController(ITaskSprintRepository tasksprintRepository, IMapper autoMapper)
        {
            _tasksprintRepository = tasksprintRepository;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskSprintDTO tasksprint)
        {
            var atra = _autoMapper.Map<TaskSprint>(tasksprint);
            try
            {
                return Ok(_tasksprintRepository.Add(atra));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] TaskSprintDTO tasksprint)
        {
            var atra = _autoMapper.Map<TaskSprint>(tasksprint);
            try
            {
                return Ok(_tasksprintRepository.Edit(atra));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("Id")]
        public IActionResult Delete(int Id)
        {
            try
            {
                return Ok(_tasksprintRepository.Delete(Id));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("Id")]
        public IActionResult GetById(int Id)
        {
            try
            {
                return Ok(_tasksprintRepository.GetById(Id));
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
                return Ok(_tasksprintRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
