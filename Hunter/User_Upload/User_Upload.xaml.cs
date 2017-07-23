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
        public string[] clueText = new string[3];
        
        /// <summary>
        /// 三条提示的内容保存
        /// </summary>
        public string[] reminderText = new string[3];

        /// <summary>
        /// 三条答案的内容保存
        /// </summary>
        public string[] keyText = new string[3];

        public object item;
        public Missons NewMisson;
        public User_Upload()
        {
            NewUser = userInfo.getInstance();
            NewMisson = MissonInfo.getInstance();
            this.InitializeComponent();
            themeComboBox.SelectedIndex = 0;
            backgroundComboBox.SelectedIndex = 0;
            clueComboBox.SelectedIndex = 0;
            item = clue1;
            for (int i=0;i<3;i++)
            {
                clueText[i] = "";
                reminderText[i] ="";
                keyText[i] = "";
            }
        }
        
        private void add_Click(System.Object sender, RoutedEventArgs e)
        {

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
                    if (item == clue1)
                    {
                        clueText[0] = contentTextBox.Text;
                        reminderText[0] = reminderTextBox.Text;
                        keyText[0] = keyTextBox.Text;
                    }
                    else if (item == clue2)
                    {
                        clueText[1] = contentTextBox.Text;
                        reminderText[1] = reminderTextBox.Text;
                        keyText[1] = keyTextBox.Text;
                    }
                    else if (item == clue3)
                    {
                        clueText[2] = contentTextBox.Text;
                        reminderText[2] = reminderTextBox.Text;
                        keyText[2] = keyTextBox.Text;
                    }
                    TimeSpan ts = new TimeSpan(15000000);
                    client.Timeout = ts;
                    try
                    {
                        var kvp = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string,string>("id", NewUser.ID),
                        new KeyValuePair<string,string>("title", TitleTextbox.Text),
                        new KeyValuePair<string,string>("classes", themeComboBox.SelectionBoxItem.ToString()),
                        new KeyValuePair<string,string>("addr", backgroundComboBox.SelectionBoxItem.ToString()),
                        new KeyValuePair<string,string>("content1",clueText[0]),
                        new KeyValuePair<string,string>("content2",clueText[1]),
                        new KeyValuePair<string,string>("content3", clueText[2]),
                        new KeyValuePair<string,string>("tips1",reminderText[0]),
                        new KeyValuePair<string,string>("tips2",reminderText[1]),
                        new KeyValuePair<string,string>("tips3",reminderText[2]),
                        new KeyValuePair<string,string>("answer1",keyText[0]),
                        new KeyValuePair<string,string>("answer2",keyText[1]),
                        new KeyValuePair<string,string>("answer3",keyText[2]),
                        new KeyValuePair<string,string>("action", "upload"),
                    };
                        System.Net.Http.HttpResponseMessage response = await client.PostAsync("http://qwq.itbears.club/hunter.php", new FormUrlEncodedContent(kvp));
                        if (response.EnsureSuccessStatusCode().StatusCode.ToString().ToLower() == "ok")
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            if (responseBody == "success")
                            {
                                var msgDialog = new Windows.UI.Popups.MessageDialog("发布成功") { Title = "提示" };
                                msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
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
                    clueText[1] = contentTextBox.Text;
                    reminderText[1] = reminderTextBox.Text;
                    keyText[1] = keyTextBox.Text;
                }
                else if (item == clue3)
                {
                    clueText[2] = contentTextBox.Text;
                    reminderText[2] = reminderTextBox.Text;
                    keyText[2] = keyTextBox.Text;
                }
                contentTextBox.IsEnabled = true;
                reminderTextBox.IsEnabled = true;
                keyTextBox.IsEnabled = true;
                contentTextBox.Text = clueText[0];
                reminderTextBox.Text = reminderText[0];
                keyTextBox.Text = keyText[0];
                item=clueComboBox.SelectedItem;

            }
           else if(clue2.IsSelected)
            {
                if(item==clue1)
                {
                    clueText[0] = contentTextBox.Text;
                    reminderText[0] = reminderTextBox.Text;
                    keyText[0] = keyTextBox.Text;
                }
                else if(item==clue3)
                {
                    clueText[2] = contentTextBox.Text;
                    reminderText[2] = reminderTextBox.Text;
                    keyText[2] = keyTextBox.Text;
                }

                if (clueText[0] == "" || reminderText[0] == "" || keyText[0] == "")
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
                    contentTextBox.Text = clueText[1];
                    reminderTextBox.Text = reminderText[1];
                    keyTextBox.Text = keyText[1];
                    item = clueComboBox.SelectedItem;
                }
            }
            else if (clue3.IsSelected)
            {
                if (item == clue1)
                {
                    clueText[0] = contentTextBox.Text;
                    reminderText[0] = reminderTextBox.Text;
                    keyText[0] = keyTextBox.Text;
                }
                else if (item == clue2)
                {
                    clueText[1] = contentTextBox.Text;
                    reminderText[1] = reminderTextBox.Text;
                    keyText[1] = keyTextBox.Text;
                }

                if (clueText[1] == "" || reminderText[1] == "" || keyText[1] == "")
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
                    contentTextBox.Text = clueText[2];
                    reminderTextBox.Text = reminderText[2];
                    keyTextBox.Text = keyText[2];
                    item = clueComboBox.SelectedItem;
                }
            }
        }
        /// <summary>
        /// contentTextBox文本被改变对应操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
       
    }
}
