using Hunter.Models;
using Hunter.Room;
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
    public sealed partial class Bag : Page
    {
        public User_ItemList item =new User_ItemList();
        public userMessages NewUser;
        public ObservableCollection<User_ItemList> MyList;
        public Bag()
        {
            this.InitializeComponent();
            MyList = User_ItemMenager.getInstance();
            NewUser = userInfo.getInstance();
            getlink.getInstance();
            point.Text = NewUser.money.ToString();
            getitem();
        }

        public async void getitem()
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                TimeSpan ts = new TimeSpan(15000000);
                client.Timeout = ts;
                try
                {
                    var kvp = new List<KeyValuePair<string, string>>
                    {

                        new KeyValuePair<string,string>("action", "getitem"),
                        new KeyValuePair<string,string>("id", NewUser.ID),
                    };
                    var response = await client.PostAsync(getlink.Ip.ip, new FormUrlEncodedContent(kvp));

                    string result = await response.Content.ReadAsStringAsync();
                    if (result=="false")
                    {
                        return;
                    }
                    else
                    {
                        var s = new DataContractJsonSerializer(typeof(User_ItemList[]));
                        var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                        User_ItemList[] data = (User_ItemList[])s.ReadObject(ms);
                        int i = 0;
                        if (MyList != null)
                        {
                            MyList.Clear();
                        }
                        while (i < data.Length)
                        {
                            MyList.Add(data[i]);
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
            }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {

            ButtonPlayer.MusicPlayer.Play();
            item = (User_ItemList)e.ClickedItem;
            Item_Info.Text = item.content;
            point.Text = NewUser.money.ToString();
        }
    }
}
