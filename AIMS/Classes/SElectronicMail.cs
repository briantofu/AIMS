using ElectronicMailNotification.Models;

namespace AIMS.Classes
{
    public static class SElectronicMail
    {
        public static void Send(this ElectronicMail electronicMail)
        {
            IFElectronicMail iFElectronicMail = new FElectronicMail();
            iFElectronicMail.Send(electronicMail);
        }
    }
}