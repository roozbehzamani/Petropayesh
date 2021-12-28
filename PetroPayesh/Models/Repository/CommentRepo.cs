using PetroPayesh.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PetroPayesh.Models.Repository
{
    public class CommentRepo
    {
        DataBase db = new DataBase();
        
        public List<Tbl_Comments> getCommentList(int ID)
        {
            try
            {
                List<Tbl_Comments> qComments = (from a in db.Tbl_Comments
                                                where a.ProductID.Equals(ID) && a.ShowStatus.Equals(true)
                                                select a).ToList();

                return qComments;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> addNewComment(string commentText, string phone, string name, int productID)
        {
            try
            {
                Tbl_Comments newComment = new Tbl_Comments();

                newComment.Comment = commentText;
                newComment.CommentDate = DateTime.Now;
                newComment.CommentTime = DateTime.Now.ToShortTimeString();
                newComment.Name = name;
                newComment.Phone = phone;
                newComment.ProductID = productID;
                newComment.Rate = 1;
                newComment.ShowStatus = false;
                newComment.ReadStatus = false;

                db.Tbl_Comments.Add(newComment);
                if(Convert.ToBoolean(await db.SaveChangesAsync()))
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

        public async Task<List<Tbl_Comments>> getUnReadedComments()
        {
            try
            {
                List<Tbl_Comments> qGetComment = await (from a in db.Tbl_Comments
                                                        where a.ReadStatus.Equals(false)
                                                        select a).ToListAsync();

                return qGetComment;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<int> getUnReadedCommentCount()
        {
            List<Tbl_Comments> coumentsCount = await (from a in db.Tbl_Comments
                               where a.ReadStatus.Equals(false)
                               select a).ToListAsync();

            int counter = coumentsCount.Count();

            return counter;
        }
    }
}