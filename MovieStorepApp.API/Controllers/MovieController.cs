using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStorepApp.API.Application.MovieOperations.CreateMovie;
using MovieStorepApp.API.Application.MovieOperations.DeleteMovie;
using MovieStorepApp.API.Application.MovieOperations.GetMovieDetail;
using MovieStorepApp.API.Application.MovieOperations.GetMovies;
using MovieStorepApp.API.Application.MovieOperations.UpdateMovie;
using MovieStorepApp.API.DbOperations;

namespace MovieStorepApp.API.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public MovieController(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            GetMovieQuery query = new(_context, _mapper);
            var result = await query.Handle();

            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MovieDetailQuery query = new(_context, _mapper);
            query.MovieId = id;
            MovieDetailQueryValidator validator = new MovieDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result = await query.Handle();
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMovieModel newMovie)
        {
            CreateMovieCommand command = new(_context, _mapper);
            command.Model = newMovie;
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateMovieModel updateMovie)
        {
            UpdateMovieCommand update = new(_context);
            update.Model = updateMovie;
            update.MovieId = id;
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            validator.ValidateAndThrow(update);
            await update.Handle();

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteMovieCommand command = new(_context);
            command.MovieId = id;
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            validator.ValidateAndThrow(command);
            await command.Handle();
            return Ok();
        }
    }
}
