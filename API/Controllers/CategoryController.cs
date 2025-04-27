using Azure;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly IUnitofwork _unitOfWork;
        private readonly APIResponse _response;

        public CategoryController(IUnitofwork unitOfWork)
		{
			_unitOfWork = unitOfWork;
            _response = new APIResponse();
        }

		[HttpGet]
		public IActionResult GetAll()
		{
			var categories = _unitOfWork.CategoryRepository.GetAll();

			if (categories == null || !categories.Any())
			{

				_response.StatusCode = HttpStatusCode.NotFound;
				_response.Errors = new List<string> { "No categories found" };
	
				return NotFound(_response);
			}


			_response.StatusCode = HttpStatusCode.OK;
			_response.Data = categories.ToList();
			
			return Ok(_response);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var category = _unitOfWork.CategoryRepository.GetById(id);
			if (category == null)
			{

				_response.StatusCode = HttpStatusCode.NotFound;
				_response.Errors = new List<string> { "Category not found" };
					
				
					
				return NotFound(_response);
			}


			_response.StatusCode = HttpStatusCode.OK;
			_response.Data = category;
			
				
			return Ok(_response);
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (!ModelState.IsValid)
			{

				_response.StatusCode = HttpStatusCode.BadRequest;
				_response.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                
				return BadRequest(_response);
			}

			_unitOfWork.CategoryRepository.Add(category);

			_response.StatusCode = HttpStatusCode.Created;
			_response.Data = category;
            

			return CreatedAtAction(nameof(GetById), new { id = category.Id }, _response);
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, Category category)
		{
			if (id != category.Id)
			{
				_response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors = new List<string> { "ID mismatch" };
               
				return BadRequest(_response);
			}

			var existing = _unitOfWork.CategoryRepository.GetById(id);
			if (existing == null)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
               _response.Errors = new List<string> { "Category not found" };
                
				return NotFound(_response);
			}

			existing.Name = category.Name;
			_unitOfWork.CategoryRepository.Update(existing);

			_response.StatusCode = HttpStatusCode.OK;
           _response.Data = existing;
            

			return Ok(_response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var existing = _unitOfWork.CategoryRepository.GetById(id);
			if (existing == null)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Category not found" };
                
				
				return NotFound(_response);
			}

			_unitOfWork.CategoryRepository.Delete(existing.Id);
			await _unitOfWork.SaveAsync();

			_response.StatusCode = HttpStatusCode.OK;
            
			

			return Ok(_response);
		}

		[HttpGet("search/{name}")]
		public IActionResult SearchByName(string name)
		{
			var categories = _unitOfWork.CategoryRepository.SearchByName(name);
			if (!categories.Any())
				return NotFound();

			return Ok(categories);
		}

		[HttpGet("{id}/products")]
		public IActionResult GetCategoryWithProducts(int id)
		{
			var category = _unitOfWork.CategoryRepository.GetCategoryWithProducts(id);
			if (category == null)
				return NotFound();

			return Ok(category);
		}
	}
}
