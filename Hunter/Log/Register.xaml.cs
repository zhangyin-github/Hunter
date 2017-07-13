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

namespace Hunter.Log
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Register : Page
    {
        public Register()
        {
            this.InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (passwordinfo.Password != password_againinfo.Password)//判断是否一致
            {
                ShowNotComponent();
            }
            if (userinfo.Text == "")//判段是否输入
            {
                ShowNotHave();
            }
            if (userinfo.Text == "user1")//判断是否已存在
            {
                ShowHad();
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
