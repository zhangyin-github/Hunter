using Hunter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

namespace Hunter.Log
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Register : Page
    {
        public userMessages NewUser;
        public Register()
        {
            getlink.getInstance();
            NewUser = userInfo.getInstance();
            this.InitializeComponent();
        }

        private async void login_ClickAsync(object sender, RoutedEventArgs e)
        {
            ButtonPlayer.MusicPlayer.Play();
            ring.IsActive = true;
            userinfo.IsEnabled = false;
            passwordinfo.IsEnabled = false;
            login.IsEnabled = false;
            if (userinfo.Text != "" && passwordinfo.Password != "")
            {
                if (passwordinfo.Password != password_againinfo.Password)//判断是否一致
                {
                    ShowNotComponent();
                }
                if (userinfo.Text == "")//判段是否输入
                {
                    ShowNotHave();
                }
                else
                {
                    using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                    {
                        TimeSpan ts = new TimeSpan(25000000);
                        client.Timeout = ts;
                        try
                        {
                            var kvp = new List<KeyValuePair<string, string>>
                            {
                                 new KeyValuePair<string,string>("action", "reg"),
                                 new KeyValuePair<string,string>("userid", userinfo.Text),
                                  new KeyValuePair<string,string>("password", passwordinfo.Password),
                       
                            };
                            System.Net.Http.HttpResponseMessage response = await client.PostAsync(getlink.Ip.ip, new FormUrlEncodedContent(kvp));
                            if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                            {
                                string responseBody = await response.Content.ReadAsStringAsync();
                                if (responseBody == "success")
                                {
                                    var info = new List<KeyValuePair<string, string>>
                                 {
                                      new KeyValuePair<string,string>("userid", userinfo.Text),
                                      new KeyValuePair<string,string>("action", "info"),
                                 };
                                    System.Net.Http.HttpResponseMessage user = await client.PostAsync(getlink.Ip.ip, new FormUrlEncodedContent(info));
                                    if (user.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                                    {
                                        string userinfo = await user.Content.ReadAsStringAsync();
                                        var userdata = userinfo.Split(',');
                                        NewUser.ID = userdata[0];
                                        NewUser.nickName = userdata[1];
                                        if (userdata[2] == "")
                                        {
                                            NewUser.Exp = 0;
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
                                    }
                                    ring.IsActive = false;
                                    Frame.Navigate(typeof(Room.RoomPage));
                                    Frame.BackStack.Clear();
                                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                                }
                                else
                                {
                                    ring.IsActive = false;
                                    var msgDialog = new Windows.UI.Popups.MessageDialog("用户名已存在，请修改用户名") { Title = "注册失败" };
                                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => {
                                        userinfo.IsEnabled = true;
                                        passwordinfo.IsEnabled = true;
                                        login.IsEnabled = true;
                                    }));
                                    await msgDialog.ShowAsync();
                                }
                            }
                        }
                        catch
                        {
                            ring.IsActive = false;
                            var msgDialog = new Windows.UI.Popups.MessageDialog("服务器可能开小差了，请稍后再试") { Title = "注册失败" };
                            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => {
                                userinfo.IsEnabled = true;
                                passwordinfo.IsEnabled = true;
                                login.IsEnabled = true;
                            }));
                            await msgDialog.ShowAsync();

                        }
                        finally
                        {
                            ring.IsActive = false;
                            userinfo.IsEnabled = true;
                            passwordinfo.IsEnabled = true;
                            login.IsEnabled = true;
                        }


                    }
                }
            }
        
        }

        private async void ShowNotComponent()//密码不一致弹窗内容
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("请检查两次输入是否一致") { Title = "密码不匹配" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("重新输入", uiCommand => { passwordinfo.Password = $""; password_againinfo.Password = $""; }));
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("返回登录", uiCommand => { Frame.Navigate(typeof(Log.Login)); ; }));
            await msgDialog.ShowAsync();
        }

        private async void ShowNotHave()//未输入弹窗内容
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("请输入用户名") { Title = "提示" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("重新输入", uiCommand => { passwordinfo.Password = $""; password_againinfo.Password = $""; userinfo.Text = $""; }));
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("返回登录", uiCommand => { Frame.Navigate(typeof(Log.Login)); ; }));
            await msgDialog.ShowAsync();
        }

        private async void ShowHad()//已存在弹窗内容
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("该用户已存在") { Title = "提示" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("重新输入", uiCommand => { userinfo.Text = $""; }));
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("返回登录", uiCommand => { Frame.Navigate(typeof(Log.Login)); ; }));
            await msgDialog.ShowAsync();
        }
    }
}
