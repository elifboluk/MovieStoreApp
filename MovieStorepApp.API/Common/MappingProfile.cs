using AutoMapper;
using MovieStorepApp.API.Application.ActorOperations.CreateActor;
using MovieStorepApp.API.Application.ActorOperations.GetActorDetail;
using MovieStorepApp.API.Application.ActorOperations.GetActors;
using MovieStorepApp.API.Application.CustomerOperations.CreateCustomer;
using MovieStorepApp.API.Application.DirectorOperations.CreateDirector;
using MovieStorepApp.API.Application.DirectorOperations.GetDirectorDetail;
using MovieStorepApp.API.Application.DirectorOperations.GetDirectors;
using MovieStorepApp.API.Application.MovieOperations.CreateMovie;
using MovieStorepApp.API.Application.MovieOperations.GetMovieDetail;
using MovieStorepApp.API.Application.MovieOperations.GetMovies;
using MovieStorepApp.API.Application.OrderOperations.CreateOrder;
using MovieStorepApp.API.Application.OrderOperations.GetOrders;
using MovieStorepApp.API.Entities;

namespace MovieStorepApp.API.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<int, Actor>().ForMember(c => c.Id, c => c.MapFrom(c => c));
            CreateMap<int, Movie>().ForMember(c => c.Id, c => c.MapFrom(c => c));
            CreateMap<CreateMovieModel, Movie>().ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors));
            CreateMap<CreateDirectorModel, Director>();
            CreateMap<CreateOrderModel, Order>();
            CreateMap<Order, OrderViewModel>().ForMember(c => c.CustomerName, c => c.MapFrom(c => c.Customer.Name + " " + c.Customer.Surname)).ForMember(c => c.PurchasedMovie, c => c.MapFrom(c => c.PurchasedMovie.Name));
            CreateMap<CreateActorModel, Actor>();
            CreateMap<CreateCustomerModel, Customer>();
            CreateMap<Movie, MovieViewModel>().ForMember(c => c.Genre, c => c.MapFrom(c => c.Genre.Name)).ForMember(c => c.Director, c => c.MapFrom(c => c.Director.Name + "" + c.Director.Surname)).ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors.Select(c => c.Name + " " + c.Surname).ToList()));
            CreateMap<Director, DirectorViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            CreateMap<Actor, ActorViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            CreateMap<Actor, ActorsViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            CreateMap<Director, DirectorsViewModel>().ForMember(c => c.Movies, c => c.MapFrom(c => c.Movies.Select(c => c.Name).ToList()));
            CreateMap<Movie, MoviesViewModel>().ForMember(c => c.Genre, c => c.MapFrom(c => c.Genre.Name)).ForMember(c => c.Director, c => c.MapFrom(c => c.Director.Name + "" + c.Director.Surname)).ForMember(c => c.Actors, c => c.MapFrom(c => c.Actors.Select(c => c.Name + " " + c.Surname).ToList()));

        }
    }
}
