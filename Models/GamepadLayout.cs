using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyApp.Namespace;

namespace AolDevicesConfig;


public class LayoutComponent{
    [Key]
    public int Id {get; set; }
    public int Index { get; set; }
    public string? Type { get; set; }
    public string? Value { get; set; }
    
    public int GamepadLayoutId {get; set;}
    public GamepadLayout? GamepadLayout {get; set; } 
}


public class GamepadLayout
{
    [Key]
    public int Id { get; set; }
    public string? LayoutName { get; set; }

    [ForeignKey("GamepadLayoutId")]
    public ICollection<LayoutComponent>? LayoutComponents { get; set; }
}
