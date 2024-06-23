//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using TodoApp.Domain;
//using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
//using Volo.Abp.EntityFrameworkCore;

//namespace TodoApp.EntityFrameworkCore
//{
//    public class BookRepository : EfCoreRepository<TodoAppDbContext, Book, Guid>, IBookRepository
//    {
//        public BookRepository(IDbContextProvider<TodoAppDbContext> dbContextProvider)
//            : base(dbContextProvider)
//        {
//        }

//        public async Task<List<Book>> GetListAsync()
//        {
//            return await DbContext.Set<Book>().ToListAsync();
//        }

//        public async Task<Book> GetAsync(Guid id)
//        {
//            return await DbContext.Set<Book>().FindAsync(id);
//        }

//        public async Task InsertAsync(Book book)
//        {
//            await DbContext.Set<Book>().AddAsync(book);
//            await DbContext.SaveChangesAsync();
//        }

//        public async Task UpdateAsync(Book book)
//        {
//            DbContext.Set<Book>().Update(book);
//            await DbContext.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(Guid id)
//        {
//            var book = await GetAsync(id);
//            if (book != null)
//            {
//                DbContext.Set<Book>().Remove(book);
//                await DbContext.SaveChangesAsync();
//            }
//        }
//    }
//}
