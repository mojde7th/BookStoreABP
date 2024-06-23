using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace TodoApp.Domain
{
    public interface IPublisherRepository : IRepository<Publisher, Guid>
    {
        Task<List<Publisher>> GetPublishersWithBooksAsync();
    }
}
