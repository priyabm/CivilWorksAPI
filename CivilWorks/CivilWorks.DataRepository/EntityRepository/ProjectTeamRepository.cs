using System.Text;
using System.Threading.Tasks;
using EO = CivilWorks.DataRepository.Model;
using BO = CivilWorks.BusinessObjects;
using CivilWorks.Common;
using CivilWorks.DataRepository.EntityRepository.Helper;
using CivilWorks.BusinessObjects.MesssagingService;
using System.Data.Entity;
using System.Linq;

namespace CivilWorks.DataRepository.EntityRepository
{
    internal class ProjectTeamRepository : BaseEntityRepo
    {


        public ProjectTeamRepository(EO.CivilWorksEntities context) : base(context)
        {

        }

        public override object Get<T>()
        {
            return _context.ProjectTeams.Select(p => new BO.ProjectTeam() { ID = p.ID, InspectorName = p.InspectorName}).ToList();
        }
    }
}
