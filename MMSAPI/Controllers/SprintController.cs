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
    [Route("api/v1/sprint")]
    [ApiController]
    public class SprintController : Controller
    {
        private ISprintRepository _sprintRepository;
        private IMapper _autoMapper;

        public IEntityUpdateHandler EntityUpdateHandler { get; }

        public SprintController(ISprintRepository sprintRepository, IMapper autoMapper, IEntityUpdateHandler entityUpdateHandler)
        {
            _sprintRepository = sprintRepository;
            _autoMapper = autoMapper;
            EntityUpdateHandler = entityUpdateHandler;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SprintDTO sprint)
        {
            var atra = sprint.ToModel();
            atra.Id = Guid.NewGuid().ToString();
            try
            {
                return Ok(SprintDTO.FromModel(_sprintRepository.Add(atra)));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] SprintDTO sprint)
        {
            try
            {
                return EntityUpdateHandler.Update<Sprint>(sprint.ToModel()).ToHttpResponse();
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
