using Microsoft.AspNetCore.Mvc;
using MMSAPI.Repository;
using System;
using DataLibrary.Models;
using AutoMapper;
using DataLibrary.DTO;

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
            var atra = _autoMapper.Map<Comment>(Comment);
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
            var atra = _autoMapper.Map<Comment>(Comment);
            try
            {
                return Ok(_commentRepository.Edit(atra));
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
                return Ok(_commentRepository.Delete(Id));
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
                return Ok(_commentRepository.GetById(Id));
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
                return Ok(_commentRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
