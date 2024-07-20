using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Contracts;

namespace TodoApp
{
    [Route("api/customaccount")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountAppService 
            _accountAppService;
        public AccountController(IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login
            ([FromBody] LoginDto model)
        {
            var token = await _accountAppService.
                LoginAsync(model);
            return Ok(new {token});
        }

    }
}
