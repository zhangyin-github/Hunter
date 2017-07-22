using Hunter.Room;
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

namespace Hunter.Missions
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Task_Message : Page
    {
        public int time;
        public Task_Message()
        {
            NowMission.getInstance();
            UserAnswer.getInstance();
            this.InitializeComponent();
            title.Text = NowMission.Task.Title;
            content.Text = NowMission.Task.Content1;
            tips.Text = NowMission.Task.Tips1;
            time = 1;
            if(UserAnswer.Answer.answer!=null)
            {
                answer.Text = UserAnswer.Answer.answer;
            }
            else
            {
                answer.Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Missions.QrCode));

        }

        private async void Button_Click_1Async(object sender, RoutedEventArgs e)
        {
            if (time == 1)
            {
                if (answer.Text == NowMission.Task.Answer1)
                {
                   if(NowMission.Task.Content2!="")
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("恭喜您成功解开本阶段谜题，即将进入下一阶段") { Title = "提示" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();
                        time++;
                        content.Text = NowMission.Task.Content2;
                        tips.Text = NowMission.Task.Tips2;
                        answer.Text = "";
                    }
                    else
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("恭喜您成功完成本谜题解谜任务") { Title = "提示" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { submit.IsEnabled = false;scan.IsEnabled = false; }));
                        await msgDialog.ShowAsync();
                        time = 0;
                    }
                }
                else
                {
                    var msgDialog = new Windows.UI.Popups.MessageDialog("答案错误，解谜失败，请重新思考") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                    await msgDialog.ShowAsync();
                }
            }
            else if (time == 2 && NowMission.Task.Content2 !="")
            {
                if (answer.Text == NowMission.Task.Answer2)
                {
                    if (NowMission.Task.Content3 != "")
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("恭喜您成功解开本阶段谜题，即将进入下一阶段") { Title = "提示" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();
                        time++;
                        content.Text = NowMission.Task.Content3;
                        tips.Text = NowMission.Task.Tips3;
                        answer.Text = "";
                    }
                    else
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("恭喜您成功完成本谜题解谜任务") { Title = "提示" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { submit.IsEnabled = false; scan.IsEnabled = false; }));
                        await msgDialog.ShowAsync();
                        time = 0;
                    }
                }
                else
                {
                    var msgDialog = new Windows.UI.Popups.MessageDialog("答案错误，解谜失败，请重新思考") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                    await msgDialog.ShowAsync();
                }
            }
            else if (time == 3 && NowMission.Task.Content3 != "")
            {
                if (answer.Text == NowMission.Task.Answer3)
                {
                    var msgDialog = new Windows.UI.Popups.MessageDialog("恭喜您成功完成本谜题解谜任务") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { submit.IsEnabled = false; scan.IsEnabled = false; }));
                    await msgDialog.ShowAsync();
                    time = 0;
                    
                }
                else
                {
                    var msgDialog = new Windows.UI.Popups.MessageDialog("答案错误，解谜失败，请重新思考") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                    await msgDialog.ShowAsync();
                }
            }
        }
    }
}
