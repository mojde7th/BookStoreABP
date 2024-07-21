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
using Microsoft.Extensions.Logging;

namespace TodoApp.Application
{
    public class BookAppService : CrudAppService<Book, BookDto, Guid,
        PagedAndSortedResultRequestDto, CreateBookDto, UpdateBookDto>, 
        IBookAppService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<BookAppService> _logger;
        public BookAppService(IRepository<Book, Guid> repository,
            IMapper mapper, ILogger<BookAppService> logger)
            : base(repository)
        {
            _mapper = mapper;
            _logger = logger;
        }

        protected override IQueryable<Book> ApplySorting
            (IQueryable<Book> query, 
            PagedAndSortedResultRequestDto input)
        {
            if (string.IsNullOrEmpty(input.Sorting) || 
                input.Sorting == "Name")
            {
                return query.OrderBy(b => b.Name);
            }
            // Add additional sorting options as needed
            return query.OrderBy(b => b.Name); // Default sorting
        }

        public override async Task<BookDto> CreateAsync
            (CreateBookDto input)
        {
            try
            {
                _logger.LogInformation("Input received: {Input}", input);

                _logger.LogInformation
                    ("Mapping CreateBookDto to Book...");
                var book = _mapper.Map<Book>(input);
                _logger.LogInformation("Mapped successfully: {Book}",
                    book);

                var createdBook = await Repository.InsertAsync(book);
                await CurrentUnitOfWork.SaveChangesAsync();

                _logger.LogInformation("Mapping Book to BookDto...");
                var bookDto = _mapper.Map<BookDto>(createdBook);
                _logger.LogInformation("Mapped successfully: {BookDto}", bookDto);

                return bookDto;

              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating book");
                throw;
            }
        }

        public override async Task<BookDto> UpdateAsync
            (Guid id, UpdateBookDto input)
        {
            try
            {
                var book = await Repository.GetAsync(id);

                _logger.LogInformation("Mojde Mapping UpdateBookDto to Book...");
                _mapper.Map(input, book);
                _logger.LogInformation("Mojde Mapped successfully.");

                await Repository.UpdateAsync(book);
                await CurrentUnitOfWork.SaveChangesAsync();

                _logger.LogInformation("Mojde Mapping Book to BookDto...");
                var bookDto = _mapper.Map<BookDto>(book);
                _logger.LogInformation("Mojde Mapped successfully: {BookDto}",
                    bookDto);

                return bookDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating book");
                throw;
            }
        }

        public override async Task DeleteAsync(Guid id)
        {
            try
            {
                await Repository.DeleteAsync(id);
                //await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book");
                throw;
            }
        }
    }
}
