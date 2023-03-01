using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStorepApp.API.Entities
{
    public class Director
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
