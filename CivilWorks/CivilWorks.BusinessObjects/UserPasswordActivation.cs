using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivilWorks.BusinessObjects
{
    public class UserPasswordActivation
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public System.Guid PasswordActivattionKey { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public System.DateTime DateCreated { get; set; }
        public bool IsExpired { get; set; }

    }
}
