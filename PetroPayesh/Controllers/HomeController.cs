using PetroPayesh.Models.DataModel.API;
using PetroPayesh.Models.Domain;
using PetroPayesh.Models.Helper;
using PetroPayesh.Models.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;

namespace PetroPayesh.Controllers
{
    public class HomeController : Controller
    {
        DataBase db;
        ProductRepo productRepo;
        CommentRepo commentRepo;
        AgenciesRepo agenciesRepo;
        ContactUsRepo contactUsRepo;
        ShoppingCartRepo cartRepo;
        GalleryRepo galleryRepo;
        DocumentRepo documentRepo;
        SitemapList sitemapList;




        public async Task<ActionResult> Index()
        {
            try
            {
                cartRepo = new ShoppingCartRepo();
                productRepo = new ProductRepo();
                string UserIP = this.Request.UserHostAddress;
                int Count = await cartRepo.CartCount(UserIP);
                Session["Count"] = Count;
                List<Tbl_Products> lstProducts = await productRepo.getAllProductList();
                return View(lstProducts);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public async Task<ActionResult> ProductSingle(int ID)
        {
            try
            {
                cartRepo = new ShoppingCartRepo();
                productRepo = new ProductRepo();
                string UserIP = this.Request.UserHostAddress;
                int Count = await cartRepo.CartCount(UserIP);
                Session["Count"] = Count;
                Tbl_Products Products = await productRepo.getSingleProduct(ID);
                return View(Products);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public async Task<JsonResult> SaveComment(string commentText, string phone, string name, int productID)
        {
            try
            {
                commentRepo = new CommentRepo();
                var result = await commentRepo.addNewComment(commentText, phone, name, productID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> Agencies()
        {
            try
            {
                cartRepo = new ShoppingCartRepo();
                agenciesRepo = new AgenciesRepo();
                string UserIP = this.Request.UserHostAddress;
                int Count = await cartRepo.CartCount(UserIP);
                Session["Count"] = Count;
                List<Tbl_Representations> lstAgencies = await agenciesRepo.getAgenciesList();
                return View(lstAgencies);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public async Task<ActionResult> ContactUs()
        {
            try
            {
                cartRepo = new ShoppingCartRepo();
                string UserIP = this.Request.UserHostAddress;
                int Count = await cartRepo.CartCount(UserIP);
                Session["Count"] = Count;
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public async Task<JsonResult> SaveMessage(string name, string email, string phone, string message)
        {
            try
            {
                contactUsRepo = new ContactUsRepo();
                var result = await contactUsRepo.addNewMessage(name, email, phone, message);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> ShoppingCart()
        {
            try
            {
                cartRepo = new ShoppingCartRepo();
                string UserIP = this.Request.UserHostAddress;
                int Count = await cartRepo.CartCount(UserIP);
                Session["Count"] = Count;
                List<Tbl_ShoppingCart> lstCart = await cartRepo.getShoppingCartItemsList(UserIP);
                return View(lstCart);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public async Task<JsonResult> addShoppingCart(int ProductID, int ColorID, int Count)
        {
            try
            {
                cartRepo = new ShoppingCartRepo();
                string UserIP = this.Request.UserHostAddress;
                var result = await cartRepo.addToShoppingCart(ProductID, ColorID, Count, UserIP);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> AddCartItemCount(int ProductID)
        {
            try
            {
                db = new DataBase();
                cartRepo = new ShoppingCartRepo();
                string UserIP = this.Request.UserHostAddress;
                var result = await cartRepo.addCartItemCount(ProductID, UserIP);
                if (result.Equals("1"))
                {
                    int totalPrice = 0;
                    var qAllCartItem = await (from a in db.Tbl_ShoppingCart
                                              where a.UserIP.Equals(UserIP)
                                              select a).ToListAsync();

                    foreach (var item in qAllCartItem)
                    {
                        var qFindProduct = await (from a in db.Tbl_Products
                                                  where a.ID.Equals(item.ProductID)
                                                  select a).SingleOrDefaultAsync();
                        totalPrice = totalPrice + (item.ProductCount * qFindProduct.Price);
                    }
                    return Json(totalPrice, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                db.Dispose();
            }
        }
        public async Task<JsonResult> MinesCartItemCount(int ProductID)
        {
            try
            {
                db = new DataBase();
                cartRepo = new ShoppingCartRepo();
                string UserIP = this.Request.UserHostAddress;
                var result = await cartRepo.minesCartItemCount(ProductID, UserIP);
                if (result.Equals("1"))
                {
                    int totalPrice = 0;
                    var qAllCartItem = await (from a in db.Tbl_ShoppingCart
                                              where a.UserIP.Equals(UserIP)
                                              select a).ToListAsync();

                    foreach (var item in qAllCartItem)
                    {
                        var qFindProduct = await (from a in db.Tbl_Products
                                                  where a.ID.Equals(item.ProductID)
                                                  select a).SingleOrDefaultAsync();
                        totalPrice = totalPrice + (item.ProductCount * qFindProduct.Price);
                    }
                    return Json(totalPrice, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                db.Dispose();
            }
        }
        public async Task<JsonResult> DeleteCartItem(int CartItemID)
        {
            try
            {
                cartRepo = new ShoppingCartRepo();
                var result = await cartRepo.DeleteCartItem(CartItemID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> UpdateCartItemColor(int colorID, int ID)
        {
            try
            {
                cartRepo = new ShoppingCartRepo();
                var result = await cartRepo.UpdateCartItemColor(ID, colorID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> SaveShoppingCart(string name, string phone, string address)
        {
            try
            {
                cartRepo = new ShoppingCartRepo();
                string UserIP = this.Request.UserHostAddress;
                var result = await cartRepo.addFactor(name, phone, address, UserIP);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> Gallery()
        {
            try
            {
                cartRepo = new ShoppingCartRepo();
                galleryRepo = new GalleryRepo();
                string UserIP = this.Request.UserHostAddress;
                int Count = await cartRepo.CartCount(UserIP);
                Session["Count"] = Count;
                List<Tbl_Gallery> lstGallery = await galleryRepo.getGalleryList();
                return View(lstGallery);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public async Task<ActionResult> Documents()
        {
            try
            {
                documentRepo = new DocumentRepo();
                cartRepo = new ShoppingCartRepo();
                string UserIP = this.Request.UserHostAddress;
                int Count = await cartRepo.CartCount(UserIP);
                Session["Count"] = Count;
                List<Tbl_Documents> lstProducts = await documentRepo.getDocumentsList();
                return View(lstProducts);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public FilePathResult CatalogPDFDownload(string fileName)
        {
            return File(Server.MapPath("~/Content/Files/" + fileName), "application/pdf", fileName);
        }
        public ActionResult SiteMap()
        {
            sitemapList = new SitemapList();
            string baseUrl = string.Format("{0}://{1}", this.Request.Url.Scheme, this.Request.Url.Authority);
            return new SitemapResult(sitemapList.Sitemap_Items(baseUrl));
        }
        public ActionResult GenerateSiteMap()
        {
            sitemapList = new SitemapList();
            string baseUrl = string.Format("{0}://{1}", this.Request.Url.Scheme, this.Request.Url.Authority);

            SitemapGenerator sg = new SitemapGenerator();
            var doc = sg.GenerateSiteMap(sitemapList.Sitemap_Items(baseUrl));

            doc.Save(Server.MapPath("~/Sitemap.xml"));

            return RedirectToAction("Index", "Home");
        }
        //public ActionResult AddNewSitemapElement()
        //{
        //    SitemapGenerator sg = new SitemapGenerator();
        //    //create a sitemap item
        //    var siteMapItem = new SitemapItem(Url.Action("NewAdded", "NewController"), changeFrequency: SitemapChangeFrequency.Always, priority: 1.0);

        //    //Get the XElement from SitemapGenerator.CreateItemElement
        //    var NewItem = sg.CreateItemElement(siteMapItem);

        //    //create XMLdocument element to add the new node in the file
        //    XmlDocument document = new XmlDocument();

        //    //load the already created XML file
        //    document.Load(Server.MapPath("~/Sitemap.xml"));

        //    //convert XElement into XmlElement
        //    XmlElement childElement = document.ReadNode(NewItem.CreateReader()) as XmlElement;
        //    XmlNode parentNode = document.SelectSingleNode("urlset");

        //    //This line of code get's urlset with it's last child and append the new Child just before the last child
        //    document.GetElementsByTagName("urlset")[0].InsertBefore(childElement, document.GetElementsByTagName("urlset")[0].LastChild);

        //    //save the updated file
        //    document.Save(Server.MapPath("~/Sitemap.xml"));

        //    return RedirectToAction("Index", "Home");
        //}

        //api

        public async Task<JsonResult> LoadingAPI(string version)
        {
            db = new DataBase();
            var qVersion = await (from a in db.Tbl_Setting
                                  select a).FirstOrDefaultAsync();

            int result;
            if (qVersion.AppVersion.Equals(version))
            {
                result = 1;
            }
            else
            {
                result = 0;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> ProductListAPI()
        {
            db = new DataBase();
            List<Tbl_Products> list = await (from a in db.Tbl_Products
                                             select a).ToListAsync();

            List<ProductListModel> productLists = new List<ProductListModel>();
            ProductListModel product;
            foreach (var item in list)
            {
                var productImage = await (from a in db.Tbl_ProductImages
                                          where a.ProductId.Equals(item.ID)
                                          select a).FirstOrDefaultAsync();

                product = new ProductListModel();
                product.ID = item.ID;
                product.productImage = productImage.Image;
                product.productName = item.Name;
                product.productStock = "500";
                product.prouductPrice = item.Price.ToString();

                productLists.Add(product);
            }
            return Json(productLists, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GalleryListAPI()
        {
            db = new DataBase();
            List<Tbl_Gallery> list = await (from a in db.Tbl_Gallery
                                            select a).ToListAsync();

            List<GalleryListModel> galleryLists = new List<GalleryListModel>();
            GalleryListModel gallery;
            foreach (var item in list)
            {
                gallery = new GalleryListModel();
                gallery.ID = item.ID;
                gallery.galleryDescription = item.Description;
                gallery.galleryImage = item.Image;
                gallery.galleryName = item.Name;

                galleryLists.Add(gallery);
            }
            return Json(galleryLists, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> ProductSingleAPI(int id)
        {
            db = new DataBase();
            Tbl_Products product = await (from a in db.Tbl_Products
                                          where a.ID.Equals(id)
                                          select a).SingleOrDefaultAsync();

            var productImage = await (from a in db.Tbl_ProductImages
                                      where a.ProductId.Equals(product.ID)
                                      select a).FirstOrDefaultAsync();

            ProductSingleModel productSingle = new ProductSingleModel();

            productSingle.ID = product.ID;
            productSingle.productDescrition = product.AndroidText;
            productSingle.productImage = productImage.Image;
            productSingle.productName = product.Name;
            productSingle.prouductPrice = product.Price.ToString();

            return Json(productSingle, JsonRequestBehavior.AllowGet);
        }
    }
}