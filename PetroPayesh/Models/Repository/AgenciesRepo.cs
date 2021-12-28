using PetroPayesh.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PetroPayesh.Models.Repository
{
    public class AgenciesRepo
    {
        DataBase db = new DataBase();
        public async Task<List<Tbl_Representations>> getAgenciesList()
        {
            List<Tbl_Representations> qAgencies = await (from a in db.Tbl_Representations
                                                         select a).ToListAsync();

            return qAgencies;
        }
    }
}