﻿using Hunter.Models;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hunter.Settings
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Settings : Page
    {
        public userMessages NewUser;
        public Settings()
        {
            BgmPlayer.getInstance();
            this.InitializeComponent();
            NewUser = userInfo.getInstance();
            volumeSlider.Value = BgmPlayer.MusicPlayer.Volume*100;
            if(BgmPlayer.MusicPlayer.AutoPlay)
            {
                bgmswitch.IsOn = true;
            }
            else
            {
                bgmswitch.IsOn = false;
            }
        }

        private void volumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            BgmPlayer.MusicPlayer.Volume = (double)(volumeSlider.Value / 100);
            ButtonPlayer.MusicPlayer.Volume = (double)(volumeSlider.Value * 0.35 / 100);
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
           if(bgmswitch.IsOn==true)
            {
                BgmPlayer.MusicPlayer.Play();
                BgmPlayer.MusicPlayer.AutoPlay = true;
                ButtonPlayer.MusicPlayer.Volume = BgmPlayer.MusicPlayer.Volume * 0.35;
                
            }
            else if (bgmswitch.IsOn == false)
            {
                BgmPlayer.MusicPlayer.Stop();
                BgmPlayer.MusicPlayer.AutoPlay = false;
                ButtonPlayer.MusicPlayer.Volume = 0;
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonPlayer.MusicPlayer.Play();
            NewUser.ID = "";NewUser.money = 0;NewUser.nickName = "";NewUser.ps = "";NewUser.rate = 0;NewUser.headimg = "";NewUser.Exp = 0;
            Frame.Navigate(typeof(Log.Login));
            Frame.BackStack.Clear();
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
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
