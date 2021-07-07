using MCVApp.Models;
using MCVApp.Repository;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace MCVApp.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository objproduct = new ProductRepository();
        HttpResponseMessage response = new HttpResponseMessage();
        log4net.ILog loggers = log4net.LogManager.GetLogger(typeof(ProductController));

       /// <summary>
       /// display Details view 
       /// </summary>
       /// <returns></returns>
        public ActionResult Details()
        {
            try
            {
                ProductModel model = PopulateModel(null);
                return View(model);
            }
            catch (Exception ex)
            {
                loggers.Error(ex.Message.ToString());
                return Json("error", JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// display Details view filter by category 
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(string Category)
        {
            try
            {
                ProductModel model = new ProductModel();
                if (string.IsNullOrEmpty(Category))
                {
                    ModelState.AddModelError(string.Empty, "Please select category");
                    model = PopulateModel(null);
                    return View("Details", model);
                }
                model = PopulateModel(Convert.ToInt32(Category));
                return View("Details", model);
            }catch(Exception ex)
            {
                loggers.Error(ex.Message.ToString());
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// display view to add new product
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            try
            {
                Products model = new Products();
                model.CategoryName = new SelectList(GetCategory().ToList(), "Value", "Text", model.FkCategoryId); 
                return View(model);
            }
            catch(Exception ex)
            {
                loggers.Error(ex.Message.ToString());
                return View();
            }

        }

        /// <summary>
        /// save new product
        /// </summary>
        /// <param name="productmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Products productmodel)
        {
            try
            {
                response = objproduct.AddProduct("AddProduct", productmodel);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Details");;
            }
            catch(Exception ex)
            {
                loggers.Error(ex.Message.ToString());
                return View();
            }
        }

        /// <summary>
        /// display view details for edit
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public ActionResult Edit(int ProductId)
        {
            try
            {
                Products product = GetProductById(ProductId);
                return View(product);
            }
            catch (Exception ex)
            {
                loggers.Error(ex.Message);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// save updated imformation
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Products product)
        {
            try
            {
                HttpResponseMessage responseCheck = objproduct.UpdateProduct("UpdateProduct", product);
                responseCheck.EnsureSuccessStatusCode();
                return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                loggers.Error(ex.Message);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// display detele view
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public ActionResult Delete(int productid)
        {
            try
            {
                Products product = GetProductById(productid);
                return View(product);
            }
            catch (Exception ex)
            {
                loggers.Error(ex.Message);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// delete product by set it to in active
        /// </summary>
        /// <param name="productmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(Products productmodel)
        {
            try
            {

                HttpResponseMessage responseCheck = objproduct.DeleteProduct("DeleteProduct/?productID="+ productmodel.ProductId);
                responseCheck.EnsureSuccessStatusCode();
                return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                loggers.Error(ex.Message);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// get product details by productb id
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        private static Products GetProductById(int productid)
        {
            ProductRepository objproduct = new ProductRepository();
            HttpResponseMessage response = objproduct.GetProductById("GetProductById/?productID=" + productid);
            response.EnsureSuccessStatusCode();
            Products product = response.Content.ReadAsAsync<Products>().Result;
            product.CategoryName = new SelectList(GetCategory().ToList(), "Value", "Text", product.FkCategoryId);
            product.SelectedCategory = GetCategory().Where(x => x.Value == product.FkCategoryId.ToString()).FirstOrDefault().Text;
            return product;
        }
        /// <summary>
        /// populate model 
        /// </summary>
        /// <param name="categoryid"></param>
        /// <returns></returns>
        private static ProductModel PopulateModel(int?  categoryid)
        {
            ProductModel model = new ProductModel();
                List<SelectListItem> categoryList = GetCategory();
                model = new ProductModel()
                {
                    Categories = categoryList,
                    Products = categoryid == null? GetProducts():  GetProducts(categoryid)
                };
                return model;
        }
        /// <summary>
        /// get products by category id
        /// </summary>
        /// <param name="categoryid"></param>
        /// <returns></returns>
        private static List<Products> GetProducts(int? categoryid)
        {
            List<Products> productList = new List<Products>();
            HttpResponseMessage productResponse = new HttpResponseMessage();
            ProductRepository objProduct = new ProductRepository();
            productResponse = objProduct.GetCategoryProduct("GetCategoryProduct/?categoryID=" + categoryid);
            return  productResponse.Content.ReadAsAsync<List<Products>>().Result;
        }
        /// <summary>
        /// get all products
        /// </summary>
        /// <returns></returns>
        private static List<Products> GetProducts()
        {
            List<Products> productList = new List<Products>();
            HttpResponseMessage productResponse = new HttpResponseMessage();
            ProductRepository objProduct = new ProductRepository();
            productResponse = objProduct.GetCategoryProduct("GetAllProduct");
            return productResponse.Content.ReadAsAsync<List<Products>>().Result;
        }
        /// <summary>
        /// get list of catergories
        /// </summary>
        /// <returns></returns>
        private static List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            HttpResponseMessage categoryResponse = new HttpResponseMessage();
            CategoryRepository objCategory = new CategoryRepository();
            categoryResponse = objCategory.GetCategoryDetails("GetCategory");
            categoryResponse.EnsureSuccessStatusCode();
            List<CategoryModel> categoryList = categoryResponse.Content.ReadAsAsync<List<CategoryModel>>().Result;
            if(categoryList.Count >0)
            {
                foreach(var i in categoryList)
                {
                    SelectListItem cat = new SelectListItem()
                    {
                        Text = i.Name,
                        Value = i.CategoryId.ToString()
                    };
                    list.Add(cat);
                }
            }
            return list;
        }
        /// <summary>
        /// view - to display product  details 
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public ActionResult View(int productid)
        {
            try
            {
                Products product = GetProductById(productid);
                return View(product); 
            }
            catch (Exception ex)
            {
                loggers.Error(ex.Message);
                return Json("error", JsonRequestBehavior.AllowGet);
            }

        }
    }
}
