using Avalonia.Controls;
using CinemaHub.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CinemaHub.ViewModels
{
    public static partial class PublicViewModelMethods
    {

        public static ICommand LogOutCommand { get; } = new AsyncRelayCommand<Window>(LogOut);
        public static ICommand OpenFilmsCommand { get; } = new AsyncRelayCommand<Window>(OpenFilms);
        public static ICommand OpenMyTicketsCommand { get; } = new AsyncRelayCommand<Window>(OpenMyTickets);
        public static ICommand OpenOptionsCommand { get; } = new AsyncRelayCommand<Window>(OpenOptions);
  

        public static async Task OpenFilms(Window? currentWindow)
        {

            PrimaryWindow primary = new PrimaryWindow
            {
                DataContext = new PrimaryWindowViewModel()
            };
            primary.Show();
            currentWindow?.Close();
        }
        public static async Task OpenMyTickets(Window? currentWindow)
        {

            MyTicketsWindow MyTick = new MyTicketsWindow
            {
                DataContext = new MyTicketsViewModel()
            };
            MyTick.Show();
            currentWindow?.Close();
        }

  
        public static async Task OpenOptions(Window? currentWindow)
        {

            PrimaryWindow primary = new PrimaryWindow
            {
                DataContext = new PrimaryWindowViewModel()
            };
            primary.Show();
            currentWindow?.Close();
        }

    
        public static async Task LogOut(Window? currentWindow)
        {

            StartWindow start = new StartWindow
            {
                DataContext = new LoginViewModel()
            };
            start.Show();
            currentWindow?.Close();
        }
    }
}
