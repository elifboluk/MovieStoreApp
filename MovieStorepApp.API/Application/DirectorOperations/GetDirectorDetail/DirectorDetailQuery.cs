using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStorepApp.API.DbOperations;

namespace MovieStorepApp.API.Application.DirectorOperations.GetDirectorDetail
{
    public class DirectorDetailQuery
    {
        public int DirectorId { get; set; }
        public DirectorViewModel Model { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DirectorDetailQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DirectorViewModel> Handle()
        {
            var director = _context.Directors.Include(c => c.Movies).FirstOrDefault(c => c.Id == DirectorId);
            if (director == null)
                throw new InvalidOperationException("Yönetmen bulunamadı.");

            Model = _mapper.Map<DirectorViewModel>(director);
            return Model;
        }
    }

    public class DirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}
