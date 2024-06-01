using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.application.contracts.Services;
using Acme.BookStore.DTOs;
using Acme.BookStore.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Application.Services
{
    public class AuthorAppService : CrudAppService<Author, AuthorDto, int,
        PagedAndSortedResultRequestDto,
        CreateUpdateAuthorDto, CreateUpdateAuthorDto>, IAuthorAppService
    {
        public AuthorAppService(IRepository<Author, int> repository) 
            : base(repository) { }
    }
}
