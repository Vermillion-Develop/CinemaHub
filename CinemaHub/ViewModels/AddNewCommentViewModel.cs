using CinemaHub.Services;
using CinemaHubShared.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using Avalonia.Controls;
using CinemaHub.Views;
using System;
using System.Linq;
using Avalonia.Media.Imaging;

namespace CinemaHub.ViewModels
{
    public partial class AddNewCommentViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        public string? _textDescription;

        private SeansDTO? _seans;
        public AddNewCommentViewModel(SeansDTO? selectedSeans)
        {
            _apiService = new ApiService();
            System.Diagnostics.Debug.WriteLine(selectedSeans?.FK_Film);
            _seans = selectedSeans;
        }

        public async Task GoBackToFilm(Window? currentWindow)
        {
            currentWindow?.Close();
        }

        [RelayCommand]
        
        public async Task AddCommentTheFilmAsync(Window? currentWindow)
        {
            if (_seans == null)
            {
                System.Diagnostics.Debug.WriteLine("какой то прикольчик");
                return;
            }

            var result = await _apiService.MakeACommenTheFilmAsync(UserSession.Current?.Id, _seans.FK_Film, TextDescription, DateTime.UtcNow);
            System.Diagnostics.Debug.WriteLine(result);
            currentWindow?.Close();
        }

        [RelayCommand]

        public async Task GoBackToAboutFilm(Window? currentWindow)
        {
            currentWindow?.Close();
        }
    }
}
