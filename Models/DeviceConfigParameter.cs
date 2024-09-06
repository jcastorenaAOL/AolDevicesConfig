using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;

namespace AolDevicesConfig;

public class DeviceConfigParameter
{
    
    [Key]
    public int Id {get; set;} = 0;
    [Required]
    [ForeignKey("DeviceConfig")]
    public int DeviceConfiguration {get; set;}
    [Required]
    [ForeignKey("Parameter")]
    public int ConfigParameter {get; set; } 

    public ConfigParameter Parameter {get; set; } = default!;
    public DeviceConfiguration DeviceConfig {get; set;} = default!;
}


