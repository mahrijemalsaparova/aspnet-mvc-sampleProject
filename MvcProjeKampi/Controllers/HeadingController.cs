using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager headinManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            //
            var headingValues = headinManager.GetList();
            return View(headingValues);
        }

        [HttpGet]
        public ActionResult AddHeading()
        {
            List<SelectListItem> valuecategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();

           

            List<SelectListItem> valueWriter = (from x in writerManager.GetList() 
                                                select new SelectListItem 
                                                { 
                                                    Text =  x.WriterName + " " + x.WriterSurname,
                                                    Value = x.WriterID.ToString(),
                                                }).ToList();
            ViewBag.vlc = valuecategory;
            ViewBag.vlw = valueWriter;
            return View();  
        }

        [HttpPost]
        public ActionResult AddHeading(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            headinManager.HeadingAdd(heading);
            return RedirectToAction("Index");
        }
    }
}