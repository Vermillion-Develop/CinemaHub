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
    public partial class MyTicketsViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        public ObservableCollection<User_ticketsDTO> MyTickets { get; } = new ObservableCollection<User_ticketsDTO>();

        [ObservableProperty]
        public Button? _option;

        public MyTicketsViewModel()
        {
            _apiService = new ApiService();
            _ = GetUserTickets();
        }

        public async Task GetUserTickets()
        {
            Debug.WriteLine(UserSession.Current?.Id);
            var result = await _apiService.GetUserTicketsAsync(UserSession.Current?.Id);
            MyTickets.Clear();
            foreach (var item in result) 
            {
                MyTickets.Add(item);
            }
        }

       
    }
}
