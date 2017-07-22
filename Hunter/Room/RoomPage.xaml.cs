﻿using Hunter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
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

namespace Hunter.Room
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RoomPage : Page
    {
        public ObservableCollection<RootObject> MissionList;
        public userMessages NewUser;
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var room = (RootObject)e.ClickedItem;
            NowMission.Task = room;
            Frame.Navigate(typeof(Missions.Task_Message));
        }
        public RoomPage()
        {
            object sender=null;
            RoutedEventArgs e=null;
            MissionList = MissionManager.getInstance();
            NewUser = userInfo.getInstance();
            NowMission.getInstance();
            UserAnswer.getInstance();
            this.InitializeComponent();
            refreshbutton_ClickAsync(sender,e);
            UserAnswer.Answer.answer = "";
            UserAnswer.Answer.time = 1;
            ExpBar.Value = NewUser.Exp % 1000;
            Pointbar.Value = NewUser.money;
        }

        private void headicon_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserInfo.userMessage));
        }

        private async void refreshbutton_ClickAsync(object sender, RoutedEventArgs e)
        {
            selectByBackgroundComboBox.SelectedIndex = 0;
            selectByDifficultyComboBox.SelectedIndex = 0;
            selectByThemeComboBox.SelectedIndex = 0;
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                TimeSpan ts = new TimeSpan(15000000);
                client.Timeout = ts;
                try
                {
                    var kvp = new List<KeyValuePair<string, string>>
                    {

                        new KeyValuePair<string,string>("action", "getmission"),
                    };
                    var response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));

                    string result = await response.Content.ReadAsStringAsync();
                   if(result=="false")
                    {
                        return;
                    }
                    else
                    {
                        var s = new DataContractJsonSerializer(typeof(RootObject[]));
                        var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                        RootObject[] data = (RootObject[])s.ReadObject(ms);
                        int i = 0;
                        MissionList.Clear();
                        while (i < data.Length)
                        {
                            MissionList.Add(data[i]);
                            i++;
                        }
                    }
                    
                }
                catch
                {
                    var msgDialog = new Windows.UI.Popups.MessageDialog("网络可能开小差了，请稍后再试") { Title = "刷新失败" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                    await msgDialog.ShowAsync();

                }
                finally
                {

                }


            }
        }

        private void NewTask_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(User_Upload.User_Upload));
        }
        private void MyItems_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Items.Bag));
        }
        private void Shop_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Items.Shop));
        }
        private void Set_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings.Settings));
        }

        private async void selectByThemeComboBox_DropDownClosedAsync(object sender, object e)
        {

            if (theme0.IsSelected)
            {
                refreshbutton_ClickAsync(sender, (RoutedEventArgs)e);
                return;
            }
            else
            {

                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    TimeSpan ts = new TimeSpan(15000000);
                    client.Timeout = ts;
                    try
                    {
                        var kvp = new List<KeyValuePair<string, string>>
                    {

                        new KeyValuePair<string,string>("action", "getmission"),
                    };
                        var response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));

                        string result = await response.Content.ReadAsStringAsync();
                        if (result == "false")
                        {
                            return;
                        }
                        else
                        {
                            var s = new DataContractJsonSerializer(typeof(RootObject[]));
                            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                            RootObject[] data = (RootObject[])s.ReadObject(ms);
                            int i = 0;
                            MissionList.Clear();
                            while (i < data.Length)
                            {
                                if (data[i].Classes == selectByThemeComboBox.SelectionBoxItem.ToString())
                                {
                                    if (selectByDifficultyComboBox.SelectedIndex == 0 && selectByDifficultyComboBox.SelectedIndex == 0)
                                    {
                                        MissionList.Add(data[i]);
                                    }
                                    else
                                    {
                                        if (data[i].Addr == selectByBackgroundComboBox.SelectionBoxItem.ToString())
                                        {
                                            MissionList.Add(data[i]);
                                        }

                                    }
                                }
                               
                                i++;
                            }
                        }

                    }
                    catch
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("网络可能开小差了，请稍后再试") { Title = "刷新失败" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();

                    }
                    finally
                    {

                    }


                }

            }
        }

        private async void selectByBackgroundComboBox_DropDownClosedAsync(object sender, object e)
        {
            if (background0.IsSelected)
            {
                refreshbutton_ClickAsync(sender, (RoutedEventArgs)e);
                return;
            }
            else
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    TimeSpan ts = new TimeSpan(15000000);
                    client.Timeout = ts;
                    try
                    {
                        var kvp = new List<KeyValuePair<string, string>>
                    {

                        new KeyValuePair<string,string>("action", "getmission"),
                    };
                        var response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));

                        string result = await response.Content.ReadAsStringAsync();
                        if (result == "false")
                        {
                            return;
                        }
                        else
                        {
                            var s = new DataContractJsonSerializer(typeof(RootObject[]));
                            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                            RootObject[] data = (RootObject[])s.ReadObject(ms);
                            int i = 0;
                            MissionList.Clear();
                            while (i < data.Length)
                            {
                                if (data[i].Addr == selectByBackgroundComboBox.SelectionBoxItem.ToString())
                                {
                                    if(selectByThemeComboBox.SelectedIndex==0&&selectByDifficultyComboBox.SelectedIndex==0)
                                    {
                                        MissionList.Add(data[i]);
                                    }
                                    else
                                    {
                                        if (data[i].Classes == selectByThemeComboBox.SelectionBoxItem.ToString())
                                        {
                                            MissionList.Add(data[i]);
                                        }

                                    }
                                }
                                i++;
                            }
                        }

                    }
                    catch
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("网络可能开小差了，请稍后再试") { Title = "刷新失败" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();

                    }
                    finally
                    {

                    }


                }
               
            }
        }

        private async void selectByDifficultyComboBox_DropDownClosedAsync(object sender, object e)
        {
            if (level0.IsSelected)
            {
                refreshbutton_ClickAsync(sender, (RoutedEventArgs)e);
                return;
            }
            else
            {

                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    TimeSpan ts = new TimeSpan(15000000);
                    client.Timeout = ts;
                    try
                    {
                        var kvp = new List<KeyValuePair<string, string>>
                    {

                        new KeyValuePair<string,string>("action", "getmission"),
                    };
                        var response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));

                        string result = await response.Content.ReadAsStringAsync();
                        if (result == "false")
                        {
                            return;
                        }
                        else
                        {
                            var s = new DataContractJsonSerializer(typeof(RootObject[]));
                            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                            RootObject[] data = (RootObject[])s.ReadObject(ms);
                            int i = 0;
                            MissionList.Clear();
                            while (i < data.Length)
                            {
                                /*if (data[i].Classes == selectByDifficultyComboBox.SelectionBoxItem.ToString())
                                {
                                    MissionList.Add(data[i]);
                                }*/
                                i++;
                            }
                        }

                    }
                    catch
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("网络可能开小差了，请稍后再试") { Title = "刷新失败" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();

                    }
                    finally
                    {

                    }


                }

            }
        }
    }
}
