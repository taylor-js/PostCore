using System;
using System.Collections.Generic;

namespace PostCore.Models;

public partial class Amcontent
{
    public Guid Uniquecontentid { get; set; }

    public Guid Uniqueassetidcont { get; set; }

    public string? Assetcontentnumber { get; set; }

    public string? Assetcontentdescription { get; set; }

    public string? Assetcontentversion { get; set; }

    public DateOnly Assetcontentdateassigned { get; set; }

    public virtual AssetMgmt? UniqueassetidcontNavigation { get; set; }
}
