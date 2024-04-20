using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostCore.Models;

public partial class Amdistrib
{
    [Key]
    public Guid Uniquedistributionid { get; set; } = Guid.NewGuid();

    public Guid Uniqueassetiddistr { get; set; }

    [Required]
    public string Assetdistributionowner { get; set; } = string.Empty;

    [Required]
    public string Assetdistributionlocation { get; set; } = string.Empty;

    [Required]
    public int Assetdistributionquantity { get; set; }

    [Required]
    public DateOnly Assetdistributiondateassigned { get; set; }

    public virtual AssetMgmt? UniqueassetiddistrNavigation { get; set; }
}
