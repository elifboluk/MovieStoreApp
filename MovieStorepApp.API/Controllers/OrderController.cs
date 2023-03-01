using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStorepApp.API.Application.OrderOperations.CreateOrder;
using MovieStorepApp.API.Application.OrderOperations.GetOrders;
using MovieStorepApp.API.DbOperations;

namespace MovieStorepApp.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(MovieStoreDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetOrderQuery query = new GetOrderQuery(_context, _mapper);
            var result = await query.Handle();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderModel newOrder)
        {
            CreateOrderCommand command = new CreateOrderCommand(_context, _mapper);
            command.Model = newOrder;
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }

    }
}
