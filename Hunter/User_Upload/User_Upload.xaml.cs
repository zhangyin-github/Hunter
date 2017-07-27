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
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media.Capture;
using Hunter.Models;
using System.Net.Http;
using Hunter.Room;
using Windows.UI.Core;
using Windows.Storage.Streams;
using System.Threading.Tasks;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hunter.User_Upload
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class User_Upload : Page
    {
        public userMessages NewUser;
        /// <summary>
        /// 三条线索的内容保存
        /// </summary>
       
        public object item;
        public Missons NewMisson;
        public User_Upload()
        {
            NewUser = userInfo.getInstance();
            this.InitializeComponent();
            UserAnswer.getInstance();
            NowMission.getInstance();
            addAsync();
          
            if (NowMission.Task.Title!=""&& NowMission.Task.Title!=null)
            {
                TitleTextbox.Text = NowMission.Task.Title;
            }
            else
            {
                TitleTextbox.Text = "";
            }
            if(UserAnswer.Answer.show==1)
            {
                keyTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                keyTextBox.Visibility = Visibility.Collapsed;
            }
            if(UserAnswer.Answer.time==1)
            {
                item = clue1;
                clueComboBox.SelectedIndex = 0;
                if (NowMission.Task.Classes!=""&&NowMission.Task.Classes !=null)
                {
                    if (NowMission.Task.Classes == "悬疑")
                    {
                        themeComboBox.SelectedIndex = 0;
                    }
                    else if (NowMission.Task.Classes == "推理")
                    {
                        themeComboBox.SelectedIndex = 1;
                    }
                    else if (NowMission.Task.Classes == "日常")
                    {
                        themeComboBox.SelectedIndex = 2;
                    }
                    else if (NowMission.Task.Classes == "其他")
                    {
                        themeComboBox.SelectedIndex = 3;
                    }
                }
                else
                {
                    themeComboBox.SelectedIndex = 0;
                }
                if(NowMission.Task.Addr != ""&& NowMission.Task.Addr !=null)
                {
                    if (NowMission.Task.Addr == "校园")
                    {
                        backgroundComboBox.SelectedIndex = 0;
                    }
                    else if (NowMission.Task.Addr == "密室")
                    {
                        backgroundComboBox.SelectedIndex = 1;
                    }
                    else if (NowMission.Task.Addr == "街道")
                    {
                        backgroundComboBox.SelectedIndex = 2;
                    }
                    else if (NowMission.Task.Addr == "其他")
                    {
                        backgroundComboBox.SelectedIndex = 3;
                    }
                }
                else
                {
                    backgroundComboBox.SelectedIndex = 0;
                }
                if(NowMission.Task.Content1 != ""&& NowMission.Task.Content1 !=null)
                {
                    contentTextBox.Text = NowMission.Task.Content1;
                    reminderTextBox.Text = NowMission.Task.Tips1;
                    keyTextBox.Text = NowMission.Task.Answer1;
                }
                else
                {
                    contentTextBox.Text = "";
                    reminderTextBox.Text = "";
                    keyTextBox.Text = "";
                }
            }
            else if (UserAnswer.Answer.time == 2)
            {
                item = clue2;
                clueComboBox.SelectedIndex = 1;
                if (NowMission.Task.Classes != "" && NowMission.Task.Classes != null)
                {
                    if (NowMission.Task.Classes == "悬疑")
                    {
                        themeComboBox.SelectedIndex = 0;
                    }
                    else if (NowMission.Task.Classes == "推理")
                    {
                        themeComboBox.SelectedIndex = 1;
                    }
                    else if (NowMission.Task.Classes == "日常")
                    {
                        themeComboBox.SelectedIndex = 2;
                    }
                    else if (NowMission.Task.Classes == "其他")
                    {
                        themeComboBox.SelectedIndex = 3;
                    }
                }
                else
                {
                    themeComboBox.SelectedIndex = 0;
                }
                if (NowMission.Task.Addr != "" && NowMission.Task.Addr != null)
                {
                    if (NowMission.Task.Addr == "校园")
                    {
                        backgroundComboBox.SelectedIndex = 0;
                    }
                    else if (NowMission.Task.Addr == "密室")
                    {
                        backgroundComboBox.SelectedIndex = 1;
                    }
                    else if (NowMission.Task.Addr == "街道")
                    {
                        backgroundComboBox.SelectedIndex = 2;
                    }
                    else if (NowMission.Task.Addr == "其他")
                    {
                        backgroundComboBox.SelectedIndex = 3;
                    }
                }
                else
                {
                    backgroundComboBox.SelectedIndex = 0;
                }
                if (NowMission.Task.Content2 != ""&& NowMission.Task.Content2 !=null)
                {
                    contentTextBox.Text = NowMission.Task.Content2;
                    reminderTextBox.Text = NowMission.Task.Tips2;
                    keyTextBox.Text = NowMission.Task.Answer2;
                }
                else
                {
                    contentTextBox.Text = "";
                    reminderTextBox.Text = "";
                    keyTextBox.Text = "";
                }
            }
            else if (UserAnswer.Answer.time == 3)
            {
                item = clue3;
                clueComboBox.SelectedIndex = 2;
                if (NowMission.Task.Classes != "" && NowMission.Task.Classes != null)
                {
                    if (NowMission.Task.Classes == "悬疑")
                    {
                        themeComboBox.SelectedIndex = 0;
                    }
                    else if (NowMission.Task.Classes == "推理")
                    {
                        themeComboBox.SelectedIndex = 1;
                    }
                    else if (NowMission.Task.Classes == "日常")
                    {
                        themeComboBox.SelectedIndex = 2;
                    }
                    else if (NowMission.Task.Classes == "其他")
                    {
                        themeComboBox.SelectedIndex = 3;
                    }
                }
                else
                {
                    themeComboBox.SelectedIndex = 0;
                }
                if (NowMission.Task.Addr != "" && NowMission.Task.Addr != null)
                {
                    if (NowMission.Task.Addr == "校园")
                    {
                        backgroundComboBox.SelectedIndex = 0;
                    }
                    else if (NowMission.Task.Addr == "密室")
                    {
                        backgroundComboBox.SelectedIndex = 1;
                    }
                    else if (NowMission.Task.Addr == "街道")
                    {
                        backgroundComboBox.SelectedIndex = 2;
                    }
                    else if (NowMission.Task.Addr == "其他")
                    {
                        backgroundComboBox.SelectedIndex = 3;
                    }
                }
                else
                {
                    backgroundComboBox.SelectedIndex = 0;
                }
                if (NowMission.Task.Content3 != ""&& NowMission.Task.Content3 !=null)
                {
                    contentTextBox.Text = NowMission.Task.Content3;
                    reminderTextBox.Text = NowMission.Task.Tips3;
                    keyTextBox.Text = NowMission.Task.Answer3;
                }
                else
                {
                    contentTextBox.Text = "";
                    reminderTextBox.Text = "";
                    keyTextBox.Text = "";
                }
            }

        }


        public async void addAsync()
        {
            if (NowMission.Task.Img1 != null && NowMission.Task.Img1 != "")
            {
                add.Visibility = Visibility.Collapsed;
                add1.Visibility = Visibility.Visible;
                picture1.Visibility = Visibility.Visible;
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
                    picture1.Source = bi;
                }

            }
            if (NowMission.Task.Img2 != null && NowMission.Task.Img2 != "")
            {
                add1.Visibility = Visibility.Collapsed;
                add2.Visibility = Visibility.Visible;
                picture2.Visibility = Visibility.Visible;
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
                    picture2.Source = bi;
                }

            }
            if (NowMission.Task.Img3 != null && NowMission.Task.Img3 != "")
            {
                add2.Visibility = Visibility.Collapsed;
                picture3.Visibility = Visibility.Visible;
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
                    picture3.Source = bi;
                }
            }

          
        }

        private async void upload_Click(System.Object sender, RoutedEventArgs e)
        {
            ButtonPlayer.MusicPlayer.Play();
            var dialog = new ContentDialog()
            {
                Title = "消息提示",
                Content = "发布后将无法更改，你确认要发布吗?",
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消",
                FullSizeDesired = false,
            };

            dialog.PrimaryButtonClick += async (_s, _e) =>
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    if (NowMission.Task.Classes == "" || NowMission.Task.Classes == null)
                    {
                        NowMission.Task.Classes = themeComboBox.SelectionBoxItem.ToString();
                    }
                    if (NowMission.Task.Addr == "" || NowMission.Task.Addr == null)
                    {
                        NowMission.Task.Addr = backgroundComboBox.SelectionBoxItem.ToString();
                    }
                    if (item == clue1)
                    {
                        NowMission.Task.Content1 = contentTextBox.Text;
                        NowMission.Task.Tips1 = reminderTextBox.Text;
                        if (keyTextBox.Text != "")
                        {
                            NowMission.Task.Answer1 = keyTextBox.Text;
                        }
                        
                    }
                    else if (item == clue2)
                    {
                        NowMission.Task.Content2 = contentTextBox.Text;
                        NowMission.Task.Tips2 = reminderTextBox.Text;
                        if (keyTextBox.Text != "")
                        {
                            NowMission.Task.Answer2 = keyTextBox.Text;
                        }
                    }
                    else if (item == clue3)
                    {
                        NowMission.Task.Content3 = contentTextBox.Text;
                        NowMission.Task.Tips3 = reminderTextBox.Text;
                        if (keyTextBox.Text != "")
                        {
                            NowMission.Task.Answer3 = keyTextBox.Text;
                        }
                    }
                    TimeSpan ts = new TimeSpan(15000000);
                    client.Timeout = ts;
                    try
                    {
                    //    var kvp = new List<KeyValuePair<string, string>>
                    //{
                    //    new KeyValuePair<string,string>("id", NewUser.ID),
                    //    new KeyValuePair<string,string>("title", NowMission.Task.Title),
                    //    new KeyValuePair<string,string>("classes",  NowMission.Task.Classes),
                    //    new KeyValuePair<string,string>("addr",  NowMission.Task.Addr),
                    //    new KeyValuePair<string,string>("content1",NowMission.Task.Content1),
                    //    new KeyValuePair<string,string>("content2",NowMission.Task.Content2),
                    //    new KeyValuePair<string,string>("content3", NowMission.Task.Content3),
                    //    new KeyValuePair<string,string>("tips1",NowMission.Task.Tips1),
                    //    new KeyValuePair<string,string>("tips2",NowMission.Task.Tips2),
                    //    new KeyValuePair<string,string>("tips3",NowMission.Task.Tips3),
                    //    new KeyValuePair<string,string>("answer1",NowMission.Task.Answer1),
                    //    new KeyValuePair<string,string>("answer2",NowMission.Task.Answer2),
                    //    new KeyValuePair<string,string>("answer3",NowMission.Task.Answer3),
                    //    new KeyValuePair<string,string>("action", "upload"),
                    //};
                        string str = "action=" + "upload" + "&";
                        str += "id=" + NewUser.ID + "&";
                        str += "title=" + NowMission.Task.Title + "&";
                        str += "classes=" + NowMission.Task.Classes + "&";
                        str += "addr=" + NowMission.Task.Addr + "&";
                        str += "content1=" + NowMission.Task.Content1 + "&";
                        str += "content2=" + NowMission.Task.Content2 + "&";
                        str += "content3=" + NowMission.Task.Content3 + "&";
                        str += "tips1=" + NowMission.Task.Tips1 + "&";
                        str += "tips2=" + NowMission.Task.Tips2 + "&";
                        str += "tips3=" + NowMission.Task.Tips3 + "&";
                        str += "answer1=" + NowMission.Task.Answer1 + "&";
                        str += "answer2=" + NowMission.Task.Answer2 + "&";
                        str += "answer3=" + NowMission.Task.Answer3 + "&";
                        

                        System.Net.Http.StringContent content = new StringContent(str, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                        System.Net.Http.HttpResponseMessage response = await client.PostAsync("http://qwq.itbears.club/hunter.php", content);
                        if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            if (responseBody == "success")
                            {
                                if (NowMission.Task.Img1 != "")
                                {
                                    str = "action=" + "uploadimg" + "&";
                                    str += "title=" + NowMission.Task.Title + "&";
                                    str += "imgnum=" + "one" + "&";
                                    str += "img=" + System.Net.WebUtility.UrlEncode(NowMission.Task.Img1);
                                    content = new StringContent(str, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                                    response = await client.PostAsync("http://qwq.itbears.club/hunter.php", content);
                                    if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                                    {
                                        responseBody = await response.Content.ReadAsStringAsync();
                                        if (responseBody == "success")
                                        {
                                            if (NowMission.Task.Img2 != "")
                                            {
                                                str = "action=" + "uploadimg" + "&";
                                                str += "title=" + NowMission.Task.Title + "&";
                                                str += "imgnum=" + "two" + "&";
                                                str += "img=" + System.Net.WebUtility.UrlEncode(NowMission.Task.Img2);
                                                content = new StringContent(str, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                                                response = await client.PostAsync("http://qwq.itbears.club/hunter.php", content);
                                                if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                                                {
                                                    responseBody = await response.Content.ReadAsStringAsync();
                                                    if (responseBody == "success")
                                                    {
                                                        if (NowMission.Task.Img3 != "")
                                                        {
                                                            str = "action=" + "uploadimg" + "&";
                                                            str += "title=" + NowMission.Task.Title + "&";
                                                            str += "imgnum=" + "three" + "&";
                                                            str += "img=" + System.Net.WebUtility.UrlEncode(NowMission.Task.Img3);
                                                            content = new StringContent(str, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
                                                            response = await client.PostAsync("http://qwq.itbears.club/hunter.php", content);
                                                            if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                                                            {
                                                                responseBody = await response.Content.ReadAsStringAsync();
                                                                if (responseBody == "success")
                                                                {
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                               
                                                        var msgDialog = new Windows.UI.Popups.MessageDialog("发布成功") { Title = "提示" };
                                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { NowMission.Task.Title = ""; NowMission.Task.Addr = ""; NowMission.Task.Content1 = ""; NowMission.Task.Content2 = ""; NowMission.Task.Content3 = ""; NowMission.Task.Classes = ""; NowMission.Task.Answer1 = ""; NowMission.Task.Answer2 = ""; NowMission.Task.Answer3 = ""; NowMission.Task.Tips1 = ""; NowMission.Task.Tips2 = ""; NowMission.Task.Tips3 = ""; NowMission.Task.Title = ""; NowMission.Task.User = "";Frame.Navigate(typeof(Room.RoomPage)); Frame.BackStack.Clear();
                                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                                }));
                                await msgDialog.ShowAsync();
                            }
                            else
                            {
                                var msgDialog = new Windows.UI.Popups.MessageDialog("发布失败，该标题已存在，请更改标题后重新发布") { Title = "提示" };
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
            };
            await dialog.ShowAsync();
        }

        private async void Choose_Click(System.Object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker(); //打开文件选择器。
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");//过滤文件类型，目前只支持jpg, png,选择其他文件会报错。
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {

                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var image = new BitmapImage();

                image.SetSource(stream);
                picture1.Source = image;
                var streamData = await file.OpenReadAsync();
                var bytes = new byte[streamData.Size];
                using (var dataReader = new DataReader(streamData))
                {
                    await dataReader.LoadAsync((uint)streamData.Size);
                    dataReader.ReadBytes(bytes);
                }
                string inputString = System.Convert.ToBase64String(bytes);
                NowMission.Task.Img1 = inputString;
            }
            add1.Visibility = Visibility.Visible;
            add.Visibility = Visibility.Collapsed;
        }

        private async void Choose_Click1(System.Object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker(); //打开文件选择器。
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");//过滤文件类型，目前只支持jpg, png,选择其他文件会报错。
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {

                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var image = new BitmapImage();

                image.SetSource(stream);
                picture2.Source = image;
                var streamData = await file.OpenReadAsync();
                var bytes = new byte[streamData.Size];
                using (var dataReader = new DataReader(streamData))
                {
                    await dataReader.LoadAsync((uint)streamData.Size);
                    dataReader.ReadBytes(bytes);
                }
                string inputString = System.Convert.ToBase64String(bytes);
                NowMission.Task.Img2 = inputString;
            }
            add2.Visibility = Visibility.Visible;
            add1.Visibility = Visibility.Collapsed;
        }

     
        private async void Choose_Click2(System.Object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker(); //打开文件选择器。
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");//过滤文件类型，目前只支持jpg, png,选择其他文件会报错。
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {

                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var image = new BitmapImage();

                image.SetSource(stream);
                picture3.Source = image;
                var streamData = await file.OpenReadAsync();
                var bytes = new byte[streamData.Size];
                using (var dataReader = new DataReader(streamData))
                {
                    await dataReader.LoadAsync((uint)streamData.Size);
                    dataReader.ReadBytes(bytes);
                }
                string inputString = System.Convert.ToBase64String(bytes);
                NowMission.Task.Img3 = inputString;
            }
            add2.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// clueComboBox_DropDownClosed某线索被选中规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void clueComboBox_DropDownClosed(object sender, object e)
        {
            if (clue1.IsSelected)
            {
                if (item == clue2)
                {
                    NowMission.Task.Content2 = contentTextBox.Text;
                    NowMission.Task.Tips2 = reminderTextBox.Text;
                    if (keyTextBox.Text != "")
                    {
                        NowMission.Task.Answer2 = keyTextBox.Text;

                    }
                }
                else if (item == clue3)
                {
                    NowMission.Task.Content3 = contentTextBox.Text;
                    NowMission.Task.Tips3 = reminderTextBox.Text;
                    if (keyTextBox.Text != "")
                    {
                        NowMission.Task.Answer3 = keyTextBox.Text;
                    }
                }
                contentTextBox.IsEnabled = true;
                reminderTextBox.IsEnabled = true;
                keyTextBox.IsEnabled = true;
                contentTextBox.Text = NowMission.Task.Content1;
                reminderTextBox.Text = NowMission.Task.Tips1;
                keyTextBox.Text = NowMission.Task.Answer1;
                item=clueComboBox.SelectedItem;
                UserAnswer.Answer.time = 1;

            }
           else if(clue2.IsSelected)
            {
                if(item==clue1)
                {
                    NowMission.Task.Content1 = contentTextBox.Text;
                    NowMission.Task.Tips1 = reminderTextBox.Text;
                    if (keyTextBox.Text != "")
                    {
                        NowMission.Task.Answer1 = keyTextBox.Text;
                    }
                }
                else if(item==clue3)
                {
                    NowMission.Task.Content3 = contentTextBox.Text;
                    NowMission.Task.Tips3 = reminderTextBox.Text;
                    if (keyTextBox.Text != "")
                    {
                        NowMission.Task.Answer3 = keyTextBox.Text;
                    }
                }

                if (NowMission.Task.Content1 == "" || NowMission.Task.Tips1 == "" || NowMission.Task.Answer1 == "")
                {
                    contentTextBox.IsEnabled = false;
                    reminderTextBox.IsEnabled = false;
                    keyTextBox.IsEnabled = false;
                    var msgDialog = new Windows.UI.Popups.MessageDialog("需要完整填写线索一相关内容") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                    await msgDialog.ShowAsync();
                }
                else
                {
                    contentTextBox.IsEnabled = true;
                    reminderTextBox.IsEnabled = true;
                    keyTextBox.IsEnabled = true;
                    contentTextBox.Text = NowMission.Task.Content2;
                    reminderTextBox.Text = NowMission.Task.Tips2;
                    keyTextBox.Text = NowMission.Task.Answer2;
                    item = clueComboBox.SelectedItem;
                    UserAnswer.Answer.time = 2;
                }
            }
            else if (clue3.IsSelected)
            {
                if (item == clue1)
                {
                    NowMission.Task.Content1 = contentTextBox.Text;
                    NowMission.Task.Tips1 = reminderTextBox.Text;
                    if (keyTextBox.Text != "")
                    {
                        NowMission.Task.Answer1 = keyTextBox.Text;
                    }
                }
                else if (item == clue2)
                {
                    NowMission.Task.Content2 = contentTextBox.Text;
                    NowMission.Task.Tips2 = reminderTextBox.Text;
                    if (keyTextBox.Text != "")
                    {
                        NowMission.Task.Answer2 = keyTextBox.Text;
                    }
                }

                if (NowMission.Task.Content2 == "" || NowMission.Task.Tips2 == "" || NowMission.Task.Answer2 == "")
                {
                    contentTextBox.IsEnabled = false;
                    reminderTextBox.IsEnabled = false;
                    keyTextBox.IsEnabled = false;
                    var msgDialog = new Windows.UI.Popups.MessageDialog("需要完整填写线索二相关内容") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                    await msgDialog.ShowAsync();
                }
                else
                {
                    contentTextBox.IsEnabled = true;
                    reminderTextBox.IsEnabled = true;
                    keyTextBox.IsEnabled = true;
                    contentTextBox.Text = NowMission.Task.Content3;
                    reminderTextBox.Text = NowMission.Task.Tips3;
                    keyTextBox.Text = NowMission.Task.Answer3;
                    item = clueComboBox.SelectedItem;
                    UserAnswer.Answer.time = 3;
                }
            }
        }

        private async void scan_ClickAsync(object sender, RoutedEventArgs e)
        {
            var msgDialog = new Windows.UI.Popups.MessageDialog("使用二维码答案会导致任务各个阶段仅能使用二维码解答，确定继续吗？") { Title = "提示" };
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => {
                if(UserAnswer.Answer.time==1)
                {
                    NowMission.Task.Content1 = contentTextBox.Text;
                    NowMission.Task.Tips1 = reminderTextBox.Text;
                    NowMission.Task.Answer1 = "";
                }
                else if (UserAnswer.Answer.time == 2)
                {
                    NowMission.Task.Content2 = contentTextBox.Text;
                    NowMission.Task.Tips2 = reminderTextBox.Text;
                    NowMission.Task.Answer2 = "";
                }
                else if (UserAnswer.Answer.time == 3)
                {
                    NowMission.Task.Content3 = contentTextBox.Text;
                    NowMission.Task.Tips3 = reminderTextBox.Text;
                    NowMission.Task.Answer3 = "";
                }
                keyTextBox.Text = "";
                keyTextBox.Visibility = Visibility.Collapsed;
                UserAnswer.Answer.show = 0;
                Frame.Navigate(typeof(QrCode));
            }));
            msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("取消", uiCommand => { } ));
            await msgDialog.ShowAsync();
           
        }

        private async void keyTextBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            if (keyTextBox.Text == "")
            {
                scan.IsEnabled = true;
            }
            else
            {
                if (keyTextBox.Text.Length > 50)
                {
                    var msgDialog = new Windows.UI.Popups.MessageDialog("内容限制长度为50，请修改后重试") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { upload.IsEnabled = false; }));
                    await msgDialog.ShowAsync();
                }
                else
                {
                    upload.IsEnabled = true;
                }
                scan.IsEnabled = false;
            }
        }

        private void themeComboBox_DropDownClosed(object sender, object e)
        {
           NowMission.Task.Classes= themeComboBox.SelectionBoxItem.ToString();
        }

        private void backgroundComboBox_DropDownClosed(object sender, object e)
        {
            NowMission.Task.Addr = backgroundComboBox.SelectionBoxItem.ToString();
        }

        private void TitleTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NowMission.Task.Title = TitleTextbox.Text;
        }

        private async void contentTextBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            if (contentTextBox.Text.Length > 500)
            {
                var msgDialog = new Windows.UI.Popups.MessageDialog("内容限制长度为500，请修改后重试") { Title = "提示" };
                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { upload.IsEnabled = false; }));
                await msgDialog.ShowAsync();
            }
            else
            {
                upload.IsEnabled = true;
            }
        }

        private async void reminderTextBox_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            if (reminderTextBox.Text.Length > 50)
            {
                var msgDialog = new Windows.UI.Popups.MessageDialog("内容限制长度为50，请修改后重试") { Title = "提示" };
                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { upload.IsEnabled = false; }));
                await msgDialog.ShowAsync();
            }
            else
            {
                upload.IsEnabled = true;
            }
        }

        /// <summary>
        /// contentTextBox文本被改变对应操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>

    }
}
