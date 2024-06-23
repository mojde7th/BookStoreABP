using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Contracts;
using TodoApp.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace TodoApp.HttpApi
{
    [Route("api/publishers")]
    public class PublisherController : AbpController
    {
        private readonly IPublisherAppService _publisherAppService;

        public PublisherController(IPublisherAppService publisherAppService)
        {
            _publisherAppService = publisherAppService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PublisherDto>>> GetPublishersWithBooksAsync()
        {
            var publishers = await _publisherAppService.GetPublishersWithBooksAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDto>> GetAsync(Guid id)
        {
            try
            {
                var publisher = await _publisherAppService.GetAsync(id);
                return Ok(publisher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = new
                    {
                        message = ex.Message,
                        innerException = ex.InnerException?.Message,
                        stackTrace = ex.StackTrace
                    }
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult<PublisherDto>> CreateAsync([FromBody] CreatePublisherDto input)
        {
            var publisher = await _publisherAppService.CreateAsync(input);
            return Ok(publisher);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PublisherDto>> UpdateAsync(Guid id, [FromBody] UpdatePublisherDto input)
        {
            var publisher = await _publisherAppService.UpdateAsync(id, input);
            return Ok(publisher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _publisherAppService.DeleteAsync(id);
            return NoContent();
        }
    }
}
