using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
        private StorageFile storageFile;

        public object Console { get; private set; }

        public userMessage()
        {
            StorageFile storageFile = null;
            
            this.InitializeComponent();
        }

        private void userSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void userBackButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void submitWholeChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelWholeChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void SelectPicButton_Click(object sender, RoutedEventArgs e)
        {
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
                }
                else
                {
                    OutputTextBlock.Text = "没有选择图片";
                }
        }

        private void confirmPicButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newDickNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dickName.Text = newDickNameTextBox.Text;
        }

        private void tbTitle_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void submitWholeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
