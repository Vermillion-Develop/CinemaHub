using CinemaHub.Services;
using CinemaHubShared.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CinemaHub.Views;
using Avalonia.Controls;

namespace CinemaHub.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public LoginViewModel()
        {
            _apiService = new ApiService();
        }

        [RelayCommand]
        private async Task LoginInAsync(Window? currentWindow)
        {
            ErrorMessage = string.Empty;

            if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Заполните поля!";
                return;
            }
            User? loggedUser = await _apiService.LoginAsync(Email, Password);
            if(loggedUser != null)
            {
                System.Diagnostics.Debug.WriteLine($"Вход выполнен под: {loggedUser.Name}");
                UserSession.Current = new CurrentUser
                {
                    Id = loggedUser.Id,
                    Family = loggedUser.Family,
                    Name = loggedUser.Name,
                    Father = loggedUser.Father,
                    FK_Role = loggedUser.FK_Role,
                    FK_Login = loggedUser.FK_Login,
                    Date_registration = loggedUser.Date_registration
                };
                PrimaryWindow primaryWindow = new PrimaryWindow()
                {
                    DataContext = new PrimaryWindowViewModel()
                };
                primaryWindow.Show();
                System.Diagnostics.Debug.WriteLine(UserSession.Current.Name);
                currentWindow?.Close();
            }
            else
            {
                ErrorMessage = "Неверный логин или пароль!";
            }
        }

        [RelayCommand]
        private async Task OpenRegistrationWindow(Window? currentWindow)
        {
            RegisterWindow register = new RegisterWindow()
            {
                DataContext = new RegisterViewModel()
            };

            register.Show();
            currentWindow?.Close();
        }


    }
}
