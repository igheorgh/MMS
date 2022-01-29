using Microsoft.AspNetCore.Mvc;
using MMSAPI.Repository;
using System;
using DataLibrary.Models;
using AutoMapper;
using DataLibrary.DTO;

namespace MMSAPI.Controllers
{
    [Route("taskComment")]
    [ApiController]
    public class TaskCommentController : Controller
    {
        private ITaskCommentRepository _taskCommentRepository;
        private IMapper _autoMapper;
        public TaskCommentController(ITaskCommentRepository taskCommentRepository, IMapper autoMapper)
        {
            _taskCommentRepository = taskCommentRepository;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskCommentDTO taskComment)
        {
            var atra = _autoMapper.Map<TaskComment>(taskComment);
            try
            {
                return Ok(_taskCommentRepository.Add(atra));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] TaskCommentDTO taskComment)
        {
            var atra = _autoMapper.Map<TaskComment>(taskComment);
            try
            {
                return Ok(_taskCommentRepository.Edit(atra));
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
                return Ok(_taskCommentRepository.Delete(Id));
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
                return Ok(_taskCommentRepository.GetById(Id));
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
                return Ok(_taskCommentRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
