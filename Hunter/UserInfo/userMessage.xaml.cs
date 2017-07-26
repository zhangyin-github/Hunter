using Hunter.Models;
using Hunter.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hunter.UserInfo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class userMessage : Page
    {
       
        public string oldnickName;
        public object Console { get; private set; }
        public bool newnickNameCanBeCahenged = false;
        public userMessages NewUser;
        public userMessage()
        {
            
            NewUser = userInfo.getInstance();

            this.InitializeComponent();
            ExpBar.Maximum = 1000;
            ExpBar.Minimum = 0;
            nickName.Text = NewUser.nickName;
            ID.Text = NewUser.ID;
            rate.Text = (NewUser.Exp / 1000).ToString();
            Exp.Text = (NewUser.Exp % 1000).ToString()+"/1000";
            ExpBar.Value = NewUser.Exp % 1000;
            psTextBox.Text = NewUser.ps;

            List<solve> difficulties = new List<solve>();
            difficulties.Add(new solve() { difficulty = "全部难度题目" , difficultyScores = "解谜数目（成功/失败）：16/20" });
            difficulties.Add(new solve() { difficulty = "三星难度题目" , difficultyScores = "解谜数目（成功/失败）：3/3" });
            difficulties.Add(new solve() { difficulty = "二星难度题目" , difficultyScores = "解谜数目（成功/失败）：3/2" });
            difficulties.Add(new solve() { difficulty = "一星难度题目" , difficultyScores = "解谜数目（成功/失败）：4/0" });
            solveCombobox.ItemsSource = difficulties;

            List<create> createPuzzles = new List<create>();
            createPuzzles.Add(new create() { createTitle = "云霄飞车杀人事件", createScores = "被解次数（成功/失败）：126/2000" });
            createPuzzles.Add(new create() { createTitle = "董事长千金绑架事件", createScores = "被解次数（成功/失败）：50/6030" });
            createPuzzles.Add(new create() { createTitle = "偶像密室杀人事件", createScores = "被解次数（成功/失败）：39/1380" });
            createPuzzles.Add(new create() { createTitle = "大都会暗号地图事件", createScores = "被解次数（成功/失败）：96/2463" });
            createPuzzles.Add(new create() { createTitle = "新干线大爆破事件", createScores = "被解次数（成功/失败）：87/3521" });
            createCombobox.ItemsSource = createPuzzles;
            object sender = null;
            RoutedEventArgs e = null;
            refreshbutton_ClickAsync(sender, e);

        }


        private async void SelectPicButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonPlayer.MusicPlayer.Play();
            FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                openPicker.FileTypeFilter.Add(".jpg");
                openPicker.FileTypeFilter.Add(".png");
                StorageFile file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                OutputTextBlock.Text = "您已选择名为" + file.Name + "的文件";
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    var image = new BitmapImage();
                    
                    image.SetSource(stream);
                    headPic.Source = image;
                    var streamData = await file.OpenReadAsync();
                    var bytes = new byte[streamData.Size];
                using (var dataReader = new DataReader(streamData))
                {
                    await dataReader.LoadAsync((uint)streamData.Size);
                    dataReader.ReadBytes(bytes);
                }
                string inputString = System.Convert.ToBase64String(bytes);
                NewUser.headimg = inputString;

            }
                else
                {
                    OutputTextBlock.Text = "没有选择图片";
                }
        }

        /// <summary>
        /// 修改昵称打开Flyout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changenickNameFlyout_Opening(object sender, object e)
        {
            oldnickName = nickName.Text;
            
        }


        /// <summary>
        /// 修改昵称TextBox文本变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newnickNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            

            if (string.IsNullOrEmpty(newnickNameTextBox.Text))
            {
                newnickNameTitleTextBlock.Text = "昵称不能为空！请重新输入！";
                checkTextBlockISWrong.Visibility = Visibility.Visible;
                checkTextBlockIsOk.Visibility = Visibility.Collapsed;
                newnickNameCanBeCahenged = false;
                return;
            }
            else
            {
              //  newnickNameTitleTextBlock.Text = "新昵称可用";
                checkTextBlockIsOk.Visibility = Visibility.Visible;
                checkTextBlockISWrong.Visibility = Visibility.Collapsed;
                newnickNameCanBeCahenged = true;
            }
        }
        /// <summary>
        /// 修改昵称关闭Flyout的动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void changenickNameFlyout_ClosedAsync(object sender, object e)
        {
            if (newnickNameCanBeCahenged)
            {
                nickName.Text = newnickNameTextBox.Text;
                NewUser.nickName = nickName.Text;
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    TimeSpan ts = new TimeSpan(15000000);
                    client.Timeout = ts;
                    try
                    {
                        var kvp = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("name", NewUser.nickName),
                        new KeyValuePair<string,string>("id", NewUser.ID),
                        new KeyValuePair<string,string>("action", "changename"),
                    };
                        System.Net.Http.HttpResponseMessage response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));
                        if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            if (responseBody == "success")
                            {
                                var msgDialog = new Windows.UI.Popups.MessageDialog("修改成功") { Title = "提示" };
                                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                                await msgDialog.ShowAsync();
                            }
                            else
                            {
                                var msgDialog = new Windows.UI.Popups.MessageDialog("修改失败，服务器可能开小差了，要不待会儿重试一下？") { Title = "提示" };
                                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                                await msgDialog.ShowAsync();
                            }
                        }
                    }
                    catch
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("服务器可能开小差了，请稍后再试") { Title = "登录失败" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();

                    }
                    finally
                    {

                    }


                }
            }
            else
            {
                nickName.Text = oldnickName;
            }

        }


        /// <summary>
        /// 解谜情况统计数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void solveCombobox_DropDownClosed(object sender, object e)
        {
            if( solveCombobox.SelectedItem !=null)
            {
                solve difficulty = solveCombobox.SelectedItem as solve;
                this.solveTextBlock.Text = difficulty.difficultyScores;
            }
        }
        /// <summary>
        /// 设迷情况统计数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createCombobox_DropDownClosed(object sender, object e)
        {
            if (createCombobox.SelectedItem != null)
            {
                create createTitle = createCombobox.SelectedItem as create;
                this.createTextBlock.Text = createTitle.createScores;
            }
        }


        private void submitWholeChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelWholeChange_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void submitWholeButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonPlayer.MusicPlayer.Play();
            var dialog = new ContentDialog()
            {
                Title = "消息提示",
                Content = "确认提交信息修改么?",
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消",
                FullSizeDesired = false,
            };

            dialog.PrimaryButtonClick += async (_s, _e) =>
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    NewUser.ps = psTextBox.Text;
                    TimeSpan ts = new TimeSpan(35000000);
                    client.Timeout = ts;
                    try
                    {
                        var kvp = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("name", NewUser.nickName),
                        new KeyValuePair<string,string>("sign", NewUser.ps),
                        new KeyValuePair<string,string>("id", NewUser.ID),
                        new KeyValuePair<string,string>("headicon", NewUser.headimg),
                        new KeyValuePair<string,string>("action", "changeinfo"),
                    };
                        string str = "name=" + NewUser.nickName+"&";
                        str += "action=" + "changeinfo" + "&";
                        str += "sign=" + NewUser.ps + "&";
                        str += "id=" + NewUser.ID + "&";
                        str += "headicon=" + System.Net.WebUtility.UrlEncode(NewUser.headimg);
                        
                        System.Net.Http.StringContent content = new StringContent(str,System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

                        System.Net.Http.HttpResponseMessage response = await client.PostAsync("http://qwq.itbears.club/hunter.php", content);
                        if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            if (responseBody == "success")
                            {
                                var msgDialog = new Windows.UI.Popups.MessageDialog("修改成功") { Title = "提示" };
                                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                                await msgDialog.ShowAsync();
                            }
                            else
                            {
                                var msgDialog = new Windows.UI.Popups.MessageDialog("修改失败，服务器可能开小差了，要不待会儿重试一下？") { Title = "提示" };
                                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                                await msgDialog.ShowAsync();
                            }
                        }
                    }
                    catch
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("服务器可能开小差了，请稍后再试") { Title = "修改失败" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();

                    }
                    finally
                    {

                    }


                }
            };
            await dialog.ShowAsync();
        }

        private async void refreshbutton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if(NewUser.headimg!=""&& NewUser.headimg != null)
            {
                var data = Convert.FromBase64String(NewUser.headimg);
                BitmapImage bi = new BitmapImage();
                Stream stream2Write;
                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {

                    stream2Write = stream.AsStreamForWrite();

                    await stream2Write.WriteAsync(data, 0, data.Length);

                    await stream2Write.FlushAsync();
                    stream.Seek(0);

                    await bi.SetSourceAsync(stream);
                    headPic.Source = bi;
                }
            }
           
        }

    }
}
