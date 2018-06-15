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
            if (typeof(T) == typeof(BO.Project))
            {
                repo = new ProjectRepository(context);
            }
            if (typeof(T) == typeof(BO.Item))
            {
                repo = new  ItemRepository(context);
            }
            if (typeof(T) == typeof(BO.ProjectTeam))
            {
                repo = new ProjectTeamRepository(context);
            }
            if (typeof(T)==typeof(BO.ProjectReport))
            {
                repo = new  ProjectReportRepository(context);
            }
            return repo;
        }
    }
}
