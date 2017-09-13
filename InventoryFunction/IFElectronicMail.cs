using ElectronicMailNotification.Models;

namespace AIMS.Classes
{
    public interface IFElectronicMail
    {
        void Send(ElectronicMail electronicMail);
    }
}