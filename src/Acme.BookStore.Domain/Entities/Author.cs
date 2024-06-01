using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Entities
{
    public class Author:FullAuditedEntity<int>
    {
        public string Name {  get; set; }
        public string Bio { get; set; }
    }
}
