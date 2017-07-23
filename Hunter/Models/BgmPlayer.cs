using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Hunter.Models
{
    class BgmPlayer
    {
        public static MediaElement MusicPlayer;
        public static MediaElement getInstance()
        {
            if (MusicPlayer == null)
            {
                MusicPlayer = new MediaElement();
            }
            return MusicPlayer;
        }
    }

    class ButtonPlayer
    {
        public static MediaElement MusicPlayer;
        public static MediaElement getInstance()
        {
            if (MusicPlayer == null)
            {
                MusicPlayer = new MediaElement();
            }
            return MusicPlayer;
        }
    }

}
