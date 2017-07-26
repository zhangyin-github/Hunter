using Hunter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace Hunter.Items
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    /// 
    public sealed partial class Shop : Page
    {
        public userMessages NewUser;
        public List<Shop_ItemList> MyList;
        Shop_ItemList item = new Shop_ItemList();
        public Shop()
        {
            
            NewUser = userInfo.getInstance();
            this.InitializeComponent();
            MyList = Shop_ItemMenager.getInstance();
            NewUser = userInfo.getInstance();
            point.Text = NewUser.money.ToString();
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ButtonPlayer.MusicPlayer.Play();
            item =(Shop_ItemList) e.ClickedItem;
            Item_Info.Text = item.content;
            point.Text = NewUser.money.ToString();
        }

        private async void usebutton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (NewUser.money >= item.cost)
            {
                NewUser.money = NewUser.money - item.cost;
                point.Text = NewUser.money.ToString();
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    TimeSpan ts = new TimeSpan(15000000);
                    client.Timeout = ts;
                    try
                    {
                        var kvp = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("id", NewUser.ID),
                        new KeyValuePair<string,string>("name",item.name),
                        new KeyValuePair<string,string>("cost",NewUser.money.ToString()),
                        new KeyValuePair<string,string>("action", "buy"),
                    };
                        System.Net.Http.HttpResponseMessage response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));
                        if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            if (responseBody == "success")
                            {
                                var msgDialog = new Windows.UI.Popups.MessageDialog("购买成功") { Title = "提示" };
                                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                                await msgDialog.ShowAsync();
                            }
                            else
                            {
                                var msgDialog = new Windows.UI.Popups.MessageDialog("服务器可能开小差了，请稍后重试") { Title = "提示" };
                                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                                await msgDialog.ShowAsync();
                            }
                        }
                    }
                    catch
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("服务器可能开小差了，请稍后重试") { Title = "提示" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();
                    }
                }
            }
            else
            {
                point.Text = NewUser.money.ToString();
                var msgDialog = new Windows.UI.Popups.MessageDialog("您的积分不足，请努力赚取积分") { Title = "提示" };
                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("点我获取更多积分", uiCommand => { }));
                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                await msgDialog.ShowAsync();
            }
        }
    }
}
