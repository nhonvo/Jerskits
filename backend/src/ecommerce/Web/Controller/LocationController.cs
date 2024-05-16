using AutoMapper;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Web.Controller
{
    public class LocationController(ILocationService locationService, IMapper mapper) : BaseController
    {
        private readonly ILocationService _locationService = locationService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("")]
        public async Task<IActionResult> GetCountry()
        {
            var data = await _locationService.GetLocationData("", "");
            var country = _mapper.Map<List<Country>>(data);
            return Ok(country);
        }

        [HttpGet("{countryCode}")]
        public async Task<IActionResult> GetAllStateByCountry(string countryCode)
        {
            var locationData = await _locationService.GetLocationData(countryCode, "");
            var country = _mapper.Map<List<State>>(locationData);
            return Ok(country);
        }

        [HttpGet("{countryCode}/{stateCode}")]
        public async Task<IActionResult> GetAllCityByCountryAndState(string countryCode, string stateCode)
        {
            var locationData = await _locationService.GetLocationData(countryCode, stateCode);
            var country = _mapper.Map<List<City>>(locationData);
            return Ok(country);
        }
    }
}
