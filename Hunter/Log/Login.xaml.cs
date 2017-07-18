using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hunter.Log
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();

        }

        private async System.Threading.Tasks.Task login_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (username.Text != "" && passwordinfo.Password != "")
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    TimeSpan ts = new TimeSpan(25000000);
                    client.Timeout = ts;
                    try
                    {
                        var kvp = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("username", username.Text),
                        new KeyValuePair<string,string>("password", passwordinfo.Password),
                        new KeyValuePair<string,string>("action", "login"),
                    };
                        System.Net.Http.HttpResponseMessage response = await client.PostAsync("./test.php", new FormUrlEncodedContent(kvp));
                        if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                        {
                            Frame.Navigate(typeof(Room.RoomPage));
                            Frame.BackStack.Clear();

                        }
                    }
                    catch
                    {
                        Frame.Navigate(typeof(Room.RoomPage));
                        Frame.BackStack.Clear();
                    }
                    finally
                    {

                    }


                }
            }
            else
            {
                ShowPasswordWrong();
            }

           
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Log.Register));
        }

        private async void ShowUserWrong()//弹窗内容
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("请检查用户名是否输入正确，没有用户名可点击注册") { Title = "用户名不存在" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("重新输入", uiCommand => { this.userinfo.Text = $""; this.passwordinfo.Password = $""; }));
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("注册", uiCommand => { Frame.Navigate(typeof(Log.Register)); }));
            await msgDialog.ShowAsync();
        }

        private async void ShowPasswordWrong()//弹窗内容
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("请检查密码是否输入正确，如果是，请重新选择重新输入,或重新注册") { Title = "密码输入错误" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("重新输入", uiCommand => { passwordinfo.Password = $""; }));
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("注册", uiCommand => { Frame.Navigate(typeof(Log.Register)); }));
            await msgDialog.ShowAsync();
        }
    }
}
