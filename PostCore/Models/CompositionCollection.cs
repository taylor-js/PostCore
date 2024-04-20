using Microsoft.EntityFrameworkCore;
using PostCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PostCore.Models
{
    public class CompositionCollection
    {
        public CompositionCollection()
        {
            AM = new AssetMgmt();
            AM_C = new Amcontent();
            AM_D = new Amdistrib();
            IE_AM = new List<AssetMgmt>();
            IE_AM_C = new List<Amcontent>();
            IE_AM_D = new List<Amdistrib>();
        }

        public AssetMgmt AM { get; set; }
        public Amcontent AM_C { get; set; }
        public Amdistrib AM_D { get; set; }

        public List<AssetMgmt> IE_AM { get; set; }
        public List<Amcontent> IE_AM_C { get; set; }
        public List<Amdistrib> IE_AM_D { get; set; }
    }
}
