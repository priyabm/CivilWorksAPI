using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = CivilWorks.DataRepository.Model;
using BO = CivilWorks.BusinessObjects;
using CivilWorks.Common;
using CivilWorks.DataRepository.EntityRepository.Helper;
using CivilWorks.BusinessObjects.MesssagingService;

namespace CivilWorks.DataRepository.EntityRepository
{
    internal class UserRepository : BaseEntityRepo, IDisposable
    {
      
        public UserRepository(EO.CivilWorksEntities context) : base(context)
        {

        }

        public override object Get<T>()
        {           
             return _context.Users.Select(p => new BO.User() { ID = p.ID, FirstName = p.FirstName, LastName = p.LastName, MiddleName = p.MiddleName }).ToList();
        }

        #region Login
        public override Object Login<T>(T entity)
        {
            BO.User userBO = (BO.User)(object)entity;
            string Pass = userBO.Password;
            dynamic data_ = _context.Users.Where(x => x.UserName == userBO.UserName && x.IsDeleted.Value == false).FirstOrDefault();
            if (data_ == null)
            {
                return new BO.ErrorObject { ErrorMessage = "No record found for this user.", errorObject = "", ErrorLevel = BO.ErrorLevel.Error };
            }
            bool isPasswordCorrect = false;
            try
            {
                isPasswordCorrect = PasswordHash.ValidatePassword(userBO.Password, ((EO.User)data_).Password);

                if (!isPasswordCorrect)
                    return new BO.ErrorObject { ErrorMessage = "Invalid credentials.Please check details..", errorObject = "", ErrorLevel = BO.ErrorLevel.Error };
                else
                    return data_;
            }
            catch
            {
                return new BO.ErrorObject { ErrorMessage = "Invalid credentials.Please check details..", errorObject = "", ErrorLevel = BO.ErrorLevel.Error };
            }
            
        }


        #endregion

        #region Save Data

        public override Object Save<T>(T entity)
        {
            BO.User userBO = (BO.User)(object)entity;
            if (userBO == null) return new BO.ErrorObject { ErrorMessage = "User object can't be null", errorObject = "", ErrorLevel = BO.ErrorLevel.Error };

            EO.User userDB = new EO.User();
            EO.UserPasswordActivation activationDB = new EO.UserPasswordActivation();
            bool isEditMode = false;

            userDB.UserName = userBO.UserName;
            userDB.FirstName = userBO.FirstName;
            userDB.LastName = userBO.LastName;
            userDB.ID = userBO.ID;
            userDB.Password = userBO.Password;
            userDB.CreatedBy = userBO.CreatedBy;
            userDB.CreatedDate = userBO.CreatedDate;
            userDB.UpdatedBy = userBO.UpdatedBy;
            userDB.UpdatedDate = userBO.UpdatedDate;
            userDB.IsDeleted = userBO.IsDeleted;
            userDB.ImageUrl = userBO.ImageUrl;
            userDB.ContactNumber = userBO.ContactNumber;
            if (userDB.ID > 0)
            {
                //UPDATE 
            }
            else
            {
              //  if (_context.Users.Any(o => o.UserName == userBO.UserName && o.IsDeleted.Value == false))   return new BO.ErrorObject { ErrorMessage = "User already exists.", errorObject = "", ErrorLevel = BO.ErrorLevel.Information };
                userDB.CreatedDate = DateTime.UtcNow;
                _context.Users.Add(userDB);
            }
            _context.SaveChanges();
     
            #region Insert UserPasswordActivations    

            activationDB.PasswordActivattionKey = Guid.NewGuid();
            activationDB.DateCreated = userDB.CreatedDate;
            activationDB.UserID = userDB.ID;
            activationDB.IsExpired = false;
            activationDB.ExpiryDate = System.DateTime.Now.AddDays(1);
            _context.UserPasswordActivations.Add(activationDB);
            _context.SaveChanges();
            #endregion

            #region mail notification
            try
            {

                string VerificationLink = "<a href='" + Utility.GetConfigValue("VerificationLink") + "/" + activationDB.PasswordActivattionKey + "' target='_blank'>" + Utility.GetConfigValue("VerificationLink") + "/" + activationDB.PasswordActivattionKey + "</a>";

                string MailMessageForCompany = "Dear " + userDB.FirstName + ",<br><br>You have been registered in Our Portal. <br><br> Please confirm your account by clicking below link in order to use.<br><br><b>" + VerificationLink + "</b><br><br>Thanks";
                //  string NotificationForCompany = "You have been registered in midas portal as a Medical Provider. ";
                //  string SmsMessageForCompany = "Dear " + user.FirstName + ",<br><br>You have been registered in midas portal as a Medical provider. <br><br> Please confirm your account by clicking below link in order to use.<br><br><b>" + VerificationLink + "</b><br><br>Thanks";

                // NotificationHelper nh = new NotificationHelper();
                MessagingHelper mh = new MessagingHelper();

                #region  company mail object                 
                EmailMessage emCompany = new EmailMessage();
                emCompany.ApplicationName = "Civil Works";
                emCompany.ToEmail = userDB.UserName;
                emCompany.EMailSubject = "Civil Works Notification";
                emCompany.EMailBody = MailMessageForCompany;
                #endregion

                //  mh.SendEmailAndSms(user_.UserName, 1,emCompany);

                mh.SendMail(userDB.UserName, emCompany.EMailSubject, MailMessageForCompany);


            }
            catch (Exception ex)
            {
            }
            #endregion    
            return (object)userDB;
        }

        #endregion
     
        
        #region ResetPassword
        public override Object ResetPassword<T>(T entity)
        {
            BO.User addUserBO = (BO.User)(object)entity;
            
         
            if (addUserBO == null)
            {
                return new BO.ErrorObject { ErrorMessage = "User object can't be null", errorObject = "", ErrorLevel = BO.ErrorLevel.Error };
            }
            if (addUserBO.ID == 0)
            {
                return new BO.ErrorObject { ErrorMessage = "Invalid user id", errorObject = "", ErrorLevel = BO.ErrorLevel.Error };
            }

            EO.User userDB = new EO.User();           
            EO.UserPasswordActivation invitationDB = new EO.UserPasswordActivation();

            userDB = addUserBO.ID > 0 ? _context.Users.Where(p => p.ID == addUserBO.ID).FirstOrDefault<EO.User>() : null;

            if (addUserBO != null)
            {
                userDB.Password = PasswordHash.HashPassword(addUserBO.Password);
            }
            
            _context.SaveChanges();

            userDB = _context.Users.Where(p => p.ID == userDB.ID && p.IsDeleted.Value == false).FirstOrDefault<EO.User>();
            var res = userDB;
            return (object)res;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
