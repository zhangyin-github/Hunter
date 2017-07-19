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
        public string clue1Text;
        public string clue2Text;
        public string clue3Text;
        /// <summary>
        /// 线索能否被更改标记
        /// </summary>
        public bool clue1TextCanBeChange=false;
        public bool clue2TextCanBeChange = false;
        public bool clue3TextCanBeChange = false;
        /// <summary>
        /// 线索是否已经被更改标记
        /// </summary>
        public bool clue1TextHasBeenChanged = false;
        public bool clue2TextHasBeenChanged = false;
        public bool clue3TextHasBeenChanged = false;

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
        private void clueComboBox_DropDownClosed(object sender, object e)
        {
            if (clue1.IsSelected)
            {
                contentTextBox.IsReadOnly = false;
                clue1TextCanBeChange = true;
            }

            if(clue1TextHasBeenChanged)
            {
                if (clue2.IsSelected)
                {
                    contentTextBox.Text = "";
                    clue2TextCanBeChange = true;
                    contentTextBox.IsReadOnly = false;
                }
            }

            if (clue1TextHasBeenChanged)
            {
                if (clue2TextHasBeenChanged)
                {
                    if (clue3.IsSelected)
                    {
                        contentTextBox.Text = "";
                        clue3TextCanBeChange = true;
                        contentTextBox.IsReadOnly = false;
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
                clue1Text = contentTextBox.Text;
                clue1TextHasBeenChanged = true;

            }

            if (clue2TextCanBeChange)
            {

                clue2Text = contentTextBox.Text;
                clue2TextHasBeenChanged = true;
            }

            if (clue3TextCanBeChange)
            {
                clue3Text = contentTextBox.Text;
                clue3TextHasBeenChanged = true;
            }

        }
    }
}
