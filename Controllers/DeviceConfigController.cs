using AolDevicesConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceConfigController : ControllerBase
    {
        private ConfigContext _deviceConfigContext;
        public DeviceConfigController(ConfigContext configContext)
        {
            _deviceConfigContext = configContext;
        }

        [HttpPost("SaveDeviceConfig")]
        public async Task<IActionResult> SaveConfig(DeviceConfiguration config)
        {
            var devConf = _deviceConfigContext.DeviceConfigurations.FirstOrDefault(q => q.PID == config.PID);
            if(devConf != null ){
                devConf.PIDConfig = config.PIDConfig;
            } 
            else{
                await  _deviceConfigContext.DeviceConfigurations.AddAsync(config);
            }
            
            var result = await _deviceConfigContext.SaveChangesAsync();

            return Ok(result);
        }

        [HttpGet("GetConfig")]
        public async Task<IActionResult> GetConfig(){
            return Ok(await _deviceConfigContext.DeviceConfigurations.ToListAsync());
        }

        [HttpGet("GetByPID")]
        public async Task<IActionResult> GetConfig(string pid){
            return Ok(await _deviceConfigContext.DeviceConfigurations
            .OrderBy(q => q.ConfigID)
            .LastOrDefaultAsync(q => q.PID == pid));
        }
    }
}
