using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CivilWorks.Models;
using CivilWorks.Common;
using CivilWorks.Helper;
using CivilWorks.BOModel;
using System.Data.Entity;

namespace CivilWorks.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        UserPasswordActivation userActivation = new UserPasswordActivation();

        CivilWorksEntities2 _context = null;

        [HttpGet]
        [Route("GetUser")]
        public HttpResponseMessage GetUser(string userName)
        {

            try
            {
                _context = new CivilWorksEntities2();
                var user = _context.User1.Where(u => u.UserName == userName).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpGet]
        [Route("GetAllUsers")]
        public HttpResponseMessage GetAllUsers()
        {
            try
            {
                _context = new CivilWorksEntities2();
                var users = _context.User1.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        //Added by Priya
        [HttpPost]
        [Route("SaveUser")]
        public HttpResponseMessage SaveUser([FromBody] Models.User1 user)
        {
            try
            {

                CivilWorksEntities2 context = new CivilWorksEntities2();
                context.User1.Add(user);
                context.SaveChanges();
                User1 user_ = context.User1.Where(p => p.UserName == user.UserName).FirstOrDefault<User1>();

                #region Insert Invitation    
                CivilWorksEntities2 context1 = new CivilWorksEntities2();
                userActivation.PasswordActivattionKey = Guid.NewGuid();
                userActivation.DateCreated = user.CreatedDate;
                userActivation.UserID = user_.ID;
                userActivation.IsExpired = false;
                userActivation.ExpiryDate = System.DateTime.Now.AddDays(1);
                context1.UserPasswordActivations.Add(userActivation);
                context1.SaveChanges();
                #endregion

                #region mail notification
                try
                {

                    string VerificationLink = "<a href='" + Utility.GetConfigValue("VerificationLink") + "/" + userActivation.PasswordActivattionKey + "' target='_blank'>" + Utility.GetConfigValue("VerificationLink") + "/" + userActivation.PasswordActivattionKey + "</a>";

                    string MailMessageForCompany = "Dear " + user_.FirstName + ",<br><br>You have been registered in Our Portal. <br><br> Please confirm your account by clicking below link in order to use.<br><br><b>" + VerificationLink + "</b><br><br>Thanks";
                    //  string NotificationForCompany = "You have been registered in midas portal as a Medical Provider. ";
                    //  string SmsMessageForCompany = "Dear " + user.FirstName + ",<br><br>You have been registered in midas portal as a Medical provider. <br><br> Please confirm your account by clicking below link in order to use.<br><br><b>" + VerificationLink + "</b><br><br>Thanks";

                    // NotificationHelper nh = new NotificationHelper();
                    MessagingHelper mh = new MessagingHelper();

                    #region  company mail object                 
                    EmailMessage emCompany = new EmailMessage();
                    emCompany.ApplicationName = "Civil Works";
                    emCompany.ToEmail = user_.UserName;
                    emCompany.EMailSubject = "Civil Works Notification";
                    emCompany.EMailBody = MailMessageForCompany;
                    #endregion

                    //  mh.SendEmailAndSms(user_.UserName, 1,emCompany);

                    mh.SendMail(user_.UserName, emCompany.EMailSubject, MailMessageForCompany);


                }
                catch (Exception ex)
                {
                }
                #endregion

                return Request.CreateResponse(HttpStatusCode.OK, "Successfully Saved");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("ValidateInvitation")]
        public HttpResponseMessage ValidateInvitation([FromBody] Models.UserPasswordActivation data)
        {
            UserPasswordActivation invitation = null;
            try
            {
                if (data != null)
                {
                    _context = new CivilWorksEntities2();
                    invitation = _context.UserPasswordActivations.Where(p => p.IsExpired != true && p.PasswordActivattionKey == data.PasswordActivattionKey).FirstOrDefault<UserPasswordActivation>();
                }

                if (invitation != null)
                {

                    invitation.IsExpired = true;
                    // _context.Entry(invitation).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    //return new BO.ErrorObject { ErrorMessage = "Invalid appkey or other parameters.", errorObject = "", ErrorLevel = ErrorLevel.Error };
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid appkey or other parameters");
                }
                return Request.CreateResponse(HttpStatusCode.OK, invitation);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpPost]
        [Route("ResetPassword")]
        public HttpResponseMessage ResetPassword([FromBody] Models.User1 user)
        {
            _context = new CivilWorksEntities2();
            User1 userDb = new User1();
            try
            {
                userDb = user.ID > 0 ? _context.User1.Where(p => p.ID == user.ID).FirstOrDefault<User1>() : null;

                if (user != null)
                {
                    userDb.Password = PasswordHash.HashPassword(user.Password);
                    _context.SaveChanges();
                }

                userDb = _context.User1.Where(p => p.ID == user.ID && p.IsDeleted.Value == false).FirstOrDefault<User1>();
                return Request.CreateResponse(HttpStatusCode.OK, userDb);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpPost]
        [Route("Signin")]
        public HttpResponseMessage Signin([FromBody]User1 user)
        {
            _context = new CivilWorksEntities2();

            string Pass = user.Password;
            //Invitation invitation = _context.Invitations.Include("Company")
            //                                          .Include("User.UserCompanies")
            //                                          .Where(p => p.UniqueID == invitationBO.UniqueID).FirstOrDefault<Invitation>();




            User1 data_ = _context.User1
                            .Where(x => x.UserName == user.UserName && x.IsDeleted.Value == false).FirstOrDefault<User1>();

            if (data_ == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found for this user");
                //  return new BO.ErrorObject { ErrorMessage = "No record found for this user.", errorObject = "", ErrorLevel = ErrorLevel.Error };
            }
            bool isPasswordCorrect = false;
            try
            {
                isPasswordCorrect = PasswordHash.ValidatePassword(user.Password, ((User1)data_).Password);

                if (!isPasswordCorrect)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid credentials");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, data_);

                // return new BO.ErrorObject { ErrorMessage = "Invalid credentials.Please check details..", errorObject = "", ErrorLevel = ErrorLevel.Error };
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid credentials");
                //return new BO.ErrorObject { ErrorMessage = "Invalid credentials.Please check details..", errorObject = "", ErrorLevel = ErrorLevel.Error };
            }



        }


        [HttpPost]
        [Route("GeneratePasswordResetLink")]
        public HttpResponseMessage GeneratePasswordResetLink([FromBody]User1 passwordToken)
        {
            _context = new CivilWorksEntities2();
            UserPasswordActivation passwordReset = null;
            try
            {
                User1 data_ = _context.User1.Where(x => x.UserName == passwordToken.UserName).FirstOrDefault<User1>();
                if (data_ == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found for this user");
                    // return new BO.ErrorObject { ErrorMessage = "No record found for this user.", errorObject = "", ErrorLevel = ErrorLevel.Error };
                }
                #region Insert link    
                CivilWorksEntities2 context1 = new CivilWorksEntities2();
                userActivation.PasswordActivattionKey = Guid.NewGuid();
                userActivation.DateCreated = System.DateTime.Now;
                userActivation.UserID = data_.ID;
                userActivation.IsExpired = false;
                userActivation.ExpiryDate = System.DateTime.Now.AddDays(1);
                context1.UserPasswordActivations.Add(userActivation);
                context1.SaveChanges();
                #endregion


                string Message = "Dear " + data_.FirstName + ",<br><br>You are receiving this email because you (or someone pretending to be you) requested that your password be reset on the " + Utility.GetConfigValue("Website") + " site.  If you do not wish to reset your password, please ignore this message.<br><br>To reset your password, please click the following link, or copy and paste it into your web browser:<br><br>" + Utility.GetConfigValue("ForgotPasswordLink") + "/" + userActivation.PasswordActivattionKey + " <br><br>Your username, in case you've forgotten: " + data_.UserName + "<br><br>Thanks";
                #region  company mail object                 
                EmailMessage emCompany = new EmailMessage();
                emCompany.ApplicationName = "Civil Works";
                emCompany.ToEmail = passwordToken.UserName;
                emCompany.EMailSubject = "Civil Works Reset Password Link";
                emCompany.EMailBody = Message;
                #endregion

                MessagingHelper mh = new MessagingHelper();
                mh.SendMail(passwordToken.UserName, emCompany.EMailSubject, Message);


                User1 userDb = _context.User1.Where(p => p.UserName == passwordToken.UserName && p.IsDeleted.Value == false).FirstOrDefault<User1>();
                return Request.CreateResponse(HttpStatusCode.OK, userDb);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        [HttpPost]
        [Route("ValidatePassword")]
        public HttpResponseMessage ValidatePassword([FromBody]UserPasswordActivation passwordToken)
        {
            UserPasswordActivation invitation = null;
            try
            {
                if (passwordToken != null)
                {
                    _context = new CivilWorksEntities2();

                    invitation = _context.UserPasswordActivations.Where(p => p.IsExpired != true && p.PasswordActivattionKey == passwordToken.PasswordActivattionKey).FirstOrDefault<UserPasswordActivation>();
                }

                if (invitation != null)
                {

                    invitation.IsExpired = true;
                    // _context.Entry(invitation).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    //return new BO.ErrorObject { ErrorMessage = "Invalid appkey or other parameters.", errorObject = "", ErrorLevel = ErrorLevel.Error };
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid password link");
                }
                return Request.CreateResponse(HttpStatusCode.OK, invitation);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
