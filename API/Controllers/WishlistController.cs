﻿using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IUnitofwork _unitOfWork;
        private readonly APIResponse _response;

        public WishlistController(IUnitofwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetWishlist()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Errors = new List<string> { "User ID not found" };
                    return BadRequest(_response);
                }

                var wishlist = await _unitOfWork.Wishlists.GetWishlistByUserId(userId);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Data = wishlist;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.OK;
                _response.Errors = new List<string> { ex.Message };
                return Ok(_response);
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<APIResponse>> AddToWishlist(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Errors = new List<string> { "User ID not found" };
                    return BadRequest(_response);
                }

                bool Added = await _unitOfWork.Wishlists.AddProductToWishlist(id, userId);

                _response.Data = Added;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add(ex.Message);
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> RemoveWishlistProduct(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Errors = new List<string> { "User ID not found" };
                    return BadRequest(_response);
                }

                bool Removed = await _unitOfWork.Wishlists.RemoveFromWishlist(id, userId);

                _response.Data = Removed;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add(ex.Message);
                return BadRequest(_response);
            }

        }
    }
}
