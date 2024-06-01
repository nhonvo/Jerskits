using ecommerce.Application.Common.Models;

namespace ecommerce.Application.Common.Interfaces;

public interface ILocationService
{
    public Task<List<LocationDTO>> GetLocationData(string? countryCode, string? stateCode);
}
