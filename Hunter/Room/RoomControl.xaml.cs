using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Hunter.Room
{
    public sealed partial class RoomControl : UserControl
    {
        public Room.RootObject Contact
        {
            get
            {
                return this.DataContext as RootObject;
            }
        }


        public RoomControl()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => Bindings.Update();
        }

        public ImageSource s;
        public async void show()
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
                s = bi;
            }
        }
    }
}
