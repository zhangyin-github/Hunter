using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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


        private void changeDickName_Tapped(object sender, TappedRoutedEventArgs e)
        {
            dickName.Text = "君君";
        }

        private void submitWholeChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelWholeChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void SelectPicButton_Click(object sender, RoutedEventArgs e)
        {
            //定义文件选取器对象

            FileOpenPicker openPicker = new FileOpenPicker();

            //设置文件显示方式为缩略图

            openPicker.ViewMode = PickerViewMode.Thumbnail;

            //设置文件选取器的起始位置为图片库

            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            //设置在文件选取器中显示的文件类型

            openPicker.FileTypeFilter.Add(".jpg");

            openPicker.FileTypeFilter.Add(".jpeg");

            openPicker.FileTypeFilter.Add(".png");

            storageFile = await openPicker.PickSingleFileAsync();

            if (storageFile != null)

            {

                OutputTextBlock.Text = "您已选择名为" + storageFile.Name + "的文件";

            }

            else

            {

                OutputTextBlock.Text = "没有选择图片";

            }
        }

        private void confirmPicButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
