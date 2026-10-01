using System;
using System.Collections.Generic;
using System.Text;
using CinemaHubShared.Models;
using System.Threading.Tasks;

namespace CinemaHub.Services
{
    public interface IApiService
    {
        //Task<List<FilmDisplayDTO>> GetAllFilmsAsync();
        Task<User?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password, string family, string name, string? father);
        Task<List<SeansDTO>> GetAllSeansByDateAsync(DateOnly date);
        Task<List<Actor_in_filmDTO>> GetActorsInSelectedFilmAsync(SeansDTO? selectedFilm);
        Task<List<Rewards_in_filmDTO>> GetRewardsInSelectedFilmAsync(SeansDTO? selectedFilm);

    }
}
