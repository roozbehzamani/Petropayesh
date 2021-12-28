using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetroPayesh.Models.Helper
{
    public interface ISitemapItem
    {
        string Url { get; }

        DateTime? LastModified { get; }

        SitemapChangeFrequency? ChangeFrequency { get; }

        double? Priority { get; }
    }
}
