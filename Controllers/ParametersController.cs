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

        [HttpPost("RegisterParameter")]
        public async Task<IActionResult> RegisterParameter(ConfigParameter parameter){
            await _configContext.ConfigParameters.AddAsync(parameter);
            try{
                _configContext.SaveChanges();
                return Ok("OK");
            }
            catch(Exception err){
                Console.WriteLine(err);
                return BadRequest("Error ");
            }
        }

        [HttpPost("SetConfigParameter")]
        public async Task<IActionResult> SetConfigParameter(string devicePID, string parameter ){
            
            DeviceConfiguration deviceConfiguration = await _configContext.DeviceConfigurations
                                                    .OrderBy(q => q.ConfigID)
                                                    .LastOrDefaultAsync(q => q.PID == devicePID);
            
            if(deviceConfiguration == null){
                return BadRequest($"Configuration for {devicePID} doesn't exist");
            }

            ConfigParameter configParameter = await _configContext.ConfigParameters
                                                .FirstOrDefaultAsync(q => q.ParameterName == parameter)!;

            if(configParameter == null){
                return BadRequest($"Parameter {parameter} doesn't exist");
            }
            
                DeviceConfigParameter devcon = new(){
                    ConfigParameter = configParameter.Id,
                    DeviceConfiguration = deviceConfiguration.ConfigID
                };
                
                await _configContext.DeviceConfigParameters.AddAsync(devcon);
                
                try{
                    await _configContext.SaveChangesAsync();
                    return Ok(devcon);
                }
                catch(Exception err){
                return BadRequest("Error saving config parameter");
            }
        }

    }



