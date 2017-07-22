using Hunter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hunter.Settings
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        private void volumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            BgmPlayer.MusicPlayer.Volume = (double)(volumeSlider.Value / 100);
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
           if(bgmswitch.IsOn==true)
            {
                BgmPlayer.MusicPlayer.Play();
            }
            else if (bgmswitch.IsOn == false)
            {
                BgmPlayer.MusicPlayer.Stop();
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Log.Login));
        }

        private void Bg1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Bg2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bg3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bg4_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
