using CinemaHub.Services;
using CinemaHubShared.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CinemaHub.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        public ObservableCollection<Zal> Zals { get; } = new();

        public MainWindowViewModel()
        {
            _apiService = new ApiService();
        }
    }
}
