using PetroPayesh.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PetroPayesh.Models.Repository
{
    public class ShoppingCartRepo
    {
        DataBase db = new DataBase();
        public async Task<string> addToShoppingCart(int ProductID, int ColorID, int Count, string IP)
        {
            try
            {
                var qProductFind = await (from a in db.Tbl_ShoppingCart
                                          where a.ProductID.Equals(ProductID) && a.UserIP.Equals(IP)
                                          select a).SingleOrDefaultAsync();

                if (qProductFind != null)
                {
                    qProductFind.ProductCount = Count + qProductFind.ProductCount;

                    db.Tbl_ShoppingCart.Attach(qProductFind);
                    db.Entry(qProductFind).State = System.Data.Entity.EntityState.Modified;
                    if (Convert.ToBoolean(await db.SaveChangesAsync()))
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    var qColorFind = await (from a in db.Tbl_Colors
                                            where a.ID.Equals(ColorID)
                                            select a).SingleOrDefaultAsync();

                    if (Count >= qColorFind.Limited)
                    {
                        Tbl_ShoppingCart newCart = new Tbl_ShoppingCart();

                        newCart.ProductColorID = ColorID;
                        newCart.ProductCount = Count;
                        newCart.ProductID = ProductID;
                        newCart.UserIP = IP;

                        db.Tbl_ShoppingCart.Add(newCart);
                        if (Convert.ToBoolean(await db.SaveChangesAsync()))
                        {
                            return "1";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    else
                    {
                        string s = "این رنگ برای سفارش بالای " + qColorFind.Limited.ToString() + "امکان انتخاب دارد";
                        return s;
                    }

                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> addCartItemCount(int ProductID, string IP)
        {
            try
            {
                var qFindProduct = await (from a in db.Tbl_ShoppingCart
                                          where a.ProductID.Equals(ProductID) && a.UserIP.Equals(IP)
                                          select a).SingleOrDefaultAsync();

                qFindProduct.ProductCount++;

                db.Tbl_ShoppingCart.Attach(qFindProduct);
                db.Entry(qFindProduct).State = System.Data.Entity.EntityState.Modified;
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
        public async Task<string> minesCartItemCount(int ProductID, string IP)
        {
            try
            {
                var qFindProduct = await (from a in db.Tbl_ShoppingCart
                                          where a.ProductID.Equals(ProductID) && a.UserIP.Equals(IP)
                                          select a).SingleOrDefaultAsync();

                var qColorFind = await (from a in db.Tbl_Colors
                                        where a.ID.Equals(qFindProduct.ProductColorID)
                                        select a).SingleOrDefaultAsync();

                if (qFindProduct.ProductCount > qColorFind.Limited)
                {
                    qFindProduct.ProductCount--;

                    db.Tbl_ShoppingCart.Attach(qFindProduct);
                    db.Entry(qFindProduct).State = System.Data.Entity.EntityState.Modified;
                    if (Convert.ToBoolean(await db.SaveChangesAsync()))
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    string s = "این رنگ برای سفارش بالای " + qColorFind.Limited.ToString() + "امکان انتخاب دارد";
                    return s;
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> DeleteCartItem(int ProductID)
        {
            try
            {
                var qFindProduct = await (from a in db.Tbl_ShoppingCart
                                          where a.ID.Equals(ProductID)
                                          select a).SingleOrDefaultAsync();

                db.Tbl_ShoppingCart.Remove(qFindProduct);
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
        public async Task<string> UpdateCartItemColor(int ID, int ColorID)
        {
            try
            {
                var qFindProduct = await (from a in db.Tbl_ShoppingCart
                                          where a.ID.Equals(ID)
                                          select a).SingleOrDefaultAsync();

                var qColorFind = await (from a in db.Tbl_Colors
                                        where a.ID.Equals(ColorID)
                                        select a).SingleOrDefaultAsync();

                if (qFindProduct.ProductCount >= qColorFind.Limited)
                {
                    qFindProduct.ProductColorID = ColorID;

                    db.Tbl_ShoppingCart.Attach(qFindProduct);
                    db.Entry(qFindProduct).State = System.Data.Entity.EntityState.Modified;
                    if (Convert.ToBoolean(await db.SaveChangesAsync()))
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    string s = "این رنگ برای سفارش بالای " + qColorFind.Limited.ToString() + "امکان انتخاب دارد";
                    return s;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<List<Tbl_ShoppingCart>> getShoppingCartItemsList(string IP)
        {
            List<Tbl_ShoppingCart> qCartItemList = await (from a in db.Tbl_ShoppingCart
                                                          where a.UserIP.Equals(IP)
                                                          select a).ToListAsync();

            return qCartItemList;
        }
        public async Task<string> addFactor(string name, string phone, string address, string ip)
        {
            try
            {
                Tbl_Factor newFactor = new Tbl_Factor();

                newFactor.Name = name;
                newFactor.Phone = phone;
                newFactor.Address = address;
                newFactor.ReadStatus = false;

                db.Tbl_Factor.Add(newFactor);
                if (Convert.ToBoolean(await db.SaveChangesAsync()))
                {
                    var qCartList = await (from a in db.Tbl_ShoppingCart
                                           where a.UserIP.Equals(ip)
                                           select a).ToListAsync();

                    foreach (var item in qCartList)
                    {
                        Tbl_FactorItems newFactorItem = new Tbl_FactorItems();

                        newFactorItem.FactorID = newFactor.ID;
                        newFactorItem.ProductColorID = item.ProductColorID;
                        newFactorItem.ProductCount = item.ProductCount;
                        newFactorItem.ProductID = item.ProductID;

                        db.Tbl_FactorItems.Add(newFactorItem);
                        db.Tbl_ShoppingCart.Remove(item);
                    }


                    if (Convert.ToBoolean(await db.SaveChangesAsync()))
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }

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
        public async Task<int> CartCount(string ip)
        {
            var q = await (from a in db.Tbl_ShoppingCart
                           where a.UserIP.Equals(ip)
                           select a).ToListAsync();

            return q.Count();
        }
        public async Task<int> getUnReadedFactorsCount()
        {
            List<Tbl_Factor> FactorsCount = await (from a in db.Tbl_Factor
                                                   where a.ReadStatus.HasValue && a.ReadStatus.Value.Equals(false)
                                                   select a).ToListAsync();

            int counter = FactorsCount.Count();

            return counter;
        }
    }
}