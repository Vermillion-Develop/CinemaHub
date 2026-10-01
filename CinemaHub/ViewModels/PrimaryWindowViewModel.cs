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
        public Bitmap? _readyImg;

        [ObservableProperty]
        public TimeSpan _selectedTime;

        [ObservableProperty]
        public DateTimeOffset? _selectedDateSeansAxaml = DateTimeOffset.Now;

        [ObservableProperty]
        public Button? _option;

        [ObservableProperty]
        public DateOnly _selectedDateSeans;

        [ObservableProperty]

        public SeansDTO? _selectedSeans;


        public PrimaryWindowViewModel()
        {
            _apiService = new ApiService();
            SelectedTime = Times.FirstOrDefault();
        }


        //Обновить список фильмов на текущий день
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
                    ReadyImg = Helpers.ImgDehash.LoadFromBytes(film.FilmHashedImg);
                    Films.Add(film);
                }
            });
        }

        [RelayCommand]
        public async Task OpenAboutFilm(Window? currentWindow)
        {
            AboutFilmWindow about = new AboutFilmWindow()
            {
                DataContext = new AboutFilmViewModel(SelectedSeans)
            };
            about.Show();
            currentWindow?.Hide();
        }
    }
}
