using Microsoft.AspNetCore.Mvc;
using MMSAPI.Repository;
using System;
using DataLibrary.Models;
using AutoMapper;
using DataLibrary.DTO;

namespace MMSAPI.Controllers
{
    [Route("sprint")]
    [ApiController]
    public class sprintController : Controller
    {
        private ISprintRepository _sprintRepository;
        private IMapper _autoMapper;
        public sprintController(ISprintRepository sprintRepository, IMapper autoMapper)
        {
            _sprintRepository = sprintRepository;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SprintDTO sprint)
        {
            var atra = _autoMapper.Map<Sprint>(sprint);
            try
            {
                return Ok(_sprintRepository.Add(atra));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] SprintDTO sprint)
        {
            var atra = _autoMapper.Map<Sprint>(sprint);
            try
            {
                return Ok(_sprintRepository.Edit(atra));
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
                return Ok(_sprintRepository.Delete(Id));
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
                return Ok(_sprintRepository.GetById(Id));
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
                return Ok(_sprintRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
