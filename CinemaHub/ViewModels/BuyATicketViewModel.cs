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
    public partial class BuyATicketViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        public ObservableCollection<Ticket_type> TypesTicketsList { get; } = new ObservableCollection<Ticket_type>();
        public ObservableCollection<Mesta_in_zalDTO> Mesta { get; } = new ObservableCollection<Mesta_in_zalDTO>();

        [ObservableProperty]
        public Button? _option;

        [ObservableProperty]
        public decimal? _cost;

        [ObservableProperty]
        public decimal? _summa;

        [ObservableProperty]
        public SeansDTO? _seans;

        [ObservableProperty]
        public Zal? _selectedZal;

        [ObservableProperty]
        public Ticket_type? _selectedTicketType;

        [ObservableProperty]
        public string? _userName = UserSession.Current?.Name;

        [ObservableProperty]
        public string? _userFamily = UserSession.Current?.Family;

        [ObservableProperty]
        public string? _userFather = UserSession.Current?.Father;
        public BuyATicketViewModel(SeansDTO? selectedSeans, Zal? selectedZal)
        {
            _apiService = new ApiService();
            Seans = selectedSeans;
            SelectedZal = selectedZal;
            _ =  GetTicketTypesAsync();
        }

        public async Task GetTicketTypesAsync()
        {
            TypesTicketsList.Clear();
            var result = await _apiService.GetTicketTypesAsync();
            foreach (var ticketType in result) 
            {
                TypesTicketsList.Add(ticketType);
            }
            _ = GetMestaAsync();
        }

        public async Task GetMestaAsync()
        {
            Mesta.Clear();
            var result = await _apiService.GetMestaSelectedZalAsync(SelectedZal);
            foreach (var mesto in result)
            {
                Mesta.Add(mesto);

            }
            _ = GetCostAndSum();
        }

        public async Task GetCostAndSum()
        {
            Cost = 1000;
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday || DateTime.Now.DayOfWeek == DayOfWeek.Saturday) 
            {
                Cost = Convert.ToDecimal(1.5) * Cost;
            }
            if(SelectedTicketType?.Name == "Вип")
            {
                Cost += 300;
            }
        }

        partial void OnSelectedTicketTypeChanged(Ticket_type? value)
        {
            if (value != null)
            {
                Task.Run(async () => await GetCostAndSum());
            }
        }

        [RelayCommand]
        public async Task GoBack(Window? currentWindow)
        {
            currentWindow?.Close();
        }
    }
}
