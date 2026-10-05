using CinemaHub.Services;
using CinemaHubShared.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Avalonia.Controls;
using CinemaHub.Views;

namespace CinemaHub.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;
        [ObservableProperty]
        private string _secondPassword = string.Empty;
        [ObservableProperty]
        private string _family = string.Empty;
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private string _father = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public RegisterViewModel()
        {
            _apiService = new ApiService();
        }
        [RelayCommand]
        private async Task SignUpAsync()
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Family) || string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Заполните обязательные поля!";
                return;
            }
            if(Password.Length < 8)
            {
                ErrorMessage = "Длина пароля должна состоять минимум из 8 символов!";
                return;
            }
            if(Password != SecondPassword)
            {
                ErrorMessage = "Пароли не совпадают!";
                return;
            }
            if(await _apiService.RegisterAsync(Email, Password, Family, Name, Father))
            {
                ErrorMessage = "Регистрация прошла успешно!";
                return;
            }
            else
            {
                ErrorMessage = "Произошла ошибка регистрации";
                return;
            }
        }

        [RelayCommand]
        public async Task GoToStartWindow(Window? currentWindow)
        {
            StartWindow start = new StartWindow
            {
                DataContext = new LoginViewModel()
            };
            start.Show();
            currentWindow?.Close();
        }
    }
}
