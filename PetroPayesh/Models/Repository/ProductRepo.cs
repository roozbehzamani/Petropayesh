using PetroPayesh.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PetroPayesh.Models.Repository
{
    public class ProductRepo
    {
        DataBase db = new DataBase();

        public async Task<List<Tbl_Products>> getAllProductList()
        {
            List<Tbl_Products> qProduct = await (from a in db.Tbl_Products
                                                 select a).ToListAsync();

            return qProduct;
        }
        public List<Tbl_Products> getAllProductListNotAsync()
        {
            List<Tbl_Products> qProduct = (from a in db.Tbl_Products
                                           select a).ToList();

            return qProduct;
        }
        public async Task<Tbl_Products> getSingleProduct(int ID)
        {
            Tbl_Products qProduct = await (from a in db.Tbl_Products
                                           where a.ID.Equals(ID)
                                           select a).SingleOrDefaultAsync();

            return qProduct;
        }
        public Tbl_Products getSingleCartProduct(int ID)
        {
            Tbl_Products qProduct = (from a in db.Tbl_Products
                                     where a.ID.Equals(ID)
                                     select a).SingleOrDefault();

            return qProduct;
        }
        public List<Tbl_Colors> getColorList(int ID)
        {
            List<Tbl_Colors> qColor = (from a in db.Tbl_Colors
                                       where a.ProductID.Equals(ID)
                                       select a).ToList();

            return qColor;
        }
        public List<Tbl_ProductImages> getProductImageList(int ID)
        {
            List<Tbl_ProductImages> qImageList = (from a in db.Tbl_ProductImages
                                                  where a.ProductId.Equals(ID)
                                                  select a).ToList();

            return qImageList;
        }
        public Tbl_ProductImages getProductImage(int ID)
        {
            Tbl_ProductImages qImage = (from a in db.Tbl_ProductImages
                                                  where a.ProductId.Equals(ID)
                                                  select a).FirstOrDefault();

            return qImage;
        }
        public Tbl_Colors getSingleColor(int ID)
        {
            Tbl_Colors qColors = (from a in db.Tbl_Colors
                                  where a.ID.Equals(ID)
                                  select a).SingleOrDefault();

            return qColors;
        }
        public List<int> getProductsID()
        {
            List<int> qListID = (from a in db.Tbl_Products
                                 select a.ID).ToList();

            return qListID;
        }
    }
}