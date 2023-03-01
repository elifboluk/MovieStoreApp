using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStorepApp.API.Application.DirectorOperations.CreateDirector;
using MovieStorepApp.API.Application.DirectorOperations.DeleteDirector;
using MovieStorepApp.API.Application.DirectorOperations.GetDirectorDetail;
using MovieStorepApp.API.Application.DirectorOperations.GetDirectors;
using MovieStorepApp.API.Application.DirectorOperations.UpdateDirectors;
using MovieStorepApp.API.DbOperations;

namespace MovieStorepApp.API.Controllers
{
    [Route("api/[controller]")]
    public class DirectorController : Controller
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DirectorController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetDirectorQuery query = new(_context, _mapper);
            var result = await query.Handle();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            DirectorDetailQuery query = new(_context, _mapper);
            query.DirectorId = id;
            DirectorDetailQueryValidator validator = new DirectorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDirectorModel newDirector)
        {
            CreateDirectorCommand command = new(_context, _mapper);
            command.Model = newDirector;
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDirectorModel updateDirector)
        {
            UpdateDirectorCommand command = new(_context);
            command.Model = updateDirector;
            command.DirectorId = id;
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteDirectorCommand command = new(_context);
            command.DirectorId = id;
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            await command.Handle();
            return Ok();
        }
    }
}
