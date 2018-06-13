using CivilWorks.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Web;
using BO = CivilWorks.BOModel;

namespace CivilWorks.Helper
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
                var tokenresponse = tokenResponse.Content.ReadAsAsync<dynamic>().Result;

                AccessToken = "Bearer " + tokenresponse.AccessToken;
            }
        }

        public BO.Enums.PreferedModeOfComunication SendEmailAndSms(string userName, int companyId, BO.EmailMessage emailData)
        {
            try
            {

                BO.Enums.PreferedModeOfComunication predferredModewOfCommunication = BO.Enums.PreferedModeOfComunication.Email; // GetModeOfComunication(userName, companyId);

                if (predferredModewOfCommunication == BO.Enums.PreferedModeOfComunication.Email)
                {

                    AddMessageToEmailQueue(emailData);
                    return BO.Enums.PreferedModeOfComunication.Email;
                }
                //else if (predferredModewOfCommunication == BO.Enums.PreferedModeOfComunication.SMS)
                //{
                //    AddMessageToSMSQueue(smsData);
                //    return BO.Enums.PreferedModeOfComunication.SMS;
                //}
                //else
                //{
                //    AddMessageToEmailQueue(emailData);
                //    AddMessageToSMSQueue(smsData);
                //    return BO.Enums.PreferedModeOfComunication.Both;

                //}
            }
            catch (Exception e)
            {
                return BO.Enums.PreferedModeOfComunication.Both;
            }
            return BO.Enums.PreferedModeOfComunication.Email;
        }

        public string AddMessageToEmailQueue(BO.EmailMessage message)
        {
            try
            {
                //Set AccessToken to client header
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", AccessToken);
                client.BaseAddress = new Uri(NotificationServiceBaseURL);

                //HttpResponseMessage response = client.PostAsJsonAsync("EMail/AddMessageToQueue", message).Result;
                HttpResponseMessage response = client.PostAsJsonAsync("EMail/SendMessageInstantly", message).Result;

                response.EnsureSuccessStatusCode();
                var status = response.Content.ReadAsStringAsync().Result;


                //TEMP
                //HttpResponseMessage response2 = client.PostAsJsonAsync("EMail/SendMessageInstantly", message).Result;


                return status;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //public bool SendEMail(string recipient, string subject, string message)
        //{
        //    bool isMessageSent = false;
        //    //Intialise Parameters  
        //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        //    client.Port = 587;
        //    client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        //    client.UseDefaultCredentials = false;
        //    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("", "");
        //    client.EnableSsl = true;
        //    client.Credentials = credentials;
        //    try
        //    {
        //        var mail = new System.Net.Mail.MailMessage("", recipient.Trim());
        //        mail.Subject = subject;
        //        mail.Body = message;
        //        mail.IsBodyHtml = true;
        //        //System.Net.Mail.Attachment attachment;  
        //        //attachment = new Attachment(@"C:\Users\XXX\XXX\XXX.jpg");  
        //        //mail.Attachments.Add(attachment);  
        //        client.Send(mail);
        //        isMessageSent = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        isMessageSent = false;
        //    }
        //    return isMessageSent;
        //}

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