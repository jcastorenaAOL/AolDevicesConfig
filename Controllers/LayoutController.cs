using AolDevicesConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayoutController : ControllerBase
    {
        private ConfigContext _configContext;

        public LayoutController(ConfigContext configContext)
        {
            _configContext = configContext;
        }

        [HttpGet("GetAllLayouts")]
        public async Task<IActionResult> getAllLayouts(){
            
            return Ok(await _configContext.GamepadLayouts.ToListAsync());
        }

        [HttpPost("SaveGamepadLayout")]
        public async Task<IActionResult> SaveGamepadLayout(GamepadLayout layout){
            await _configContext.GamepadLayouts.AddAsync(layout);
            await _configContext.SaveChangesAsync();
            return Ok(layout);
        }

        [HttpGet("GetGamepadLayout")]
        public async Task<IActionResult> GetGamepadLayout(string layoutName){

            var layout = await _configContext.GamepadLayouts.Where(q => q.LayoutName == layoutName).FirstOrDefaultAsync();

            var GamepadLayout = await _configContext.LayoutComponents.Where(q => q.GamepadLayoutId == layout.Id).ToListAsync();

            bool a = true;

            return Ok(layout);
        } 
    }
}
