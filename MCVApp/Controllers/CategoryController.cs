using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Repository;
using WebApp.Models;
using MCVApp.Utility;
using MCVApp.Repository;

namespace MCVApp.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository objCategory = new CategoryRepository();
        HttpResponseMessage response = new HttpResponseMessage();
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CategoryController));
        /// <summary>
        /// display view with list of categories 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllCategoryDetails()
        {
            try
            {
                ModelState.Clear();
                List<CategoryModel> categoryList = GetCategory();
                return View(categoryList);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message.ToString());
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// get list of all categories
        /// </summary>
        /// <returns></returns>
        private static List<CategoryModel> GetCategory()
        {
            CategoryRepository objCategory = new CategoryRepository();
            HttpResponseMessage response = new HttpResponseMessage();
            response = objCategory.GetCategoryDetails("GetCategory");
            response.EnsureSuccessStatusCode();
            List<CategoryModel> categoryList = response.Content.ReadAsAsync<List<CategoryModel>>().Result;
            return categoryList;
        }
        /// <summary>
        /// Add new category view
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// submit a new category to database
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CategoryModel category)
        {
            try
            {
                if(!UtilityCheck.IsValidCode(category.CategoryCode))
                {
                    ModelState.AddModelError(string.Empty, "Please use correct categor code format e.g ABC123");
                    return View();
                }                
                if(CategoryExist(category.CategoryCode))
                {
                    ModelState.AddModelError(string.Empty, "category code alreay exist.");
                    return View();
                }
                response = objCategory.AddCategory("AddCategory", category);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("GetAllCategoryDetails");                                 
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// check code if it exist or not
        /// </summary>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        private static Boolean CategoryExist(string CategoryCode)
        {
            CategoryRepository objCategory = new CategoryRepository();
            HttpResponseMessage response = new HttpResponseMessage();
            HttpResponseMessage responseCheck = objCategory.GetCategoryByCode("GetCategoryByCode/?CategoryCode=" + CategoryCode.Trim());
            ApiResponse result = responseCheck.Content.ReadAsAsync<ApiResponse>().Result;
            if (result.ResponseType == true) return true;
            return false;
        }
        /// <summary>
        /// Edit category view
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public ActionResult Edit(int CategoryId)
        {
            try
            {
                HttpResponseMessage response = objCategory.GetCategoryByCode("GetCategoryByID/?CategoryId=" + CategoryId);
                response.EnsureSuccessStatusCode();
                CategoryModel category = response.Content.ReadAsAsync<CategoryModel>().Result;
                return View(category);
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            
        }

        /// <summary>
        /// update category details to database
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(CategoryModel category)
        {
            try
            {
                if (!UtilityCheck.IsValidCode(category.CategoryCode))
                {
                    ViewBag.ErrorMessage("Please use correct format e.g ABC123");
                    return View();
                }
                HttpResponseMessage responseCheck = objCategory.UpdateCategory("UpdateCategory" , category);
                responseCheck.EnsureSuccessStatusCode();
                return RedirectToAction("GetAllCategoryDetails");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// view catory details 
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public ActionResult Details(int CategoryId)
        {
            try
            {
                HttpResponseMessage response = objCategory.GetCategoryByCode("GetCategoryByID/?CategoryId=" + CategoryId);
                response.EnsureSuccessStatusCode();
                CategoryModel category = response.Content.ReadAsAsync<CategoryModel>().Result;
                return View(category);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}
