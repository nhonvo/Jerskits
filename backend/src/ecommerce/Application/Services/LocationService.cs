using System.Diagnostics;
using System.Text;
using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Common.Models;
using Newtonsoft.Json;

namespace ecommerce.Application.Services;

public class LocationService : ILocationService
{
    public async Task<List<LocationDTO>> GetLocationData(string? countryCode, string? stateCode)
    {
        try
        {
            string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "SolutionFolder", "location", "getLocationCode.js");
            string nodePath = "node";
            string arguments = GetArguments(countryCode, stateCode, scriptPath);
            string result = await ExecuteNodeProcess(nodePath, arguments);
            var location = JsonConvert.DeserializeObject<List<LocationDTO>>(result);

            return location;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting location data.", ex);
        }
    }

    private string GetArguments(string? countryCode, string? stateCode, string scriptPath)
    {
        string methodName = GetMethodName(countryCode, stateCode);
        if (string.IsNullOrEmpty(methodName))
        {
            throw new ArgumentException("Invalid method name.");
        }

        return $"\"{scriptPath}\" \"{methodName}\"" +
            (countryCode != null ? $" \"{countryCode}\"" : "") +
            (stateCode != null ? $" \"{stateCode}\"" : "");
    }

    private string GetMethodName(string? countryCode, string? stateCode)
    {
        if (string.IsNullOrEmpty(countryCode) && string.IsNullOrEmpty(stateCode))
        {
            return "getCountries";
        }
        else if (!string.IsNullOrEmpty(countryCode) && string.IsNullOrEmpty(stateCode))
        {
            return "getStates";
        }
        else if (!string.IsNullOrEmpty(countryCode) && !string.IsNullOrEmpty(stateCode))
        {
            return "getCities";
        }
        return "";
    }

    private async Task<string> ExecuteNodeProcess(string nodePath, string arguments)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = nodePath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8
        };

        using (Process process = Process.Start(startInfo))
        {
            if (process == null)
            {
                throw new Exception("Failed to start Node.js process.");
            }

            string result = await process.StandardOutput.ReadToEndAsync();

            process.WaitForExit();

            return result;
        }
    }
}
