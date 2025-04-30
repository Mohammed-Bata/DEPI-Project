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
	public class OrderController : ControllerBase
	{
		private readonly IUnitofwork _unitOfWork;
		private readonly APIResponse _response;

		public OrderController(IUnitofwork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_response = new APIResponse();
            _response.StatusCode = HttpStatusCode.OK;
            _response.Data = new List<Order>();
		}

		[HttpGet]
		public async Task<ActionResult<APIResponse>> GetUserOrders()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userId == null)
			{
				_response.StatusCode = HttpStatusCode.Unauthorized;
				_response.Errors = new List<string> { "User not authorized" };
				return Unauthorized(_response);
			}

			//var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId);
			var orders = (await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId)).ToList();
			_response.StatusCode = HttpStatusCode.OK;
			_response.Data = orders;
			return Ok(_response);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<APIResponse>> GetOrder(int id)
		{
			var order = await _unitOfWork.Orders.GetAsync(
				o => o.Id == id,
				includeProperties: "OrderItems,Address");
			if (order == null)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
				_response.Errors = new List<string> { "Order not found" };
				return NotFound(_response);
			}

			_response.Data = new List<Order> { order }; 
			_response.StatusCode = HttpStatusCode.OK;
			return Ok(_response);
		}

		//[HttpGet]
		//public async Task<ActionResult<APIResponse>> AddOrder(string paymentIntentId)
		//{
		//	var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		//	var addressId = _unitOfWork.Addresses.GetAsync(o => o.UserId == userId);
		//	if (userId == null)
		//	{
		//		_response.StatusCode = HttpStatusCode.Unauthorized;
		//		_response.Errors = new List<string> { "User not authorized" };
		//		return Unauthorized(_response);
		//	}

		//	//var orders = await _unitOfWork.Orders.GetOrdersByUserIdAsync(userId);
		//	await _unitOfWork.Orders.CreateAsync(new Order { 
		//		AddressId = 1,
		//		OrderDate = DateTime.Now,
		//		PaymentIntentId = paymentIntentId
		//	});
		//	_response.StatusCode = HttpStatusCode.OK;
		//	return Ok(_response);
		//}

	}
}
