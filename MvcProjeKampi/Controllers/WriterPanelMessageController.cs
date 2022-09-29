using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());
        MessageValidator validationRules = new MessageValidator();
     
        public ActionResult Inbox()
        {
           string p = (string)Session["WriterMail"];
           var messageList = messageManager.GetListInbox(p);
           return View(messageList);
        }
        public ActionResult SendBox()
        {
            string p = (string)Session["WriterMail"];
            var messageList = messageManager.GetListSendbox(p);
            return View(messageList);
        }
        public ActionResult MessageListMenu()
        {
            return PartialView();
        }
        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = messageManager.GetByID(id);
            return View(values);

        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = messageManager.GetByID(id);
            return View(values);

        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Mesaj message)
        {
            string sender = (string)Session["WriterMail"];

            ValidationResult validationResult = validationRules.Validate(message);
            if (validationResult.IsValid)
            {
                message.SenderMail = sender;
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortTimeString());
                messageManager.MessageAdd(message);
                return RedirectToAction("SendBox");
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

    }
}