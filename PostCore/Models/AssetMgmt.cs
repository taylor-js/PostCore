using System;
using System.Collections.Generic;

namespace PostCore.Models;

public partial class AssetMgmt
{
    public Guid Uniqueassetid { get; set; }

    public long Assetid { get; set; }

    public string? Assettype { get; set; }

    public string? Assetname { get; set; }

    public string? Assetmanufacturer { get; set; }

    public string? Assetcategory { get; set; }

    public long Assetworkordernumber { get; set; }

    public long Assetpurchaseordernumber { get; set; }

    public DateOnly Assetdate { get; set; }

    public string? Assetprojectmanager { get; set; }

    public decimal Assetequipmentamount { get; set; }

    public string? Assetdescription { get; set; }

    public virtual ICollection<Amcontent> Amcontents { get; set; } = new List<Amcontent>();

    public virtual ICollection<Amdistrib> Amdistribs { get; set; } = new List<Amdistrib>();
}
