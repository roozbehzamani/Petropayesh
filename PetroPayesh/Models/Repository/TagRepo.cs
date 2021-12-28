using PetroPayesh.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace PetroPayesh.Models.Repository
{
    public class TagRepo
    {
        DataBase db = new DataBase();
        public List<Tbl_Tag> getTagList(int id)
        {
            List<Tbl_Tag> qTags = (from a in db.Tbl_Tag
                                   where a.MainProductID.Equals(id)
                                   select a).ToList();

            return qTags;
        }
    }
}