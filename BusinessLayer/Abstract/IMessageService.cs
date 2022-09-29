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
        List<Mesaj> GetListInbox(string p);
        List<Mesaj> GetListSendbox(string p);

        void MessageAdd(Mesaj message);

        Mesaj GetByID(int id);

        void MessageDelete(Mesaj message);
        void MessageUpdate(Mesaj message);
    }
}
