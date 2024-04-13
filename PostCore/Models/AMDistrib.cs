using System;
using System.Collections.Generic;

namespace PostCore.Models;

public partial class Amdistrib
{
    public Guid Uniquedistributionid { get; set; }

    public Guid Uniqueassetiddistr { get; set; }

    public string? Assetdistributionowner { get; set; }

    public string? Assetdistributionlocation { get; set; }

    public int Assetdistributionquantity { get; set; }

    public DateOnly Assetdistributiondateassigned { get; set; }

    public virtual AssetMgmt? UniqueassetiddistrNavigation { get; set; }
}
