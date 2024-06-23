using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Dtos
{
    public class CreateBookDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public Guid PublisherId { get; set; }
    }
}
