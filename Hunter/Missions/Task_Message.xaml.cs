using Hunter.Models;
using Hunter.Room;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hunter.Missions
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Task_Message : Page
    {
        public userMessages NewUser;
       
        public Task_Message()
        {
            NewUser = userInfo.getInstance();
            NowMission.getInstance();
            UserAnswer.getInstance();
            this.InitializeComponent();
            if(UserAnswer.Answer.time==1)
            {
                title.Text = NowMission.Task.Title;
                content.Text = NowMission.Task.Content1;
                tips.Text = NowMission.Task.Tips1;
            }
            else if(UserAnswer.Answer.time==2)
            {
                title.Text = NowMission.Task.Title;
                content.Text = NowMission.Task.Content2;
                tips.Text = NowMission.Task.Tips2;
            }
            else if (UserAnswer.Answer.time == 3)
            {
                title.Text = NowMission.Task.Title;
                content.Text = NowMission.Task.Content3;
                tips.Text = NowMission.Task.Tips3;
            }
            showimgAsync();

        }

        public async void showimgAsync()
        {
            if(NowMission.Task.Img1!="")
            {
                var data = Convert.FromBase64String(NowMission.Task.Img1);
                BitmapImage bi = new BitmapImage();
                Stream stream2Write;
                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {

                    stream2Write = stream.AsStreamForWrite();

                    await stream2Write.WriteAsync(data, 0, data.Length);

                    await stream2Write.FlushAsync();
                    stream.Seek(0);

                    await bi.SetSourceAsync(stream);
                    img1_bg.ImageSource = bi;
                    Image1.Source = bi;
                }
            }
            if (NowMission.Task.Img2 != "")
            {
                var data = Convert.FromBase64String(NowMission.Task.Img2);
                BitmapImage bi = new BitmapImage();
                Stream stream2Write;
                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {

                    stream2Write = stream.AsStreamForWrite();

                    await stream2Write.WriteAsync(data, 0, data.Length);

                    await stream2Write.FlushAsync();
                    stream.Seek(0);

                    await bi.SetSourceAsync(stream);
                    img2_bg.ImageSource = bi;
                    Image2.Source = bi;
                }
            }
            if (NowMission.Task.Img3 != "")
            {
                var data = Convert.FromBase64String(NowMission.Task.Img3);
                BitmapImage bi = new BitmapImage();
                Stream stream2Write;
                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {

                    stream2Write = stream.AsStreamForWrite();

                    await stream2Write.WriteAsync(data, 0, data.Length);

                    await stream2Write.FlushAsync();
                    stream.Seek(0);

                    await bi.SetSourceAsync(stream);
                    img3_bg.ImageSource = bi;
                    Image3.Source = bi;
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonPlayer.MusicPlayer.Play();
            Frame.Navigate(typeof(Missions.QrCode));

        }

        private async void Button_Click_1Async(object sender, RoutedEventArgs e)
        {
            ButtonPlayer.MusicPlayer.Play();
            if (UserAnswer.Answer.time == 1)
            {
                if (answer.Text == NowMission.Task.Answer1)
                {
                   if(NowMission.Task.Content2!="")
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("恭喜您成功解开本阶段谜题，即将进入下一阶段") { Title = "提示" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();
                        UserAnswer.Answer.time++;
                        content.Text = NowMission.Task.Content2;
                        tips.Text = NowMission.Task.Tips2;
                        answer.Text = "";
                        UserAnswer.Answer.answer = "";
                    }
                    else
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("恭喜您成功完成本谜题解谜任务") { Title = "提示" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", async uiCommand =>
                        {
                            NewUser.Exp = NewUser.Exp + 100; NewUser.money = NewUser.money + 10; using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                            {
                                TimeSpan ts = new TimeSpan(15000000);
                                client.Timeout = ts;
                                try
                                {
                                    var kvp = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("Exp", NewUser.Exp.ToString()),
                        new KeyValuePair<string,string>("money", NewUser.money.ToString()),
                        new KeyValuePair<string,string>("id", NewUser.ID),
                        new KeyValuePair<string,string>("action", "changeEM"),
                    };
                                  
                                    System.Net.Http.HttpResponseMessage response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));
                                    string result = await response.Content.ReadAsStringAsync();
                                }
                                catch
                                {
                                    

                                }
                                finally
                                {

                                }
                            }
                            submit.IsEnabled = false; scan.IsEnabled = false;
                        }));
                        await msgDialog.ShowAsync();
                        UserAnswer.Answer.time = 1;
                        UserAnswer.Answer.answer = "";
                    }
                }
                else
                {
                    var msgDialog = new Windows.UI.Popups.MessageDialog("答案错误，解谜失败，请重新思考") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                    await msgDialog.ShowAsync();
                }
            }
            else if (UserAnswer.Answer.time == 2 && NowMission.Task.Content2 !="")
            {
                if (answer.Text == NowMission.Task.Answer2)
                {
                    if (NowMission.Task.Content3 != "")
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("恭喜您成功解开本阶段谜题，即将进入下一阶段") { Title = "提示" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();
                        UserAnswer.Answer.time++;
                        content.Text = NowMission.Task.Content3;
                        tips.Text = NowMission.Task.Tips3;
                        answer.Text = "";
                        UserAnswer.Answer.answer = "";
                    }
                    else
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("恭喜您成功完成本谜题解谜任务") { Title = "提示" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", async uiCommand =>
                        {
                            NewUser.Exp = NewUser.Exp + 100; NewUser.money = NewUser.money + 10; using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                            {
                                TimeSpan ts = new TimeSpan(15000000);
                                client.Timeout = ts;
                                try
                                {
                                    var kvp = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("Exp", NewUser.Exp.ToString()),
                        new KeyValuePair<string,string>("money", NewUser.money.ToString()),
                        new KeyValuePair<string,string>("id", NewUser.ID),
                        new KeyValuePair<string,string>("action", "changeEM"),
                    };
                                    System.Net.Http.HttpResponseMessage response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));
                                    string result = await response.Content.ReadAsStringAsync();
                                }
                                catch
                                {


                                }
                                finally
                                {

                                }
                            }
                            submit.IsEnabled = false; scan.IsEnabled = false;
                        }));
                        await msgDialog.ShowAsync();
                        UserAnswer.Answer.time = 1;
                        UserAnswer.Answer.answer = "";
                    }
                }
                else
                {
                    var msgDialog = new Windows.UI.Popups.MessageDialog("答案错误，解谜失败，请重新思考") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                    await msgDialog.ShowAsync();
                }
            }
            else if (UserAnswer.Answer.time == 3 && NowMission.Task.Content3 != "")
            {
                if (answer.Text == NowMission.Task.Answer3)
                {
                    var msgDialog = new Windows.UI.Popups.MessageDialog("恭喜您成功完成本谜题解谜任务") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", async uiCommand =>
                    {
                        NewUser.Exp = NewUser.Exp + 100; NewUser.money = NewUser.money + 10; using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                        {
                            TimeSpan ts = new TimeSpan(15000000);
                            client.Timeout = ts;
                            try
                            {
                                var kvp = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("Exp", NewUser.Exp.ToString()),
                        new KeyValuePair<string,string>("money", NewUser.money.ToString()),
                        new KeyValuePair<string,string>("id", NewUser.ID),
                        new KeyValuePair<string,string>("action", "changeEM"),
                    };
                                System.Net.Http.HttpResponseMessage response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));
                                string result = await response.Content.ReadAsStringAsync();
                            }
                            catch
                            {


                            }
                            finally
                            {

                            }
                        }
                        submit.IsEnabled = false; scan.IsEnabled = false;
                    }));
                    await msgDialog.ShowAsync();
                    UserAnswer.Answer.time = 1;
                    UserAnswer.Answer.answer = "";

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
