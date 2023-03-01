using MovieStorepApp.API.DbOperations;

namespace MovieStorepApp.API.Application.ActorOperations.DeleteActor
{
    public class DeleteActorCommand
    {
        public int ActorId { get; set; }
        private readonly MovieStoreDbContext _context;
        private IMovieStoreDbContext context;

        public DeleteActorCommand(MovieStoreDbContext context)
        {
            _context = context;
        }

        public async Task Handle()
        {
            var actor = _context.Actors.FirstOrDefault(c => c.Id == ActorId);
            if (actor == null)
                throw new InvalidOperationException("Aktör bulunamadı.");

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
        }
    }
}
