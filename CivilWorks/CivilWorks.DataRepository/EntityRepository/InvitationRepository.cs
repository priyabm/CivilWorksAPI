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
using System.Data.Entity;

namespace CivilWorks.DataRepository.EntityRepository
{
    internal class InvitationRepository : BaseEntityRepo
    {
        private DbSet<BO.UserPasswordActivation> _dbInvitation;

        public InvitationRepository(EO.CivilWorksEntities context) : base(context)
        {

        }
        #region Validate User
        public override object ValidateInvitation<T>(T data)
        {
            BO.UserPasswordActivation invitationBO = (BO.UserPasswordActivation)(object)data;
            EO.UserPasswordActivation invitation = _context.UserPasswordActivations.Where(p => p.PasswordActivattionKey == invitationBO.PasswordActivattionKey).FirstOrDefault<EO.UserPasswordActivation>();
            //p.IsExpired != true &&

            if (invitation != null)
            {

                invitation.IsExpired = true;
                 _context.Entry(invitation).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
            }

            else
            {
                return new BO.ErrorObject { ErrorMessage = "Invalid appkey or other parameters.", errorObject = "", ErrorLevel = BO.ErrorLevel.Error };
            }
            return (object)(invitation);
        }
        #endregion
    }
}
