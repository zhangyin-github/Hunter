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
using Windows.Storage;
using Hunter.Models;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Hunter.User_Upload
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class User_Upload : Page
    {
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

        /// <summary>
        /// 线索能否被更改标记
        /// </summary>
        public bool reminder1TextCanBeChange = false;
        public bool reminder2TextCanBeChange = false;
        public bool reminder3TextCanBeChange = false;
        /// <summary>
        /// 线索是否已经被更改标记
        /// </summary>
        public bool reminder1TextHasBeenChanged = false;
        public bool reminder2TextHasBeenChanged = false;
        public bool reminder3TextHasBeenChanged = false;
        /// <summary>
        /// 提示能否被更改标记
        /// </summary>
        public bool clue1TextCanBeChange = false;
        public bool clue2TextCanBeChange = false;
        public bool clue3TextCanBeChange = false;
        /// <summary>
        /// 提示是否已经被更改标记
        /// </summary>
        public bool clue1TextHasBeenChanged = false;
        public bool clue2TextHasBeenChanged = false;
        public bool clue3TextHasBeenChanged = false;
        /// <summary>
        /// 答案能否被更改标记
        /// </summary>
        public bool key1TextCanBeChange = false;
        public bool key2TextCanBeChange = false;
        public bool key3TextCanBeChange = false;
        /// <summary>
        /// 答案是否已经被更改标记
        /// </summary>
        public bool key1TextHasBeenChanged = false;
        public bool key2TextHasBeenChanged = false;
        public bool key3TextHasBeenChanged = false;

        public Missons NewMisson;
        public User_Upload()
        {
            NewMisson = MissonInfo.getInstance();
            this.InitializeComponent();
        }

        private void add_Click(System.Object sender, RoutedEventArgs e)
        {

        }

        private async void upload_Click(System.Object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog()
            {
                Title = "消息提示",
                Content = "发布后将无法更改，你确认要发布吗?",
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消",
                FullSizeDesired = false,
            };

            dialog.PrimaryButtonClick += (_s, _e) => { };
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
                contentTextBox.IsReadOnly = false;
                reminderTextBox.IsReadOnly = false;
                keyTextBox.IsReadOnly = false;
                clue1TextCanBeChange = true;
                reminder1TextCanBeChange = true;
                key1TextCanBeChange = true;

            }

            if (clue1TextHasBeenChanged&&reminder1TextHasBeenChanged&&key1TextHasBeenChanged)
            {
                if (clue2.IsSelected)
                {
                    contentTextBox.Text = "";
                    reminderTextBox.Text = "";
                    keyTextBox.Text = "";
                    clue2TextCanBeChange = true;
                    reminder2TextCanBeChange = true;
                    key2TextCanBeChange = true;
                    contentTextBox.IsReadOnly = false;
                    reminderTextBox.IsReadOnly = false;
                    keyTextBox.IsReadOnly = false;
                }
            }
            else
            {
                if (clue2.IsSelected|| clue3.IsSelected)
                {
                    var msgDialog = new Windows.UI.Popups.MessageDialog("请先填写线索一") { Title = "提示" };
                    msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                    await msgDialog.ShowAsync();
                }
            }
        

            if (clue1TextHasBeenChanged&&reminder2TextHasBeenChanged && key2TextHasBeenChanged)
            {
                if (clue2TextHasBeenChanged)
                {
                    if (clue3.IsSelected)
                    {
                        contentTextBox.Text = "";
                        reminderTextBox.Text = "";
                        keyTextBox.Text = "";
                        clue3TextCanBeChange = true;
                        reminder3TextCanBeChange = true;
                        key3TextCanBeChange = true;
                        contentTextBox.IsReadOnly = false;
                        reminderTextBox.IsReadOnly = false;
                        keyTextBox.IsReadOnly = false;
                    }
                }
                else
                {
                    if (clue3.IsSelected)
                    {
                        var msgDialog = new Windows.UI.Popups.MessageDialog("请先填写线索二") { Title = "提示" };
                        msgDialog.Commands.Add(new Windows.UI.Popups.UICommand("确定", uiCommand => { }));
                        await msgDialog.ShowAsync();
                    }
                }
            }
            

        }
        /// <summary>
        /// contentTextBox文本被改变对应操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void contentTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (clue1TextCanBeChange)
            {
                clueText[0] = contentTextBox.Text;
                clue1TextHasBeenChanged = true;
                clue1TextCanBeChange = false;

            }

            if (clue2TextCanBeChange)
            {

                clueText[1] = contentTextBox.Text;
                clue2TextHasBeenChanged = true;
                clue2TextCanBeChange = false;
            }

            if (clue3TextCanBeChange)
            {
                clueText[2] = contentTextBox.Text;
                clue3TextHasBeenChanged = true;
                clue3TextCanBeChange = false;
            }

        }

        private void reminderTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (reminder1TextCanBeChange)
            {
                reminderText[0] = reminderTextBox.Text;
                reminder1TextHasBeenChanged = true;
                reminder1TextCanBeChange = false;

            }

            if (reminder2TextCanBeChange)
            {

                reminderText[1] = reminderTextBox.Text;
                reminder2TextHasBeenChanged = true;
                reminder2TextCanBeChange = false;
            }

            if (reminder3TextCanBeChange)
            {
                reminderText[2] = reminderTextBox.Text;
                reminder3TextHasBeenChanged = true;
                reminder3TextCanBeChange = false;
            }
        }

        private void keyTextBox_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (key1TextCanBeChange)
            {
                keyText[0] = keyTextBox.Text;
                key1TextHasBeenChanged = true;
                key1TextCanBeChange = false;

            }

            if (key2TextCanBeChange)
            {

                keyText[1] = keyTextBox.Text;
                key2TextHasBeenChanged = true;
                key2TextCanBeChange = false;
            }

            if (key3TextCanBeChange)
            {
                keyText[2] = keyTextBox.Text;
                key3TextHasBeenChanged = true;
                key3TextCanBeChange = false;
            }
        }
    }
}
