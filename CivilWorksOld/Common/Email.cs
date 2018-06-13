﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace CivilWorks.Common
{
    public class Email
    {
        private string _subject;
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }

        private string _body;
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        private string _toemail;
        public string ToEmail
        {
            get
            {
                return _toemail;
            }
            set
            {
                _toemail = value;
            }
        }

        private string _fromemail;
        public string FromEmail
        {
            get
            {
                return _fromemail;
            }
            set
            {
                _fromemail = value;
            }
        }

        private string _ccemail;
        public string CCEmail
        {
            get
            {
                return _ccemail;
            }
            set
            {
                _ccemail = value;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void SendMail()
        {
            var client = new SmtpClient("smtp.zoho.com", 587)
            {
                Credentials = new NetworkCredential("support@codearray.tech", "supp0rt@2017"),
                EnableSsl = true,
            };

            var mail = new System.Net.Mail.MailMessage("support@codearray.tech", ToEmail);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;

            Task.Factory.StartNew(() => client.Send(mail));
        }
    }
}