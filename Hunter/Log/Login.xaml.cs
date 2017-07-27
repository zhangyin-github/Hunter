using Hunter.Models;
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
        public userMessages NewUser;
        public Login()
        {
            NewUser = userInfo.getInstance();
            this.InitializeComponent();

        }

        private async void login_ClickAsync(object sender, RoutedEventArgs e)
        {
            ButtonPlayer.MusicPlayer.Play();
            ring.IsActive = true;
            userinfo.IsEnabled = false;
            passwordinfo.IsEnabled = false;
            register.IsEnabled = false;
            login.IsEnabled = false;
            if (userinfo.Text != "" && passwordinfo.Password != "")
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    TimeSpan ts = new TimeSpan(15000000);
                    client.Timeout = ts;
                    try
                    {
                        var kvp = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("userid", userinfo.Text),
                        new KeyValuePair<string,string>("password", passwordinfo.Password),
                        new KeyValuePair<string,string>("action", "login"),
                    };
                        System.Net.Http.HttpResponseMessage response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));
                        if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            if(responseBody=="success")
                            {
                                var info = new List<KeyValuePair<string, string>>
                                 {
                                      new KeyValuePair<string,string>("userid", userinfo.Text),
                                      new KeyValuePair<string,string>("action", "info"),
                                 };
                                System.Net.Http.HttpResponseMessage user = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(info));
                                if (user.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                                {
                                    string userinfo = await user.Content.ReadAsStringAsync();
                                    var userdata = userinfo.Split(',');
                                    NewUser.ID = userdata[0];
                                    NewUser.nickName = userdata[1];
                                    if(userdata[2]=="")
                                    {
                                        NewUser.Exp =0;
                                    }
                                    else
                                    {
                                        NewUser.Exp = int.Parse(userdata[2]);
                                    }
                                    NewUser.ps = userdata[3];
                                    if (userdata[4] == "")
                                    {
                                        NewUser.money = 0;
                                    }
                                    else
                                    {
                                        NewUser.money = int.Parse(userdata[4]);
                                    }
                                    if (userdata[5] == "")
                                    {
                                        NewUser.headimg = "";
                                    }
                                    else
                                    {
                                        NewUser.headimg =userdata[5];
                                    }
                                }
                                ring.IsActive = false;
                                Frame.Navigate(typeof(Room.RoomPage));
                                Frame.BackStack.Clear();
                                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                            }
                            else
                            {
                               
                                var msgDialog = new Windows.UI.Popups.MessageDialog("用户名或密码错误，请检查您的用户名和密码") { Title = "登录失败" };
                                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => {
                                    userinfo.IsEnabled = true;
                                    passwordinfo.IsEnabled = true;
                                    register.IsEnabled = true;
                                    login.IsEnabled =true;
                                    ring.IsActive = false;
                                }));
                                await msgDialog.ShowAsync();
                            }
                        }
                    }
                    catch
                    {
                        ring.IsActive = false;
                        var msgDialog = new Windows.UI.Popups.MessageDialog("服务器可能开小差了，请稍后再试") { Title = "登录失败" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => {
                            userinfo.IsEnabled = true;
                            passwordinfo.IsEnabled = true;
                            register.IsEnabled = true;
                            login.IsEnabled = true;
                        }));
                        await msgDialog.ShowAsync();
                       
                    }
                    finally
                    {
                        userinfo.IsEnabled = true;
                        passwordinfo.IsEnabled = true;
                        register.IsEnabled = true;
                        login.IsEnabled = true;
                        ring.IsActive = false;
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
            ButtonPlayer.MusicPlayer.Play();
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
            userinfo.IsEnabled = true;
            passwordinfo.IsEnabled = true;
            register.IsEnabled = true;
            login.IsEnabled = true;
            ring.IsActive = false;
        }
    }
}
