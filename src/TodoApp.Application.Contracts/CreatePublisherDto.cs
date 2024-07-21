using System;
using System.Collections.Generic;

namespace TodoApp.Dtos
{
    public class CreatePublisherDto
    {
        public string Name { get; set; }
        public List<Guid> BookIds { get; set; } = new List<Guid>();
    }
}
