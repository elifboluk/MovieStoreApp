using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStorepApp.API.Application.CustomerOperations.CreateCustomer;
using MovieStorepApp.API.DbOperations;
using MovieStorepApp.API.TokenOperations.Models;
using static MovieStorepApp.API.Application.CustomerOperations.CreateCustomer.CreateTokenCommand;

namespace MovieStorepApp.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CustomerController(IMapper mapper, MovieStoreDbContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            command.Model = newCustomer;
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok();
        }
        [HttpPost("connect/token")]
        public async Task<ActionResult<Token>> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model = login;
            var token = await command.Handle();
            return token;
        }
        [HttpGet("refreshToken")]
        public async Task<ActionResult<Token>> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token;
            var resultToken = await command.Handle();
            return resultToken;
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
