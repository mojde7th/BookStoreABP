using System;
using Volo.Abp.Domain.Entities;

namespace TodoApp.Domain
{
    public class Book : Entity<Guid>
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
        public Guid PublisherId { get; set; }  // Foreign key to Publisher
        public Publisher Publisher { get; set; }  // Navigation property
    }
}
