using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FluentValidation.Results;
using BusinessLayer.ValidationRules;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager headinManager = new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        WriterValidator validationRules = new WriterValidator();
        Context context = new Context();

        // GET: WriterPanel
        [HttpGet]
        public ActionResult WriterProfile(int id = 0)
        {
            string p = (string)Session["WriterMail"];
            id = context.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var writerValue = writerManager.GetByID(id);
            return View(writerValue);
        }

        [HttpPost]
        public ActionResult WriterProfile(Writer writer)
        {
            ValidationResult validationResult = validationRules.Validate(writer);
            if (validationResult.IsValid)
            {
                writerManager.WriterUpdate(writer);
                return RedirectToAction("AllHeadings","WriterPanel");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult MyHeading(string p)
        {

            //sessionda tutulan mail adresinden id çekmeye çalışıyoruz
            p = (string)Session["WriterMail"];
            var writerIdInfo = context.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();

            var values = headinManager.GetListByWriter(writerIdInfo);
            return View(values);
        }

        [HttpGet]

        public ActionResult NewHeading()
        {


            List<SelectListItem> valuecategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }



        [HttpPost]

        public ActionResult NewHeading(Heading heading)
        {
            string writerMailInfo = (string)Session["WriterMail"];
            var writerIdInfo = context.Writers.Where(x => x.WriterMail == writerMailInfo).Select(y => y.WriterID).FirstOrDefault();
            ViewBag.d = writerIdInfo;
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            heading.WriterID = writerIdInfo;
            heading.HeadingStatus = true;
            headinManager.HeadingAdd(heading);
            return RedirectToAction("MyHeading");

        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valuecategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();

            ViewBag.vlc = valuecategory;
            var headingValue = headinManager.GetByID(id);
            return View(headingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            headinManager.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingValue = headinManager.GetByID(id);
            headingValue.HeadingStatus = false;
            headinManager.HeadingDelete(headingValue);
            return RedirectToAction("MyHeading");
        }

        //1 in anlamı sayfalama kaçtan başlıcak belirtmek için

        public ActionResult AllHeadings(int page = 1)
        {
            var headings = headinManager.GetList().ToPagedList(page, 4);
            return View(headings);
        }
    }
}