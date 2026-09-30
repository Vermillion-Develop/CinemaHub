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

namespace CinemaHub.ViewModels
{
    public partial class PrimaryWindowViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        public ObservableCollection<SeansDTO> Films { get; } = new();

        [ObservableProperty]
        public List<TimeSpan> _times = new List<TimeSpan>()
        {
            new TimeSpan(8, 0, 0),
            new TimeSpan(10, 30, 0),
            new TimeSpan(13, 0, 0),
            new TimeSpan(15, 30, 0),
            new TimeSpan(18, 00, 0),
            new TimeSpan(20, 30, 0),
            new TimeSpan(23, 00, 0)
        };

        [ObservableProperty]
        public TimeSpan _selectedTime;
        [ObservableProperty]
        public DateTimeOffset? _selectedDateSeansAxaml = DateTimeOffset.Now;

        [ObservableProperty]
        public Button? _option;

        [ObservableProperty]
        public DateOnly _selectedDateSeans;
        public PrimaryWindowViewModel()
        {
            _apiService = new ApiService();
            SelectedTime = Times.FirstOrDefault();
        }

      

        [RelayCommand]
        public async Task OpenFilms(Window? currentWindow)
        {
            
            PrimaryWindow primary = new PrimaryWindow
            {
                DataContext = new PrimaryWindowViewModel()
            };
            primary.Show();
            currentWindow?.Close();
        }

        [RelayCommand]
        public async Task OpenMyTickets(Window? currentWindow)
        {
            
            PrimaryWindow primary = new PrimaryWindow
            {
                DataContext = new PrimaryWindowViewModel()
            };
            primary.Show();
            currentWindow?.Close();
        }

        [RelayCommand]
        public async Task OpenOptions(Window? currentWindow)
        {
            
            PrimaryWindow primary = new PrimaryWindow
            {
                DataContext = new PrimaryWindowViewModel()
            };
            primary.Show();
            currentWindow?.Close();
        }

        [RelayCommand]
        public async Task LogOut(Window? currentWindow)
        {
            
            StartWindow start = new StartWindow
            {
                DataContext = new LoginViewModel()
            };
            start.Show();
            currentWindow?.Close();
        }

        [RelayCommand]
        public async Task RefreshSeans()
        {
            if (!SelectedDateSeansAxaml.HasValue)
            {
                return;
            }

            SelectedDateSeans = DateOnly.FromDateTime(SelectedDateSeansAxaml.Value.DateTime);

            var selectedFilms = await _apiService.GetAllSeansByDateAsync(SelectedDateSeans);

            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                Films.Clear();
                foreach (var film in selectedFilms)
                {
                    Films.Add(film);
                }
            });
        }
    }
}
