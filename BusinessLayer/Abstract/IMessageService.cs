using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<Mesaj> GetListInbox();
        List<Mesaj> GetListSendbox();

        void MessageAdd(Mesaj message);

        Mesaj GetByID(int id);

        void MessageDelete(Mesaj message);
        void MessageUpdate(Mesaj message);
    }
}
