using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace TodoApp.Dtos
{
    public class PublisherDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public List<BookDto> Books { get; set; } = new List<BookDto>();
    }
}
