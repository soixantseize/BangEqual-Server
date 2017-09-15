using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BangEqualServer.Models;
using BangEqualServer.Repositories.Interfaces;

namespace BangEqualServer.Controllers
{
    //[Authorize(Policy = "Bearer")]
    public class ArticleInfoController : ControllerBase
    {
        private readonly IArticleInfoRepository _repository;

        public ArticleInfoController(IArticleInfoRepository repository)
        {
            _repository = repository;
        }

        // GET articles/5
        [HttpGet("/articles/{chunksize}")]
        public IActionResult GetArticleInfo(int chunksize)
        {
            return Ok( _repository.GetArticleInfo(chunksize).Result);        
        }
        
        // GET articles/5/webdev
        [HttpGet("/articles/{chunksize}/{tag}")]
        public IActionResult GetArticleInfoByTag(string tag, int chunksize)
        {
            return Ok( _repository.GetArticleInfoByTag(tag, chunksize).Result);          
        }

		//GET articles/tags
        [HttpGet("/articles/tags")]
        public IActionResult GetArticleInfoTags()
        {
            return Ok( _repository.GetArticleInfoTags().Result);              
        }

        //GET home/topic/getall/article
        //[HttpGet("/home/topic/getall/{type}")]
        //public IActionResult GetTopicsByType(string type)
        //{
            //return Ok( _repository.GetTopic(type).Result);              
        //}

        //GET home/topic/article/webdev/4
        //[HttpGet("/home/topic/{type}/{topic}/{chunksize}")]
        //public IActionResult GetContentByTopicAndType(string topic, int chunksize, string type)
        //{
            //return Ok( _repository.GetByTopicAndType(topic, chunksize, type).Result);              
        //}



        // POST index/content
        [HttpPost]
        public IActionResult Post([FromBody]ArticleInfo c)
        {
            try
            {
                if (c == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TitleAndContentRequired.ToString());
                }
                bool itemExists = _repository.DoesItemExist(c.ArticleInfoId).Result;
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
        public IActionResult Put(int id, [FromBody]ArticleInfo c)
        {
            try
            {
                if (c == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TitleAndContentRequired.ToString());
                }
                var existingContent = "";
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
            // try
            //{
                //var content = _repository.GetArticleInfoById(id).Result;
                //if (content == null)
                //{
                    //return NotFound(ErrorCode.RecordNotFound.ToString());
               // }
               // _repository.DeleteAsync(content);
            //}
            //catch (Exception e)
            //{
                //return BadRequest(ErrorCode.CouldNotDeleteItem.ToString());
            //}
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
