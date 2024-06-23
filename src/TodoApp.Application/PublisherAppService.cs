using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TodoApp.Dtos;
using TodoApp.Domain;
using Volo.Abp.Application.Services;
using TodoApp.Application.Contracts;
using Volo.Abp.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace TodoApp.Application
{
    public class PublisherAppService : ApplicationService, IPublisherAppService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PublisherAppService> _logger;
        public PublisherAppService(ILogger<PublisherAppService> logger,IPublisherRepository publisherRepository, IRepository<Book, Guid> bookRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }


        
        public async Task<List<PublisherDto>> GetPublishersWithBooksAsync()
        {
            var publishers = await _publisherRepository.GetPublishersWithBooksAsync();
            return _mapper.Map<List<Publisher>, List<PublisherDto>>(publishers);
        }
        public async Task<PublisherDto> GetAsync(Guid id)
        {
            try
            {
                var publisher = await _publisherRepository.GetAsync(id);
                return _mapper.Map<Publisher, PublisherDto>(publisher);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                }
                _logger.LogError($"Stack Trace: {ex.StackTrace}");
                throw new ApplicationException("An internal error occurred while retrieving the publisher.", ex);
            }
        }
        public async Task<PublisherDto> CreateAsync(CreatePublisherDto input)
        {
            var publisher = _mapper.Map<Publisher>(input);

            // Assign books
            foreach (var bookId in input.BookIds)
            {
                var book = await _bookRepository.GetAsync(bookId);
                publisher.Books.Add(book);
            }

            await _publisherRepository.InsertAsync(publisher);
            await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<Publisher, PublisherDto>(publisher);
        }

        public async Task<PublisherDto> UpdateAsync(Guid id, UpdatePublisherDto input)
        {
            var publisher = await _publisherRepository.GetAsync(id);
            _mapper.Map(input, publisher);

            // Update books
            publisher.Books.Clear();
            foreach (var bookId in input.BookIds)
            {
                var book = await _bookRepository.GetAsync(bookId);
                publisher.Books.Add(book);
            }

            await _publisherRepository.UpdateAsync(publisher);
            await CurrentUnitOfWork.SaveChangesAsync();

            return _mapper.Map<Publisher, PublisherDto>(publisher);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _publisherRepository.DeleteAsync(id);
        }
    }
}
