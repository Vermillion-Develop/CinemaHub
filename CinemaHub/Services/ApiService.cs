using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CinemaHubShared.Models;
using Avalonia.Controls.Documents;
using System.Diagnostics;

namespace CinemaHub.Services
{
    public class ApiService:IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5062/")};
        }


        public async Task<User?> LoginAsync(string email, string password)
        {
            try
            {
                var requestData = new UserLoginRequest { Email = email, Password = password };
                var response = await _httpClient.PostAsJsonAsync("api/Users/Login", requestData);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<User>();
                }

                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сети при авторизации: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RegisterAsync(string email, string password, string family, string name, string? father) 
        {
            try
            {
                var registerData = new UserRegisterRequest { Email = email, Password = password, Family = family, Name = name, Father = father };
                var response = await _httpClient.PostAsJsonAsync("api/Users/Register", registerData);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сети при регистрации: {ex.Message}");
                return false;
            }
        }

       public async Task<List<Actor_in_filmDTO>> GetActorsInSelectedFilmAsync(SeansDTO? selectedFilm)
        {
            try
            {
                // Отправляем POST запрос, так как на сервере стоит [FromBody]
                var response = await _httpClient.PostAsJsonAsync("api/Films/actors", selectedFilm);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<Actor_in_filmDTO>>();
                    return result ?? new List<Actor_in_filmDTO>();
                }

                return new List<Actor_in_filmDTO>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сети при получении актеров: {ex.Message}");
                return new List<Actor_in_filmDTO>();
            }
        }

        public async Task<List<Rewards_in_filmDTO>> GetRewardsInSelectedFilmAsync(SeansDTO? selectedFilm)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Films/rewards", selectedFilm);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<Rewards_in_filmDTO>>();
                    return result ?? new List<Rewards_in_filmDTO>();
                }
                return new List<Rewards_in_filmDTO>();
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сети при получении наград: {ex.Message}");
                return new List<Rewards_in_filmDTO>();
            }
        }

        public async Task<List<SeansDTO>> GetAllSeansByDateAsync(DateOnly date, Zal? selectedZal)
        {
            try
            {
                var filmData = new FilmRequest(){ DateFilm = date, ZalId = selectedZal?.Id};
                var response = await _httpClient.PostAsJsonAsync("api/Films/getFilms", filmData);
                if (response.IsSuccessStatusCode)
                {
                   var result = await response.Content.ReadFromJsonAsync<List<SeansDTO>>();

                    if (result != null)
                    {
                        return result;
                    }
                    return new List<SeansDTO>();
                }
                return new List<SeansDTO>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сети при получении фильмов: {ex.Message}");
                return new List<SeansDTO>();
            }
        }

        public async Task <List<Comments_in_filmDTO>> GetCommentsInSelectedFilmAsync(SeansDTO? selectedFilm)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Films/comments", selectedFilm);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<Comments_in_filmDTO>>();
                    return result ?? new List<Comments_in_filmDTO>();
                }
                return new List<Comments_in_filmDTO>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сети при получении комментариев: {ex.Message}");
                return new List<Comments_in_filmDTO>();
            }
        }

        public async Task<bool> RateTheFilmAsync(int? selectedFilm, int? selectedUser, int? selectedRate )
        {
            try
            {
                var rateData = new RateUserRequest() { FilmId = selectedFilm, UserId = selectedUser, UserRate = selectedRate };
                var response = await _httpClient.PostAsJsonAsync("api/Films/rates", rateData);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка оценки фильма: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> MakeACommenTheFilmAsync(int? userId, int? filmId, string? commentDescription, DateTime? commentDate)
        {
            try
            {
                var commentData = new CommentUserRequest() { FilmId = filmId, UserId = userId, CommentDescription = commentDescription, CommentDate = commentDate };
                var response = await _httpClient.PostAsJsonAsync("api/Films/commentaries", commentData);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка оставления комментария: {ex.Message}");
                return false;
            }
        }
        public async Task<List<Ticket_type>> GetTicketTypesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Ticket_type>>("api/Films/ticketType");
                if (response == null)
                {
                    return new List<Ticket_type>();
                }
                return response;
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine("Пупупу.... типы билетов пупупу...");
                return new List<Ticket_type>();
            }
            
        }

        public async Task<List<Zal>> GetZalsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Zal>>("api/Films/zals");
                if (response == null)
                {
                    return new List<Zal>();
                }
                return response;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Пупупу.... залы пупупу...");
                return new List<Zal>();
            }

        }

        public async Task<List<Mesta_in_zalDTO>> GetMestaSelectedZalAsync(Zal? selectedZal)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Films/mestos", selectedZal);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<Mesta_in_zalDTO>>();
                    return result ?? new List<Mesta_in_zalDTO>();
                }
                return new List<Mesta_in_zalDTO>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сети при получении мест: {ex.Message}");
                return new List<Mesta_in_zalDTO>();
            }
        }

        public async Task<bool> BuyATicketAsync(SeansDTO? seans, decimal? cost, int? typeTicket, int? selectedZal, int? ticketStatus, DateTime? date,  int? user, int? selectedMesto)
        {
            try
            {
                var buyData = new BuyTicketRequest { NameTicket = seans?.FilmName, CostTicket = cost, FK_Type_ticket = typeTicket, FK_Zal = selectedZal, FK_Ticket_status = ticketStatus, DateSold = date, UserId = user, Mesto = selectedMesto };
                var response = await _httpClient.PostAsJsonAsync("api/Films/buyTicket", buyData);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сети при покупк билета: {ex.Message}");
                return false;
            }
        }

        public async Task<List<User_ticketsDTO>> GetUserTicketsAsync(int? userId)
        {
            try
            {
                if (userId == null) return new List<User_ticketsDTO>();
                var response = await _httpClient.GetFromJsonAsync<List<User_ticketsDTO>>($"api/Films/userBilets?user={userId}");
                return response ?? new List<User_ticketsDTO>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при получении билетов: {ex.Message}");
                return new List<User_ticketsDTO>();
            }
        }

    }
}
