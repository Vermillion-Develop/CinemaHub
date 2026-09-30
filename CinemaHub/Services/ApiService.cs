using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CinemaHubShared.Models;

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

        //public async Task<List<FilmDisplayDTO>> GetAllFilmsAsync()
        //{
        //    try
        //    {
        //        var response = await _httpClient.GetFromJsonAsync<List<FilmDisplayDTO>>("api/Films");
        //        return response ?? new List<FilmDisplayDTO>();
        //    }
        //    catch(Exception ex) 
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Ошибка сети при получении фильмов: {ex.Message}");
        //        return new List<FilmDisplayDTO>();
        //    }
        //}     

        public async Task <List<SeansDTO>> GetAllSeansByDateAsync(DateOnly date)
        {
            try
            {
                string formattedDate = date.ToString("yyyy-MM-dd");
                var response = await _httpClient.GetFromJsonAsync<List<SeansDTO>>($"api/Films?date={formattedDate}");
                return response ?? new List<SeansDTO>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка сети при получении фильмов: {ex.Message}");
                return new List<SeansDTO>();
            }
        }
    }
}
