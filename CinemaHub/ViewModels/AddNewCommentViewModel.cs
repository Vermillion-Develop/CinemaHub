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
    }
}
