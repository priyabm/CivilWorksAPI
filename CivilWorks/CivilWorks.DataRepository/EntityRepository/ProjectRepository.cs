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
   internal class ProjectRepository: BaseEntityRepo
    {
        public ProjectRepository(EO.CivilWorksEntities context) : base(context)
        {

        }

        public override object Get<T>()
        {
            return _context.Projects.Select(p => new BO.Project() { ID = p.ID, ProjectName = p.ProjectName, Description = p.Description}).ToList();
        }
    }
}
