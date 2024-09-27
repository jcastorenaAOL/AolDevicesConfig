using System.Linq;
using AolDevicesConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;

namespace MyApp.Namespace;

[Route("api/[controller]")]
[ApiController]
public class ParametersController : ControllerBase
{
    private ConfigContext _configContext;

    public ParametersController(ConfigContext configContext)
    {
        _configContext = configContext;
    }

    [HttpGet("GetAllParameters")]
    public async Task<IActionResult> GetAllParameters()
    {
        return Ok(await _configContext.ConfigParameters.ToListAsync());
    }

    [HttpGet("GetAllDeviceParameters")]
    public async Task<IActionResult> GetAllDeviceParameters(){
        return Ok(await _configContext.DeviceConfigParameters.ToListAsync());
    }  

    [HttpPost("RegisterParameter")]
    public async Task<IActionResult> RegisterParameter(ConfigParameter parameter)
    {

        await _configContext.ConfigParameters.AddAsync(parameter);

        try
        {
            _configContext.SaveChanges();
            return Ok("OK");
        }
        catch (Exception err)
        {
            Console.WriteLine(err);
            return BadRequest("Error ");
        }
    }

    [HttpPost("SetConfigParameter")]
    public async Task<IActionResult> SetConfigParameter(string devicePID, string parameter)
    {

        var deviceConfiguration = await _configContext.DeviceConfigurations.FirstOrDefaultAsync(q => q.PID == devicePID);
        if (deviceConfiguration == null)
        {
            return BadRequest($"Configuration for {devicePID} doesn't exist");
        }

        var configParameter = await _configContext.ConfigParameters.FirstOrDefaultAsync(q => q.ParameterName!.ToUpper() == parameter.ToUpper());
        if (configParameter == null)
        {
            return BadRequest($"Parameter {parameter} doesn't exist");
        }

        DeviceConfigParameter exist = _configContext.DeviceConfigParameters.FirstOrDefault(q => q.DeviceConfiguration == deviceConfiguration.ConfigID && q.ConfigParameter == configParameter.Id)!;

        if (exist == null)
        {
            DeviceConfigParameter devcon = new()
            {
                DeviceConfiguration = deviceConfiguration.ConfigID,
                ConfigParameter = configParameter.Id
            };
            await _configContext.DeviceConfigParameters.AddAsync(devcon);

            try
            {
                await _configContext.SaveChangesAsync();
                return Ok(devcon);
            }
            catch (Exception err)
            {
                return BadRequest("Error saving config parameter");
            }
        }
        return Ok("Config already exist!");

    }

    [HttpGet("GetDeviceParameters")]
    public async Task<IActionResult> GetDeviceParameters(string pid)
    {

        var device = await _configContext.DeviceConfigurations.FirstOrDefaultAsync(q => q.PID == pid);
        if(device == null){
            return BadRequest($"Config for {pid} doesn't exist!");
        }

        var parameters = await _configContext.DeviceConfigParameters.Where(q => q.DeviceConfig.PID == device.PID).ToListAsync();
        List<ConfigParameter> paramList = new List<ConfigParameter>(); 
        foreach (var item in parameters)
        {
            paramList.Add(_configContext.ConfigParameters.FirstOrDefault(q => q.Id == item.ConfigParameter)!);
        }

        return Ok(paramList);
    }
}



