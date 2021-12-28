using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetroPayesh.Models.DataModel
{
    public class CommentWithProductName
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public string ProductName { get; set; }
        public System.DateTime CommentDate { get; set; }
        public string CommentTime { get; set; }
        public bool ReadStatus { get; set; }
        public bool ShowStatus { get; set; }
    }
}