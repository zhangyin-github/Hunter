using Hunter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hunter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
           
            this.InitializeComponent();
            BgmPlayer.getInstance();
            BgmPlayer.MusicPlayer.Name = "MusicPlayer";
            Music.Children.Add(BgmPlayer.MusicPlayer);
            BgmPlayer.MusicPlayer.Visibility = Visibility.Collapsed;
            BgmPlayer.MusicPlayer.IsLooping = true;
            BgmPlayer.MusicPlayer.AutoPlay = true;
            BgmPlayer.MusicPlayer.Source = new Uri("ms-appx:///Assets/bgm.mp3");
            BgmPlayer.MusicPlayer.Play();
            BgmPlayer.MusicPlayer.Volume = 1;
            ButtonPlayer.getInstance();
            ButtonPlayer.MusicPlayer.Name = "MusicPlayer";
            Music.Children.Add(ButtonPlayer.MusicPlayer);
            ButtonPlayer.MusicPlayer.Visibility = Visibility.Collapsed;
            ButtonPlayer.MusicPlayer.IsLooping = false;
            ButtonPlayer.MusicPlayer.AutoPlay = false;
            ButtonPlayer.MusicPlayer.Source = new Uri("ms-appx:///Assets/button.wav");
            ButtonPlayer.MusicPlayer.Volume = 0.35;
            MainFrame.Navigate(typeof(Log.Login));
            MainFrame.Navigated += OnNavigated;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = MainFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ((Frame)sender).CanGoBack ?
                AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        private void BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = MainFrame;
            if (rootFrame == null)
                return;
            if (rootFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
            
        }
    }
    

}
