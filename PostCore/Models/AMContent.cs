using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostCore.Models;

public partial class Amcontent
{
    [Key]
    public Guid Uniquecontentid { get; set; } = Guid.NewGuid();

    public Guid Uniqueassetidcont { get; set; }

    [Required]
    public string Assetcontentnumber { get; set; } = string.Empty;

    [Required]
    public string Assetcontentdescription { get; set; } = string.Empty;

    [Required]
    public string Assetcontentversion { get; set; } = string.Empty;

    [Required]
    public DateOnly Assetcontentdateassigned { get; set; }

    public virtual AssetMgmt? UniqueassetidcontNavigation { get; set; }
}
