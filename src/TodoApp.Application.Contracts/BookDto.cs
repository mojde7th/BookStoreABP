using System;

namespace TodoApp.Dtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public float Price { get; set; }
        public Guid PublisherId { get; set; } // Add PublisherId here
    }
}
