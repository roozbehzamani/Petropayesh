using System;

namespace PetroPayesh.Models.Helper
{
    public class SitemapItem : ISitemapItem
    {
        public SitemapItem(string url, DateTime? lastModified = null, SitemapChangeFrequency? changeFrequency = null, double? priority = null)
        {

            Url = url;
            LastModified = lastModified;
            ChangeFrequency = changeFrequency;
            Priority = priority;
        }
        public string Url { get; protected set; }

        public DateTime? LastModified { get; protected set; }

        public SitemapChangeFrequency? ChangeFrequency { get; protected set; }

        public double? Priority { get; protected set; }
    }
}