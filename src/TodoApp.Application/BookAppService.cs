using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TodoApp.Dtos;
using TodoApp.Domain;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using TodoApp.Application.Contracts;

namespace TodoApp.Application
{
    public class BookAppService : CrudAppService<Book, BookDto, Guid, PagedAndSortedResultRequestDto, CreateBookDto, UpdateBookDto>, 
        IBookAppService
    {
        private readonly IMapper _mapper;

        public BookAppService(IRepository<Book, Guid> repository, IMapper mapper)
            : base(repository)
        {
            _mapper = mapper;
        }

        protected override IQueryable<Book> ApplySorting(IQueryable<Book> query, PagedAndSortedResultRequestDto input)
        {
            if (string.IsNullOrEmpty(input.Sorting) || input.Sorting == "Name")
            {
                return query.OrderBy(b => b.Name);
            }
            // Add additional sorting options as needed
            return query.OrderBy(b => b.Name); // Default sorting
        }

        public override async Task<BookDto> CreateAsync(CreateBookDto input)
        {
            try
            {
                Console.WriteLine("Input received: " + input.ToString());

                Console.WriteLine("Mapping CreateBookDto to Book...");
                var book = _mapper.Map<Book>(input);
                Console.WriteLine("Mapped successfully: " + book.ToString());

                var createdBook = await Repository.InsertAsync(book);
                await CurrentUnitOfWork.SaveChangesAsync();

                Console.WriteLine("Mapping Book to BookDto...");
                var bookDto = _mapper.Map<BookDto>(createdBook);
                Console.WriteLine("Mapped successfully: " + bookDto.ToString());

                return bookDto;
            }
            catch (Exception ex)
            {
                // Log detailed exception information
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public override async Task<BookDto> UpdateAsync(Guid id, UpdateBookDto input)
        {
            try
            {
                var book = await Repository.GetAsync(id);

                Console.WriteLine("Mapping UpdateBookDto to Book...");
                _mapper.Map(input, book);
                Console.WriteLine("Mapped successfully.");

                await Repository.UpdateAsync(book);
                await CurrentUnitOfWork.SaveChangesAsync();

                Console.WriteLine("Mapping Book to BookDto...");
                var bookDto = _mapper.Map<BookDto>(book);
                Console.WriteLine("Mapped successfully: " + bookDto.ToString());

                return bookDto;
            }
            catch (Exception ex)
            {
                // Log detailed exception information
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public override async Task DeleteAsync(Guid id)
        {
            try
            {
                await Repository.DeleteAsync(id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log detailed exception information
                Console.WriteLine($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
