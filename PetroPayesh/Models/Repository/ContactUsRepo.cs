using PetroPayesh.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PetroPayesh.Models.Repository
{
    public class ContactUsRepo
    {
        DataBase db = new DataBase();
        public async Task<string> addNewMessage(string name , string email , string phone , string message)
        {
            try
            {
                Tbl_ContactUs newContactUs = new Tbl_ContactUs();

                newContactUs.Email = email;
                newContactUs.Message = message;
                newContactUs.Name = name;
                newContactUs.Phone = phone;
                newContactUs.ReadStatus = false;

                db.Tbl_ContactUs.Add(newContactUs);
                if (Convert.ToBoolean(await db.SaveChangesAsync()))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<int> getUnReadedMessageCount()
        {
            List<Tbl_ContactUs> messageCount = await (from a in db.Tbl_ContactUs
                                                      where a.ReadStatus.Equals(false)
                                                      select a).ToListAsync();

            int counter = messageCount.Count();

            return counter;
        }
    }
}