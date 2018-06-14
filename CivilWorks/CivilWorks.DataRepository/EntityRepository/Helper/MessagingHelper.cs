using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Web;
using CivilWorks.Common;


namespace CivilWorks.DataRepository.EntityRepository.Helper
{
    public class MessagingHelper
    {
        public static string NotificationServiceBaseURL { get; set; }
        public string AccessToken { get; set; }
        public string ApplicationName { get; set; }

        public MessagingHelper()
        {
            // ApplicationName = ConfigurationManager.AppSettings["MessagingApplicationName"];
            NotificationServiceBaseURL = ConfigurationManager.AppSettings["MessagingWebAPIBaseAddress"];
            AccessToken = HttpContext.Current.Request.Headers["Authorization"];
            if (AccessToken == null || AccessToken == "")
            {
                //  GetToken();
            }
        }
        private void GetToken()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Set Token endpoint base URL
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["AuthTokenEndpointUrl"]);

            //Get the access token response
            HttpResponseMessage tokenResponse = client.GetAsync(
                string.Format("GetToken?clientid={0}&clientsecret={1}&username={2}&password={3}",
                ConfigurationManager.AppSettings["MessagingServiceClientID"],
                ConfigurationManager.AppSettings["MessagingServiceClientSecret"],
                ConfigurationManager.AppSettings["MessagingServiceClientUser"],
                ConfigurationManager.AppSettings["MessagingServiceClientPassword"])
                ).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                //Read Access Token
               // var tokenresponse = tokenResponse.Content.ReadAsAsync<dynamic>().Result;

              //  AccessToken = "Bearer " + tokenresponse.AccessToken;
            }
        }
              
        public void SendMail(string recipient, string subject, string message)
        {
            string smtpHostName = Utility.GetConfigValue("SMTPHostName"),
                smtpUserName = Utility.GetConfigValue("SMTPUsername"),
                smtpPassword = Utility.GetConfigValue("SMTPPassword"),
                adminSupportEMail = Utility.GetConfigValue("SystemAdminSupportEmail");
            bool isSSL = Convert.ToBoolean(Utility.GetConfigValue("IsSSL"));

            int smtpPortNumber = Convert.ToInt32(Utility.GetConfigValue("SMTPPortNumber"));

            var client = new SmtpClient(smtpHostName, smtpPortNumber)
            {
                Credentials = new NetworkCredential(smtpUserName, smtpPassword),
                EnableSsl = isSSL,
            };

            var mail = new MailMessage(smtpUserName, recipient);
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            //   mail.ReplyTo = new MailAddress(adminSupportEMail);

            client.Send(mail);
        }

    }
}
