using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

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


            CategoryValidator validationRules = new CategoryValidator();
            
            ValidationResult validationResult = validationRules.Validate(item);

            if (validationResult.IsValid)
            {
                categoryManager.CategoryAddBL(item);
                return RedirectToAction("GetCategoryList");
            }
            else
            {
                foreach (var param in validationResult.Errors)
                {
                    ModelState.AddModelError(param.PropertyName, param.ErrorMessage);
                }
            }
            return RedirectToAction("GetCategoryList");
        }
    }
}