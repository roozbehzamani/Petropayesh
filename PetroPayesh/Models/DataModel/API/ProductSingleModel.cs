﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetroPayesh.Models.DataModel.API
{
    public class ProductSingleModel
    {
        public int ID { get; set; }
        public string productName { get; set; }
        public string prouductPrice { get; set; }
        public string productImage { get; set; }
        public string productDescrition { get; set; }
    }
}