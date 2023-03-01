using AutoMapper;
using MovieStorepApp.API.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace MovieStorepApp.API.Application.DirectorOperations.GetDirectors
{
    public class GetDirectorQuery
    {
        public DirectorsViewModel Model { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DirectorsViewModel>> Handle()
        {
            var directorList = await _context.Directors.Include(c => c.Movies).OrderBy(c => c.Id).ToListAsync();
            List<DirectorsViewModel> vm = _mapper.Map<List<DirectorsViewModel>>(directorList);

            return vm;

        }
    }

    public class DirectorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}
