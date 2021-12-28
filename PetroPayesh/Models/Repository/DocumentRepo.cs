using PetroPayesh.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PetroPayesh.Models.Repository
{
    public class DocumentRepo
    {
        DataBase db = new DataBase();
        public async Task<List<Tbl_Documents>> getDocumentsList()
        {
            List<Tbl_Documents> qDocument = await (from a in db.Tbl_Documents
                                                   select a).ToListAsync();

            return qDocument;
        }
    }
}