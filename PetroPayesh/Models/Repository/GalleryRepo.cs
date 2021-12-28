using PetroPayesh.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PetroPayesh.Models.Repository
{
    public class GalleryRepo
    {
        DataBase db = new DataBase();
        public async Task<List<Tbl_Gallery>> getGalleryList()
        {
            List<Tbl_Gallery> qGalleryList = await (from a in db.Tbl_Gallery
                                                    select a).ToListAsync();

            return qGalleryList;
        }
        public List<Tbl_Videos> getVideosList()
        {
            List<Tbl_Videos> qGalleryList = (from a in db.Tbl_Videos
                                              select a).ToList();

            return qGalleryList;
        }
    }
}