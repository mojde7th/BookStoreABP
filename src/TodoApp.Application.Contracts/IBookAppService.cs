using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using TodoApp.Dtos;
using System.Data;

namespace TodoApp.Application.Contracts
{
    public interface IBookAppService : ICrudAppService<
        BookDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateBookDto,
        UpdateBookDto>
    {
        
    }
}
