using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BareMetalApi.Models;
using BareMetalApi.Repositories.Interfaces;

namespace BareMetalApi.Controllers
{
    [Route("shop/[controller]")]
    //[Authorize(Policy = "Bearer")]
    public class ShopDesignController : ControllerBase
    {
        private readonly IShopDesignRepository _repository;

        public ShopDesignController(IShopDesignRepository repository)
        {
            _repository = repository;
        }
        
        // GET shop/shopdesign
        [HttpGet]
        public IActionResult Get()
        {
            return Ok( _repository.GetAll().Result);     
        }

        // GET shop/shopdesign/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = _repository.GetById(id).Result;
            return Ok( data);
        }

        // POST shop/shopdesign
        [HttpPost]
        public IActionResult Post([FromBody]ShopDesign shopdesign)
        {
            try
            {
                if (shopdesign == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TitleAndContentRequired.ToString());
                }
                bool itemExists = _repository.DoesItemExist(shopdesign.DesignId).Result;
                if (itemExists)
                {
                    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.IDInUse.ToString());
                }
                _repository.AddAsync(shopdesign);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
            }
            return Ok(shopdesign);
        }

        // PUT shop/shopdesign/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ShopDesign shopdesign)
        {
            try
            {
                if (shopdesign == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TitleAndContentRequired.ToString());
                }
                var existingBlogArticle = _repository.GetById(id);
                if (existingBlogArticle == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _repository.UpdateAsync(shopdesign);
                
            }
            catch (Exception e)
            {
                return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
            }
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
             try
            {
                var shopdesign = _repository.GetById(id).Result;
                if (shopdesign == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _repository.DeleteAsync(shopdesign);
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
