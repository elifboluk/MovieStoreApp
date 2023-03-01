using MovieStorepApp.API.DbOperations;

namespace MovieStorepApp.API.Application.ActorOperations.UpdateActor
{
    public class UpdateActorCommand
    {
        public UpdateActorModel Model { get; set; }
        public int ActorId { get; set; }
        private readonly MovieStoreDbContext _context;
        public UpdateActorCommand(MovieStoreDbContext context)
        {
            _context = context;
        }
        public async Task Handle()
        {
            var actor = _context.Actors.FirstOrDefault(c => c.Id == ActorId);
            if (actor == null)
                throw new InvalidOperationException("Aktör bulunamadı");

            actor.Name = Model.Name != default ? Model.Name : actor.Name;
            actor.Surname = Model.Surname != default ? Model.Surname : actor.Surname;
            await _context.SaveChangesAsync();
        }
    }
    public class UpdateActorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
