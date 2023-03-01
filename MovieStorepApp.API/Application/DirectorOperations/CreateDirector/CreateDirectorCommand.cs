using AutoMapper;
using MovieStorepApp.API.DbOperations;
using MovieStorepApp.API.Entities;

namespace MovieStorepApp.API.Application.DirectorOperations.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorModel Model { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorCommand(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle()
        {
            var director = _context.Directors.FirstOrDefault(c => c.Name == Model.Name && c.Surname == Model.Surname);
            if (director is not null)
                throw new InvalidOperationException("Eklemek istediğiniz yönetmen mevcut");

            director = _mapper.Map<Director>(Model);
            _context.Add(director);
            await _context.SaveChangesAsync();
        }
    }
    public class CreateDirectorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
