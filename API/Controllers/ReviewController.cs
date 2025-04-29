using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Models;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUnitofwork _unitofWork;
        private readonly APIResponse _response;

        public ReviewController(IUnitofwork unitOfWork)
        {
            _unitofWork = unitOfWork;
            _response = new APIResponse();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.Errors = new List<string> { "User not authorized" };
                return Unauthorized(_response);
            }

            var Reviews = await _unitofWork.Reviews.GetReviewWithProductAsync(id);

            _response.Data = Reviews;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateReview([FromBody] ReviewInsertDto reviewDto)
        {
            try
            {
                if (reviewDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userId == null)
                    {
                        _response.StatusCode = HttpStatusCode.Unauthorized;
                        _response.Errors = new List<string> { "User not authorized" };
                        return Unauthorized(_response);
                    }
                    var entity = new Review
                    {
                        Content = reviewDto.Content,
                        Rating = reviewDto.Rating,
                        CreatedAt = reviewDto.CreatedAt,
                        ProductId = reviewDto.ProductId,
                        UserId = userId
                    };

                    await _unitofWork.Reviews.AddAsync(entity);
                    await _unitofWork.SaveAsync();
                    _response.Data = entity;
                    _response.StatusCode = HttpStatusCode.Created;
                    return Ok(_response);
                }
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.Errors = new List<string> { ex.Message };
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> UpdateReview(int id, ReviewUpdateDto reviewdto)
        {
            if (reviewdto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userId == null)
                    {
                        _response.StatusCode = HttpStatusCode.Unauthorized;
                        _response.Errors = new List<string> { "User not authorized" };
                        return Unauthorized(_response);
                    }
                    var review = await _unitofWork.Reviews.GetAsync(r => r.Id == id);
                    if (review.UserId != userId)
                    {
                        _response.StatusCode = HttpStatusCode.Unauthorized;
                        _response.Errors = new List<string> { "Unable to updated this review" };
                        return Unauthorized(_response);
                    }
                    review.Content = reviewdto.Content;
                    review.Rating = reviewdto.Rating;
                    review.UpdatedAt = reviewdto.UpdatedAt;
                    review.ProductId = reviewdto.ProductId;
                    

                    await _unitofWork.SaveAsync();
                    _response.Data = reviewdto;
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                catch (Exception ex)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
            }

            _response.Errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            _response.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(_response);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> DeleteReview(int id)
        {
            try
            {
                var review = await _unitofWork.Reviews.GetAsync(r => r.Id == id);
                if (review == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.Errors = new List<string> { "review Not Found" };
                    return NotFound(_response);
                }

                await _unitofWork.Reviews.RemoveAsync(review);
                await _unitofWork.SaveAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { ex.Message };
                return NotFound(_response);
            }
        }
    }
}
