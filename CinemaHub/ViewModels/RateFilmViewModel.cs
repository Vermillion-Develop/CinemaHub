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
    public partial class RateFilmViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        public List<int> _rates = new List<int>()
        {
            1,2,3,4,5
        };

        [ObservableProperty]
        public int? _selectedRate;
        [ObservableProperty]
        public string? _textError;

        private SeansDTO? _seans;

        public RateFilmViewModel(SeansDTO? selectedSeans)
        {
            _apiService = new ApiService();
            System.Diagnostics.Debug.WriteLine(selectedSeans?.FK_Film);
            _seans = selectedSeans;
        }

        [RelayCommand]

        public async Task AddNewRateAsync(Window? currentWindow)
        {
            if (SelectedRate == null || _seans == null)
            {
                TextError = "Выберите оценку!";
                return;
            }
            var response = await _apiService.RateTheFilmAsync(_seans?.FK_Film, UserSession.Current?.Id, SelectedRate);
            System.Diagnostics.Debug.WriteLine(response);
            currentWindow?.Close();
        }

        [RelayCommand]

        public async Task GoBackToFilm(Window? currentWindow)
        {
            currentWindow?.Close();
        }
    }
}
