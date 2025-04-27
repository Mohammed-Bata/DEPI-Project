using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.Dtos;
using MVC.Services;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressService _addressService;
        private readonly TokenProviderService _tokenProviderService;

        public AddressController(AddressService addressService, TokenProviderService tokenProviderService)
        {
            _addressService = addressService;
            _tokenProviderService = tokenProviderService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _addressService.GetAllAsync<APIResponse>();
            List<Address> addressList = new List<Address>();
            if (response != null)
            {
                addressList = JsonConvert.DeserializeObject<List<Address>>(Convert.ToString(response.Data));
            }
            return View(addressList);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(AddressDto dto)
        {
            var response = await _addressService.CreateAsync<APIResponse>(dto);
            if (response != null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("CustomError", response.Errors.FirstOrDefault());
                return View(dto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _addressService.GetAsync<APIResponse>(id);
            AddressDto address = new();
            if (response != null)
            {
                address = JsonConvert.DeserializeObject<AddressDto>(Convert.ToString(response.Data));
            }
            if (address == null)
            {
                return NotFound();
            }
            TempData["id"] = id;
            return View(address);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> UpdateAddress(int id,AddressDto dto)
        {
            var response = await _addressService.UpdateAsync<APIResponse>(id, dto);
            if (response != null)
            {
                TempData["success"] = "Address Updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("CustomError", response.Errors.FirstOrDefault());
                TempData["error"] = "Error encountered.";
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _addressService.GetAsync<APIResponse>(id);
            Address address = new();
            if (response != null)
            {
                address = JsonConvert.DeserializeObject<Address>(Convert.ToString(response.Data));
            }
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(Address dto)
        {
            var response = await _addressService.DeleteAsync<APIResponse>(dto.Id);
            if (response != null)
            {
                TempData["success"] = "Address Deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("CustomError", response.Errors.FirstOrDefault());
                TempData["error"] = "Error encountered.";
                return View(dto);
            }



        }
    }
}
