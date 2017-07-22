using Hunter.Models;
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
            this.InitializeComponent();
            refreshbutton_ClickAsync(sender,e);

            ExpBar.Value = NewUser.Exp % 1000;
            Pointbar.Value = NewUser.money;
        }

        private void headicon_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserInfo.userMessage));
        }

        private async void refreshbutton_ClickAsync(object sender, RoutedEventArgs e)
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                TimeSpan ts = new TimeSpan(15000000);
                client.Timeout = ts;
                try
                {
                    RootObject[] MissionLists = await Proxy.GetMission();
                    int i = 0;
                    MissionList.Clear();
                    while(i<MissionLists.Length)
                    {
                        MissionList.Add( MissionLists[i]);
                        i++;
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
    }
}
