using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CategoryController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController1/Create
        public ActionResult Create(CategoryModel ctmodel)
        {
            if(ctmodel != null)
            {

            }
            return View();
        }


        public ActionResult AddDetails()
        {
               return View();

        }

        // GET: CategoryController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
