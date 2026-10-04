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
using System.Diagnostics;

namespace CinemaHub.ViewModels
{
    public partial class PrimaryWindowViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;


        public ObservableCollection<SeansDTO> Films { get; } = new();
        public ObservableCollection<Zal> Zals { get; } = new();

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
        public Bitmap? _readyImg;

        [ObservableProperty]
        public TimeSpan _selectedTime;

        [ObservableProperty]
        public Zal? _selectedZal;

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
            _ = GetZalsAsync();
        }

        [RelayCommand]
        public async Task RefreshSeans()
        {
            
            if (!SelectedDateSeansAxaml.HasValue)
            {
                return;
            }

            SelectedDateSeans = DateOnly.FromDateTime(SelectedDateSeansAxaml.Value.DateTime);

            var selectedFilms = await _apiService.GetAllSeansByDateAsync(SelectedDateSeans, SelectedZal);

            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                Films.Clear();
                foreach (var film in selectedFilms)
                {
                    ReadyImg = Helpers.ImgDehash.LoadFromBytes(film.FilmHashedImg);
                    Films.Add(film);
                }
            });
        }

        public async Task GetZalsAsync()
        {
            Zals.Clear();
            var result = await _apiService.GetZalsAsync();
            foreach (var zal in result)
            {
                Zals.Add(zal);
            }
        }

        public async Task OpenAboutFilm(Window? currentWindow, SeansDTO seans)
        {
            AboutFilmWindow about = new AboutFilmWindow()
            {
                DataContext = new AboutFilmViewModel(seans)
            };
            about.Show();
            currentWindow?.Hide();
        }

        [RelayCommand]
        private async Task OpenAboutFilmFromXaml(object? parameter)
        {
            if (parameter is System.Collections.IList values && values.Count == 2)
            {
                var window = values[0] as Window;
                var seans = values[1] as SeansDTO;

                if (seans != null)
                {
                    await OpenAboutFilm(window, seans);
                }
            }
        }

        [RelayCommand]
        public async Task OpenBuyTicketAsync(SeansDTO seans)
        {
            BuyATicketWindow buy = new BuyATicketWindow()
            {
                DataContext = new BuyATicketViewModel(seans, SelectedZal)
            };
            buy.Show();
        }
    }
}
