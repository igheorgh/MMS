using Microsoft.AspNetCore.Mvc;
using MMSAPI.Repository;
using System;
using DataLibrary.Models;
using AutoMapper;
using DataLibrary.DTO;
using System.Linq;

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
            var atra = sprint.ToModel();
            atra.Id = Guid.NewGuid().ToString();
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
            var atra = sprint.ToModel();
            try
            {
                return Ok(_sprintRepository.Edit(atra));
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
                return Ok(_sprintRepository.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromQuery] string id)
        {
            try
            {
                return Ok(SprintDTO.FromModel(_sprintRepository.GetById(id)));
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
                return Ok(_sprintRepository.GetAll().Select(s => SprintDTO.FromModel(s)));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
