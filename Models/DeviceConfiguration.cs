using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace AolDevicesConfig;

public class DeviceConfiguration
{
    
    [Key]
    public int ConfigID {get; set;} = 0;
    public string? PID {get; set;}
    public string? PIDConfig {get; set;}
    public DateTime CreatedOn {get; set;} = DateTime.Now;

}

