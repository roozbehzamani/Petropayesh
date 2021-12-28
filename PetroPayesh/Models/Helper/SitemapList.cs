using PetroPayesh.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetroPayesh.Models.Helper
{
    public class SitemapList
    {
        public List<SitemapItem> Sitemap_Items(string baseUrl)
        {
            ProductRepo productRepo = new ProductRepo();
            var sitemapItems = new List<SitemapItem>();

            sitemapItems.Add(new SitemapItem(baseUrl + "/Home/Index", changeFrequency: SitemapChangeFrequency.Always, priority: 1.0, lastModified: DateTime.Now));
            sitemapItems.Add(new SitemapItem(baseUrl + "/Home/Agencies", changeFrequency: SitemapChangeFrequency.Always, priority: 1.0, lastModified: DateTime.Now));
            sitemapItems.Add(new SitemapItem(baseUrl + "/Home/ContactUs", changeFrequency: SitemapChangeFrequency.Always, priority: 1.0, lastModified: DateTime.Now));
            sitemapItems.Add(new SitemapItem(baseUrl + "/Home/ShoppingCart", changeFrequency: SitemapChangeFrequency.Always, priority: 1.0, lastModified: DateTime.Now));
            sitemapItems.Add(new SitemapItem(baseUrl + "/Home/Documents", changeFrequency: SitemapChangeFrequency.Always, priority: 1.0, lastModified: DateTime.Now));
            sitemapItems.Add(new SitemapItem(baseUrl + "/Home/Gallery", changeFrequency: SitemapChangeFrequency.Always, priority: 1.0, lastModified: DateTime.Now));

            foreach (var item in productRepo.getProductsID())
            {
                sitemapItems.Add(new SitemapItem(baseUrl + "/Home/ProductSingle/" + item.ToString(), changeFrequency: SitemapChangeFrequency.Always, priority: 1.0, lastModified: DateTime.Now));
            }

            return sitemapItems;
        }
    }
}