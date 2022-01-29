using Microsoft.AspNetCore.Mvc;
using MMSAPI.Repository;
using System;
using DataLibrary.Models;
using AutoMapper;
using DataLibrary.DTO;

namespace MMSAPI.Controllers
{
    [Route("userTask")]
    [ApiController]
    public class UserTaskController : Controller
    {
        private IUserTaskRepository _userTaskRepository;
        private IMapper _autoMapper;
        public UserTaskController(IUserTaskRepository userTaskRepository, IMapper autoMapper)
        {
            _userTaskRepository = userTaskRepository;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserTaskDTO UserTask)
        {
            var atra = _autoMapper.Map<UserTask>(UserTask);
            try
            {
                return Ok(_userTaskRepository.Add(atra));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] UserTaskDTO UserTask)
        {
            var atra = _autoMapper.Map<UserTask>(UserTask);
            try
            {
                return Ok(_userTaskRepository.Edit(atra));
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
                return Ok(_userTaskRepository.Delete(Id));
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
                return Ok(_userTaskRepository.GetById(Id));
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
                return Ok(_userTaskRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
