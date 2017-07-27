using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Hunter.UI
{
    public class apage : Page
    {
        public enum AnimationType
        {
            /// <summary>
            /// 左侧滑入/出
            /// </summary>
            LeftToRight,
            /// <summary>
            /// 右侧滑入/出
            /// </summary>
            RightToLeft,
            /// <summary>
            /// 上向下滑入/出
            /// </summary>
            TopToBottom,
            /// <summary>
            /// 下向上滑入/出
            /// </summary>
            BottomToTop,
            /// <summary>
            /// 菜单专用动画
            /// </summary>
            Menu
        }
        private Popup Popup = new Popup();

        //位移动画
        private TranslateTransform tf;
        //缩放动画
        private ScaleTransform sf;
        private Storyboard sb;
        private DoubleAnimation DoubleAnimationX;
        private DoubleAnimation DoubleAnimationY;

        /// <summary>
        /// 关闭页动画
        /// </summary>
        public AnimationType CloseAnimation { get; set; }

        /// <summary>
        /// 动画类型
        /// </summary>
        /// <param name="t">0X，1Y</param>
        private void SetType(int t, double time)
        {
            switch (t)
            {
                case 0:
                    tf = new TranslateTransform();
                    sb = new Storyboard();
                    DoubleAnimationX = new DoubleAnimation();
                    DoubleAnimationX.EasingFunction = new PowerEase()
                    {

                        EasingMode = EasingMode.EaseInOut,
                    };
                    this.RenderTransform = tf;
                    //移除所有动画
                    sb.Children.Clear();

                    sb.Children.Add(DoubleAnimationX);
                    Storyboard.SetTarget(DoubleAnimationX, tf);
                    Storyboard.SetTargetProperty(DoubleAnimationX, "X");

                    DoubleAnimationX.Duration = TimeSpan.FromSeconds(time);
                    break;
                case 1:
                    tf = new TranslateTransform();
                    sb = new Storyboard();
                    DoubleAnimationY = new DoubleAnimation();
                    DoubleAnimationY.EasingFunction = new PowerEase()
                    {

                        EasingMode = EasingMode.EaseInOut,
                    };
                    this.RenderTransform = tf;
                    //移除所有动画
                    sb.Children.Clear();

                    sb.Children.Add(DoubleAnimationY);
                    Storyboard.SetTarget(DoubleAnimationY, tf);
                    Storyboard.SetTargetProperty(DoubleAnimationY, "Y");

                    DoubleAnimationY.Duration = TimeSpan.FromSeconds(time);
                    break;
                case 2:
                    sf = new ScaleTransform();
                    sb = new Storyboard();
                    DoubleAnimationY = new DoubleAnimation();
                    DoubleAnimationY.EasingFunction = new PowerEase()
                    {

                        EasingMode = EasingMode.EaseInOut,
                    };
                    sf.CenterX = 25;
                    sf.CenterY = 25;
                    this.RenderTransform = sf;
                    sb.Children.Clear();

                    sb.Children.Add(DoubleAnimationY);
                    Storyboard.SetTarget(DoubleAnimationY, sf);
                    Storyboard.SetTargetProperty(DoubleAnimationY, "ScaleX");
                    Storyboard.SetTargetProperty(DoubleAnimationY, "ScaleY");

                    DoubleAnimationY.Duration = TimeSpan.FromSeconds(time);
                    break;

            }

        }

        private void SetAnimationType(AnimationType t, double time)
        {
            switch (t)
            {
                case AnimationType.LeftToRight:

                    SetType(0, time);
                    break;
                case AnimationType.RightToLeft:

                    SetType(0, time);


                    break;
                case AnimationType.BottomToTop:

                    SetType(1, time);


                    break;
                case AnimationType.TopToBottom:

                    SetType(1, time);


                    break;
                case AnimationType.Menu:

                    SetType(2, time);


                    break;
            }
        }
        public apage()
        {
            this.Width = Window.Current.Bounds.Width;

            this.Height = Window.Current.Bounds.Height;

            Popup.Child = this;

            Popup.IsOpen = true;

            //隐藏界面
            tf = new TranslateTransform();
            tf.X = -(this.Width);
            this.RenderTransform = tf;





        }

        /// <summary>
        /// 显示页
        /// </summary>
        /// <param name="animationtype">动画方式</param>
        /// <param name="time">动画时长</param>
        public void Show(AnimationType animationtype = AnimationType.RightToLeft, double time = 0.3)
        {



            //设置动画方式
            SetAnimationType(animationtype, time);

            switch (animationtype)
            {
                case AnimationType.LeftToRight:

                    DoubleAnimationX.From = -(this.Width);
                    DoubleAnimationX.To = 0;

                    CloseAnimation = AnimationType.RightToLeft;
                    break;
                case AnimationType.RightToLeft:

                    DoubleAnimationX.From = (this.Width);
                    DoubleAnimationX.To = 0;

                    CloseAnimation = AnimationType.LeftToRight;
                    break;
                case AnimationType.BottomToTop:

                    DoubleAnimationY.From = (this.Height);
                    DoubleAnimationY.To = 0;

                    CloseAnimation = AnimationType.TopToBottom;
                    break;
                case AnimationType.TopToBottom:

                    DoubleAnimationY.From = (this.Width);
                    DoubleAnimationY.To = 0;

                    CloseAnimation = AnimationType.BottomToTop;
                    break;
                case AnimationType.Menu:

                    DoubleAnimationY.From = 0;
                    DoubleAnimationY.To = 1;

                    CloseAnimation = AnimationType.BottomToTop;
                    break;
            }





            sb.Begin();

        }
        public void Close(double time = 0.3)
        {


            //设置动画方式
            SetAnimationType(CloseAnimation, time);

            switch (CloseAnimation)
            {
                case AnimationType.LeftToRight:

                    DoubleAnimationX.From = 0;
                    DoubleAnimationX.To = this.Width;


                    break;
                case AnimationType.RightToLeft:

                    DoubleAnimationX.From = 0;
                    DoubleAnimationX.To = -(this.Width);


                    break;
                case AnimationType.TopToBottom:

                    DoubleAnimationY.From = 0;
                    DoubleAnimationY.To = (this.Height);


                    break;
                case AnimationType.BottomToTop:

                    DoubleAnimationY.From = 0;
                    DoubleAnimationY.To = (this.Height);


                    break;
            }


            sb.Begin();
        }


    }
}
