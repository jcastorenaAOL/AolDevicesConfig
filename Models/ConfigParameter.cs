﻿using System.ComponentModel.DataAnnotations;

namespace AolDevicesConfig;


public class ConfigParameter
{

    [Key]
    public int Id { get; set; }
    [Required]
    public string? ParameterName { get; set; }

}
