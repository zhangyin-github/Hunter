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
            if (password.Text != passwordinfo.Text)//判断是否一致
            {
                ShowMessageDialog();
            }
        }

        private async void ShowMessageDialog()//弹窗内容
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("请检查两次输入是否一致") { Title = "密码不匹配" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("重新输入", uiCommand => { passwordinfo.Text = $""; password_againinfo.Text = $""; }));
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("返回登录", uiCommand => { Frame.Navigate(typeof(Log.Login)); ; }));
            await msgDialog.ShowAsync();
        }
    }
}
