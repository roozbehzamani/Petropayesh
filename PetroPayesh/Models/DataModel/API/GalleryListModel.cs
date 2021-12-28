using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetroPayesh.Models.DataModel.API
{
    public class GalleryListModel
    {
        public int ID { get; set; }
        public string galleryName { get; set; }
        public string galleryDescription { get; set; }
        public string galleryImage { get; set; }
    }
}