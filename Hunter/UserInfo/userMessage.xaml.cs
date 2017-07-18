using Hunter.Models;
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
        private score scoreValue;
        public string oldnickName;
        public object Console { get; private set; }
        public bool newnickNameCanBeCahenged = false;

        public userMessage()
        {
            StorageFile storageFile = null;

            scoreValue = new score();
            scoreValue.Max = 100;
            scoreValue.Min = 0;
            scoreValue.CurrentValue = 10;
            


            this.InitializeComponent();

            List<solve> difficulties = new List<solve>();
            difficulties.Add(new solve() { difficulty = "全部难度题目" , difficultyScores = "解谜数目（成功/失败）：16/20" });
            difficulties.Add(new solve() { difficulty = "五星难度题目" , difficultyScores = "解谜数目（成功/失败）：2/7" });
            difficulties.Add(new solve() { difficulty = "四星难度题目" , difficultyScores = "解谜数目（成功/失败）：6/8" });
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

            if (newnickNameTextBox.Text == oldnickName)
            {
                newnickNameTitleTextBlock.Text = "昵称已存在请重新输入！";
                checkTextBlockISWrong.Visibility = Visibility.Visible;
                checkTextBlockIsOk.Visibility = Visibility.Collapsed;
                newnickNameCanBeCahenged = false;
                return;
            }
            
            else
            {
                newnickNameTitleTextBlock.Text = "新昵称可用";
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
        private void changenickNameFlyout_Closed(object sender, object e)
        {
            if(newnickNameCanBeCahenged)
            {
                nickName.Text = newnickNameTextBox.Text;
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

        private void psTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            BindingExpression be = tb.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void submitWholeChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelWholeChange_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void submitWholeButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog()
            {
                Title = "消息提示",
                Content = "确认提交信息修改么?",
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消",
                FullSizeDesired = false,
            };

            dialog.PrimaryButtonClick += (_s, _e) => { };
            await dialog.ShowAsync();
        }

        
    }
}
