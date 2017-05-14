using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BareMetalApi.Models;
using BareMetalApi.Repositories.Interfaces;

namespace BareMetalApi.Controllers
{
    [Route("index/[controller]")]
    //[Authorize(Policy = "Bearer")]
    public class ContentController : ControllerBase
    {
        private readonly IContentRepository _repository;

        public ContentController(IContentRepository repository)
        {
            _repository = repository;
        }
        
        // GET index/content
        [HttpGet]
        public IActionResult Get()
        {
            return Ok( _repository.GetAll().Result);     
        }

        // GET index/content/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _repository.GetById(id).Result;
            return Ok( data);
        }

        // POST index/content
        [HttpPost]
        public IActionResult Post([FromBody]Content c)
        {
            try
            {
                if (c == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TitleAndContentRequired.ToString());
                }
                bool itemExists = _repository.DoesItemExist(c.ContentId).Result;
                if (itemExists)
                {
                    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.IDInUse.ToString());
                }
                _repository.AddAsync(c);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
            }
            return Ok(c);
        }

        // PUT index/content/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Content c)
        {
            try
            {
                if (c == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TitleAndContentRequired.ToString());
                }
                var existingContent = _repository.GetById(id);
                if (existingContent == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _repository.UpdateAsync(c);
                
            }
            catch (Exception e)
            {
                return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
            }
            return NoContent();
        }

        // DELETE index/content/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
             try
            {
                var content = _repository.GetById(id).Result;
                if (content == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _repository.DeleteAsync(content);
            }
            catch (Exception e)
            {
                return BadRequest(ErrorCode.CouldNotDeleteItem.ToString());
            }
            return NoContent();
        }

    public enum ErrorCode
    {
        TitleAndContentRequired,
        IDInUse,
        RecordNotFound,
        CouldNotCreateItem,
        CouldNotUpdateItem,
        CouldNotDeleteItem
    }
    }
}
