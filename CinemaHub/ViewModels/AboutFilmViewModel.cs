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
    public partial class AboutFilmViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        public Button? _option;

        [ObservableProperty]
        public Bitmap? _readyImage;

        [ObservableProperty]

        public decimal? _rate;
        [ObservableProperty]

        public string? _description;

        public AboutFilmViewModel(SeansDTO? selectedFilm) 
        {
            _apiService = new ApiService();

            ReadyImage = Helpers.ImgDehash.LoadFromBytes(selectedFilm?.FilmHashedImg);

            Rate = selectedFilm?.FilmRate ?? 0;
            Description = selectedFilm?.FilmDescription ?? string.Empty;

        }
    }
}
