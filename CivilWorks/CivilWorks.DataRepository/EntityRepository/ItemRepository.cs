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
    internal class  ItemRepository:BaseEntityRepo
    {
        public ItemRepository(EO.CivilWorksEntities context) : base(context)
        {

        }

        public override object Get<T>()
        {
            return _context.Items.Select(p => new BO.Item() { ID = p.ID, ItemName = p.ItemName, Price = p.Price }).ToList();
        }
    }
}
