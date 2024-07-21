using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApp.Application.Contracts;
using TodoApp.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace TodoApp.HttpApi
{
    [Route("api/books")]
    public class BookController : AbpController
    {
        private readonly IBookAppService _bookAppService;
        private readonly ILogger<BookController> _logger;
        public BookController(IBookAppService bookAppService, ILogger<BookController> logger)
        {
            _bookAppService = bookAppService;
            _logger = logger;
        }

        [HttpGet]
        public Task<PagedResultDto<BookDto>> GetListAsync([FromQuery] PagedAndSortedResultRequestDto input)
        {
            _logger.LogInformation("Processing request at {Time}", DateTime.UtcNow);
            return _bookAppService.GetListAsync(input);
        }

        [HttpGet("{id}")]
        public Task<BookDto> GetAsync(Guid id)
        {
            return _bookAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateAsync([FromBody] CreateBookDto input)
        {
            try
            {
                var bookDto = await _bookAppService.CreateAsync(input);
                return Ok(bookDto);
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
                return StatusCode(500, "An internal error occurred during your request!");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDto>> UpdateAsync(Guid id, [FromBody] UpdateBookDto input)
        {
            try
            {
                var bookDto = await _bookAppService.UpdateAsync(id, input);
                return Ok(bookDto);
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
                return StatusCode(500, "An internal error occurred during your request!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _bookAppService.DeleteAsync(id);
                return NoContent();
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
                return StatusCode(500, "An internal error occurred during your request!");
            }
        }
    }
}
