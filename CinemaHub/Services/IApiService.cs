using System;
using System.Collections.Generic;
using System.Text;
using CinemaHubShared.Models;
using System.Threading.Tasks;

namespace CinemaHub.Services
{
    public interface IApiService
    {
        Task<User?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(string email, string password, string family, string name, string? father);
        Task<List<SeansDTO>> GetAllSeansByDateAsync(DateOnly date, Zal? selectedZal);
        Task<List<Actor_in_filmDTO>> GetActorsInSelectedFilmAsync(SeansDTO? selectedFilm);
        Task<List<Rewards_in_filmDTO>> GetRewardsInSelectedFilmAsync(SeansDTO? selectedFilm);
        Task<List<Comments_in_filmDTO>> GetCommentsInSelectedFilmAsync(SeansDTO? selectedFilm);
        Task<bool> RateTheFilmAsync(int? selectedFilm, int? selectedUser, int? selectedRate);
        Task<bool> MakeACommenTheFilmAsync(int? userId, int? filmId, string? commentDescription, DateTime? commentDate);
        Task<List<Ticket_type>> GetTicketTypesAsync();
        Task<List<Zal>> GetZalsAsync();
        Task<List<Mesta_in_zalDTO>> GetMestaSelectedZalAsync(Zal? selectedZal);
    }
}
