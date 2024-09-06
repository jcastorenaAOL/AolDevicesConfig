﻿using Microsoft.EntityFrameworkCore;

namespace AolDevicesConfig;

public class ConfigContext : DbContext
{
    public ConfigContext(DbContextOptions<ConfigContext> opt ) : base(opt)
    {
    }

    public DbSet<DeviceConfiguration> DeviceConfigurations {get; set;}
    public DbSet<ConfigParameter> ConfigParameters {get; set;}
    public DbSet<DeviceConfigParameter> DeviceConfigParameters {get; set;}
}
