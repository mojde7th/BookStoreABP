using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace TodoApp.Domain
{
    public class Publisher : Entity<Guid>
    {
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
