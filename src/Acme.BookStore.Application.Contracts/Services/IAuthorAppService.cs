using Acme.BookStore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.application.contracts.Services
{
    public interface IAuthorAppService : ICrudAppService<
         AuthorDto,
         int,
         PagedAndSortedResultRequestDto,
         CreateUpdateAuthorDto,
         CreateUpdateAuthorDto>
    {
    }

}
