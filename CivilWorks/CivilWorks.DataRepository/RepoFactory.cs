using BO = CivilWorks.BusinessObjects;
using CivilWorks.DataRepository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = CivilWorks.DataRepository.Model;

namespace CivilWorks.DataRepository
{
    internal class RepoFactory
    {
        internal static BaseEntityRepo GetRepo<T>(EO.CivilWorksEntities context)
        {
            BaseEntityRepo repo = null;

            if (typeof(T) == typeof(BO.User))
            {
                repo = new UserRepository(context);
            }
            if (typeof(T) == typeof(BO.UserPasswordActivation))
            {
                repo = new InvitationRepository(context);
            }
            return repo;
        }
    }
}
