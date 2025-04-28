using API.DTOs;
using Azure;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitofwork _unitOfWork;
        private readonly APIResponse _response;

        public ProductController(IUnitofwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new APIResponse();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _unitOfWork.Products.GetAll();

            if (products == null || !products.Any())
            {

                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "No products found" };

                return NotFound(_response);
            }


            _response.StatusCode = HttpStatusCode.OK;
            _response.Data = products.ToList();

            return Ok(_response);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _unitOfWork.Products.GetById(id);
            if (product == null)
            {

                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Product not found" };



                return NotFound(_response);
            }


            _response.StatusCode = HttpStatusCode.OK;
            _response.Data = product;


            return Ok(_response);
        }
        [HttpPost]
        public IActionResult Create([FromForm] ProductInsertDto ProductDto)
        {
            if (ProductDto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;

                return BadRequest(_response);
            }
            if (!ModelState.IsValid)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return BadRequest(_response);
            }
            if (ProductDto.ImageUrl != null && ProductDto.ImageUrl.Length > 0)
            {
                //var folderPath = Path.Combine(IWebHostEnvironment.WebRootPath, "images");
               var folderPath = "images";
                var fileName = Path.GetFileName(ProductDto.ImageUrl.FileName);
                var imagePath = Path.Combine(folderPath, fileName);

                //var imagePath = Path.Combine("wwwroot/images", productInsertDto.ImageUrl.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    /* await*/
                    ProductDto.ImageUrl.CopyToAsync(stream);
                }
            }
            var entity = new Product
            {
                Name = ProductDto.Name,
                Description = ProductDto.Description,
                Price = ProductDto.Price,
                Discount = ProductDto.Discount,
                StockAmount = ProductDto.StockAmount,
                Brand = ProductDto.Brand,
                CategoryId = ProductDto.CategoryId,
                ImageUrl = "/images/" + ProductDto.ImageUrl.FileName,
                ImageLocalPath = ProductDto.ImageUrl.FileName,
                ProductCode = ProductDto.ProductCode
               
            };
            _unitOfWork.Products.Add(entity);

            _response.StatusCode = HttpStatusCode.Created;
            _response.Data = entity;


            //return CreatedAtAction(nameof(GetById), new { id /*= entity.Id*/ }, _response);
            return Ok(_response);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm]  ProductUpdateDto productDto)
        {
            if (productDto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            if (id != productDto.Id)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors = new List<string> { "ID mismatch" };

                return BadRequest(_response);
            }

            var existing = _unitOfWork.Products.GetById(id);
            if (existing == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Product not found" };

                return NotFound(_response);
            }

            if (productDto.ImageUrl != null && productDto.ImageUrl.Length > 0)
            {
                //var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var folderPath = "wwwroot/images";
                var fileName = Path.GetFileName(productDto.ImageUrl.FileName);
                var imagePath = Path.Combine(folderPath, fileName);

                //var imagePath = Path.Combine("wwwroot/images", productInsertDto.ImageUrl.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    /* await*/
                    productDto.ImageUrl.CopyToAsync(stream);
                }
            }
            existing.Name = productDto.Name;
            existing.Description = productDto.Description;
            existing.Price = productDto.Price;
            existing.Discount = productDto.Discount;
            existing.StockAmount = productDto.StockAmount;
            existing.Brand = productDto.Brand;
            existing.CategoryId = productDto.CategoryId;
            existing.ImageUrl = "/images/" + productDto.ImageUrl.FileName;
            existing.ImageLocalPath = productDto.ImageUrl.FileName;
            existing.ProductCode = productDto.ProductCode;

            _unitOfWork.Products.Update(existing);

            _response.StatusCode = HttpStatusCode.OK;
            _response.Data = existing;


            return Ok(_response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = _unitOfWork.Products.GetById(id);
            if (existing == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.Errors = new List<string> { "Product not found" };


                return NotFound(_response);
            }

            _unitOfWork.Products.Delete(existing.Id);
            await _unitOfWork.SaveAsync();

            _response.StatusCode = HttpStatusCode.OK;



            return Ok(_response);
        }

        [HttpGet("search/{name}")]
        public IActionResult SearchByName(string name)
        {
            var products = _unitOfWork.Products.SearchByName(name);
            if (!products.Any())
                return NotFound();

            return Ok(products);
        }
    }
    }