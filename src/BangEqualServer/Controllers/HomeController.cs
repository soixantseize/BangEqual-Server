using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BareMetalApi.Models;
using BareMetalApi.Repositories.Interfaces;

namespace BareMetalApi.Controllers
{
    //[Authorize(Policy = "Bearer")]
    public class HomeController : ControllerBase
    {
        private readonly IContentRepository _repository;

        public HomeController(IContentRepository repository)
        {
            _repository = repository;
        }
        
        // GET home/article/4
        [HttpGet("/home/{type}/{chunksize}")]
        public IActionResult GetContentByType(string type, int chunksize)
        {
            return Ok( _repository.GetContent(type, chunksize).Result);          
        }

        // GET home/5
        [HttpGet("/home/{id}")]
        public IActionResult GetContentById(int id)
        {
            var data = _repository.GetById(id).Result;
			if(data != null && !String.IsNullOrEmpty(data.RenderString))
				data.RenderString = CommonMark.CommonMarkConverter.Convert(data.RenderString);
			else
				Console.Write("error in homecontroller get(int)");
            return Ok( data);
        }

        //GET home/topic/getall/article
        [HttpGet("/home/topic/getall/{type}")]
        public IActionResult GetTopicsByType(string type)
        {
            return Ok( _repository.GetTopic(type).Result);              
        }

        //GET home/topic/article/webdev/4
        [HttpGet("/home/topic/{type}/{topic}/{chunksize}")]
        public IActionResult GetContentByTopicAndType(string topic, int chunksize, string type)
        {
            return Ok( _repository.GetByTopicAndType(topic, chunksize, type).Result);              
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
