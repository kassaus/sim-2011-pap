using System.Windows.Controls;
using System.Windows.Media;

namespace VideoViewer
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void play_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (videoPlayer.CurrentState == MediaElementState.Playing)
            {
                videoPlayer.Pause();
            }

            else if (videoPlayer.CurrentState == MediaElementState.Paused)
            {
                videoPlayer.Play();
            }

        }
    }
}
