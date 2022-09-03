using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Default

        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult GetCategoryList()
        {
            var categoryValues = categoryManager.GetList(); 

            return View(categoryValues);
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();

        }


        [HttpPost]
        public ActionResult AddCategory(Category item)
        {
            //categoryManager.CategoryAddBL(item);

            return RedirectToAction("GetCategoryList");
        }
    }
}