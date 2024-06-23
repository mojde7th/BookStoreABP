using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace TodoApp.Domain
{
    public class PublisherRepository : EfCoreRepository<TodoAppDbContext, Publisher, Guid>, IPublisherRepository
    {
        public PublisherRepository(IDbContextProvider<TodoAppDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Publisher>> GetPublishersWithBooksAsync()
        {
            return await DbContext.Publishers
                .Include(p => p.Books)
                .ToListAsync();
        }
    }
}
