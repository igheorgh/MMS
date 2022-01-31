using Microsoft.AspNetCore.Mvc;
using MMSAPI.Repository;
using System;
using DataLibrary.Models;
using AutoMapper;
using DataLibrary.DTO;
using System.Linq;

namespace MMSAPI.Controllers
{
    [Route("comment")]
    [ApiController]
    public class CommentController : Controller
    {
        private ICommentRepository _commentRepository;
        private IMapper _autoMapper;
        public CommentController(ICommentRepository commentRepository, IMapper autoMapper)
        {
            _commentRepository = commentRepository;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CommentDTO Comment)
        {
            var atra = Comment.ToModel();
            atra.Id = Guid.NewGuid().ToString();
            try
            {
                return Ok(_commentRepository.Add(atra));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] CommentDTO Comment)
        {
            var atra = Comment.ToModel();
            try
            {
                return Ok(_commentRepository.Edit(atra));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromQuery]string id)
        {
            try
            {
                return Ok(_commentRepository.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromQuery]string id)
        {
            try
            {
                return Ok(CommentDTO.FromModel(_commentRepository.GetById(id)));
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
                return Ok(_commentRepository.GetAll().Select(s => CommentDTO.FromModel(s)));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
