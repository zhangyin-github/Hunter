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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hunter.Room
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RoomPage : Page
    {
        public List<MissionList> MyList;
      
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var room = (MissionList)e.ClickedItem;
            Frame.Navigate(typeof(Missions.Task_Message));
        }
        public RoomPage()
        {
            MyList = ListManager.getInstance();
            this.InitializeComponent();
            
            
        }

        private void headicon_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UserInfo.userMessage));
        }

        private void refreshbutton_Click(object sender, RoutedEventArgs e)
        {
           
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
