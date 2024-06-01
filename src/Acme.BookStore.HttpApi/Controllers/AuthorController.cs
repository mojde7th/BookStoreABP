using Acme.BookStore.application.contracts.Services;
using Acme.BookStore.DTOs;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.BookStore.HttpApi.Controllers
{
    [Route("api/authors")]
    public class AuthorController : AbpController
    {
        private readonly IAuthorAppService _authorAppService;
        public AuthorController(IAuthorAppService authorAppService)
        {
            _authorAppService = authorAppService;
        }
        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetauthorAsync()
        {
            var authors = await _authorAppService.GetListAsync(
                new Volo.Abp.Application.Dtos.PagedAndSortedResultRequestDto());
            return Ok(authors.Items);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorDto(int id)
        {
            var author=await _authorAppService.GetAsync(id);
            return Ok(author);
        }
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> createAuthorDto
            ([FromBody] CreateUpdateAuthorDto input)
        {
            var author=await _authorAppService.CreateAsync(input);
            return CreatedAtAction(nameof(GetAuthorDto),
                new { id = author.Id, author } );
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] CreateUpdateAuthorDto input)
        {
            var author=await _authorAppService.UpdateAsync(id, input);
            return Ok(author);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorAppService.DeleteAsync(id);
            return NoContent();
        }
    }
}
