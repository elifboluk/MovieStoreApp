using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStorepApp.API.DbOperations;
using MovieStorepApp.API.Entities;

namespace MovieStorepApp.API.Application.ActorOperations.GetActorDetail
{
    public class ActorDetailQuery
    {
        public ActorViewModel Model { get; set; }
        public int ActorId { get; set; }
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public ActorDetailQuery(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActorViewModel> Handle()
        {
            var actor = _context.Actors.Include(c => c.Movies).FirstOrDefault(c => c.Id == ActorId);
            if (actor == null)
                throw new InvalidOperationException("Aktör mevcut değil");

            Model = _mapper.Map<ActorViewModel>(actor);
            return Model;
        }
    }
    public class ActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<string> Movies { get; set; }
    }
}
