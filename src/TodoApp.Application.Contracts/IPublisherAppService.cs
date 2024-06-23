using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Dtos;
using Volo.Abp.Application.Services;

namespace TodoApp.Application.Contracts
{
    public interface IPublisherAppService : IApplicationService
    {
 
        Task<List<PublisherDto>> GetPublishersWithBooksAsync();
        Task<PublisherDto> CreateAsync(CreatePublisherDto input);
        Task<PublisherDto> UpdateAsync(Guid id, UpdatePublisherDto input);
        Task DeleteAsync(Guid id);
        Task<PublisherDto> GetAsync(Guid id);
    }
}
