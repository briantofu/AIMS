using System.Configuration;
using System.Net.Mail;

namespace ElectronicMailNotification.Models
{
    public class ElectronicMail
    {
        //Values can be set on web.config
        private string mSendToEmail { get; set; }
        private string mFromEmail { get; set; }
        private string mDisplayName { get; set; }
        private string mFromPassword { get; set; }
        private string mCcEmail { get; set; }
        private string mBccEmail { get; set; }
        private string mSubject { get; set; }
        private string mMessage { get; set; }
        private string mSMTP { get; set; }
        private int mPort { get; set; }
        private bool mSSL { get; set; }
        private bool mHTML { get; set; }

        public string SendToEmail
        {
            get
            {
                if (!string.IsNullOrEmpty(mSendToEmail))
                {
                    return mSendToEmail;
                }
                else
                {
                    return ConfigurationManager.AppSettings["ElectronicMailSendToEmail"];
                }
            }
            set
            {
                mSendToEmail = value;
            }
        }
        public string FromEmail
        {
            get
            {
                if (!string.IsNullOrEmpty(mFromEmail))
                {
                    return mFromEmail;
                }
                else
                {
                    return ConfigurationManager.AppSettings["ElectronicMailFromEmail"];
                }
            }
            set
            {
                mFromEmail = value;
            }
        }
        public string FromPassword
        {
            get
            {
                if (!string.IsNullOrEmpty(mFromPassword))
                {
                    return mFromPassword;
                }
                else
                {
                    return ConfigurationManager.AppSettings["ElectronicMailFromPassword"];
                }
            }
            set
            {
                mFromPassword = value;
            }
        }
        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(mDisplayName))
                {
                    return mDisplayName;
                }
                else
                {
                    return ConfigurationManager.AppSettings["ElectronicMailDisplayName"];
                }
            }
            set
            {
                mDisplayName = value;
            }
        }
        public string CcEmail
        {
            get
            {
                if (!string.IsNullOrEmpty(mCcEmail))
                {
                    return mCcEmail;
                }
                else
                {
                    return ConfigurationManager.AppSettings["ElectronicMailCcEmail"];
                }
            }
            set
            {
                mCcEmail = value;
            }
        }
        public string BccEmail
        {
            get
            {
                if (!string.IsNullOrEmpty(mBccEmail))
                {
                    return mBccEmail;
                }
                else
                {
                    return ConfigurationManager.AppSettings["ElectronicMailBccEmail"];
                }
            }
            set
            {
                mBccEmail = value;
            }
        }
        public string Subject
        {
            get
            {
                if (!string.IsNullOrEmpty(mSubject))
                {
                    return mSubject;
                }
                else
                {
                    return ConfigurationManager.AppSettings["ElectronicMailSubject"];
                }
            }
            set
            {
                mSubject = value;
            }
        }
        public string Message
        {
            get
            {
                if (!string.IsNullOrEmpty(mMessage))
                {
                    return mMessage;
                }
                else
                {
                    return ConfigurationManager.AppSettings["ElectronicMailMessage"];
                }
            }
            set
            {
                mMessage = value;
            }
        }
        public string SMTP
        {
            get
            {
                if (!string.IsNullOrEmpty(mSMTP))
                {
                    return mSMTP;
                }
                else
                {
                    return ConfigurationManager.AppSettings["ElectronicMailSMTP"];
                }
            }
            set
            {
                mSMTP = value;
            }
        }
        public int Port
        {
            get
            {
                if (mPort != 0)
                {
                    return mPort;
                }
                else
                {
                    return int.Parse(ConfigurationManager.AppSettings["ElectronicMailPort"]);
                }
            }
            set
            {
                mPort = value;
            }
        }
        public bool SSL
        {
            get
            {
                if (mSSL.Equals(""))
                {
                    return mSSL;
                }
                else
                {
                    return bool.Parse(ConfigurationManager.AppSettings["ElectronicMailSSL"]);
                }
            }
            set
            {
                mSSL = value;
            }
        }
        public bool HTML
        {
            get
            {
                if (mHTML.Equals(""))
                {
                    return mHTML;
                }
                else
                {
                    return bool.Parse(ConfigurationManager.AppSettings["ElectronicMailHTML"]);
                }
            }
            set
            {
                mHTML = value;
            }
        }
    }
}