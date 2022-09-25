using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Mesaj GetByID(int id)
        {
            return _messageDal.Get(x => x.MessageID == id);
        }

        public List<Mesaj> GetListInbox()
        {
            return _messageDal.List(x => x.ReceiverMail == "admin@gmail.com");
        }

        public List<Mesaj> GetListSendbox()
        {
            return _messageDal.List(x => x.SenderMail == "admin@gmail.com"); 
        }

        public void MessageAdd(Mesaj message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Mesaj message)
        {
            throw new NotImplementedException();
        }

        public void MessageUpdate(Mesaj message)
        {
            throw new NotImplementedException();
        }
    }
}
