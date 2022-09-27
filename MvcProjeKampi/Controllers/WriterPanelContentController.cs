using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelContentController : Controller
    {
        ContentManager contentManager = new ContentManager(new EfContentDal());
        // GET: WriterPanelContent

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyContent(string p)
        {
            //her giriş yapan yazar mailine ait id yi alma
            Context context = new Context();
            p = (string)Session["WriterMail"];
            var writeridinfo = context.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var contentValues = contentManager.GetListByWriter(writeridinfo);
            return View(contentValues);
         
        }
    }
}