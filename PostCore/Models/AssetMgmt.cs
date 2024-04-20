using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostCore.Models;

public partial class AssetMgmt
{
    [Key]
    public Guid Uniqueassetid { get; set; } = Guid.NewGuid(); // Initialized with a new GUID

    [Required]
    [Range(10000, 99999)]
    public long Assetid { get; set; }

    [Required]
    public string Assettype { get; set; } = string.Empty; // Initialized

    [Required]
    public string Assetname { get; set; } = string.Empty; // Initialized

    [Required]
    public string Assetmanufacturer { get; set; } = string.Empty; // Initialized

    [Required]
    public string Assetcategory { get; set; } = string.Empty; // Initialized

    [Required]
    [Range(1000000000, 9999999999)]
    public long Assetworkordernumber { get; set; }

    [Required]
    [Range(1000000000, 9999999999)]
    public long Assetpurchaseordernumber { get; set; }

    [Required]
    public DateOnly Assetdate { get; set; } // Requires a value to be set explicitly

    [Required]
    public string Assetprojectmanager { get; set; } = string.Empty; // Initialized

    [Required]
    [DisplayFormat(DataFormatString = "{0:C}")]
    public decimal Assetequipmentamount { get; set; }

    [Required]
    public string Assetdescription { get; set; } = string.Empty; // Initialized

    public virtual ICollection<Amcontent> Amcontents { get; set; } = new List<Amcontent>();
    public virtual ICollection<Amdistrib> Amdistribs { get; set; } = new List<Amdistrib>();
}
