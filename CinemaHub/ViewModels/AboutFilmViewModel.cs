using Avalonia.Controls;
using Avalonia.Media.Imaging;
using CinemaHub.Services;
using CinemaHub.Views;
using CinemaHubShared.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
        public Bitmap? _actorImage;
        [ObservableProperty]
        public Bitmap? _rewardImage;

        [ObservableProperty]

        public decimal? _rate;

        [ObservableProperty]
        public string? _description;

        [ObservableProperty]
        public string? _nameFilm;

        public ObservableCollection<Actor_in_filmDTO> ActorsInFilm { get; } = new();
        public ObservableCollection<Rewards_in_filmDTO> RewardsInFilm { get; } = new();
        public ObservableCollection<Comments_in_filmDTO> CommentsInFilm { get; } = new();

        [ObservableProperty]
        public SeansDTO? _selectedSeans;

        public AboutFilmViewModel(SeansDTO? selectedFilm)
        {
            _apiService = new ApiService();

            ReadyImage = Helpers.ImgDehash.LoadFromBytes(selectedFilm?.FilmHashedImg);
            Rate = selectedFilm?.FilmRate ?? 0;
            Description = selectedFilm?.FilmDescription ?? string.Empty;
            NameFilm = selectedFilm?.FilmName ?? string.Empty;
            SelectedSeans = selectedFilm;
            _ = LoadActorsAsync(selectedFilm);
            
        }

        private async Task LoadActorsAsync(SeansDTO? selectedFilm)
        {
            try
            {
                var result = await _apiService.GetActorsInSelectedFilmAsync(selectedFilm);

                Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    ActorsInFilm?.Clear();
                    foreach (var actor in result)
                    {
                        System.Diagnostics.Debug.WriteLine(actor.ActorName);
                        ActorImage = Helpers.ImgDehash.LoadFromBytes(actor.Actor_image);
                        ActorsInFilm?.Add(actor);
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки актеров: {ex.Message}");
            }
            
            _ = LoadRewardsAcync(selectedFilm);
        }

        private async Task LoadRewardsAcync(SeansDTO? selectedFilm)
        {
            try
            {
                var result = await _apiService.GetRewardsInSelectedFilmAsync(selectedFilm);

                Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    RewardsInFilm?.Clear();
                    foreach (var reward in result)
                    {
                        System.Diagnostics.Debug.WriteLine(reward.NameReward);
                        RewardImage = Helpers.ImgDehash.LoadFromBytes(reward.RewardImage);
                        RewardsInFilm?.Add(reward);
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки актеров: {ex.Message}");
            }
            System.Diagnostics.Debug.WriteLine(selectedFilm?.FK_Film);
            _ = LoadCommentsAcyns(selectedFilm);

        }

        private async Task LoadCommentsAcyns(SeansDTO? selectedFilm)
        {
            try
            {
                var result = await _apiService.GetCommentsInSelectedFilmAsync(selectedFilm);
                System.Diagnostics.Debug.WriteLine(result.Count);
                Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                {
                    CommentsInFilm?.Clear();
                    foreach (var comment in result)
                    {
                        CommentsInFilm?.Add(comment);
                        System.Diagnostics.Debug.WriteLine(comment.UserName);
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки комментариев: {ex.Message}");
            }
        }

        [RelayCommand]

        public async Task OpenRateWindow(SeansDTO? selectedSeans)
        {
            RateFilmWindow rate = new RateFilmWindow()
            {
                DataContext = new RateFilmViewModel(selectedSeans)
            };

            rate.Show();
        }

        [RelayCommand]

        public async Task OpenNewCommentWindow(SeansDTO? selectedSeans)
        {
            AddNewCommentWindow comment = new AddNewCommentWindow()
            {
                DataContext = new AddNewCommentViewModel(selectedSeans)
            };

            comment.Show();
        }
    }
}
