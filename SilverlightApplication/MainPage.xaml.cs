using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace SilverlightApplication
{

	public partial class MainPage : UserControl
	{
		private bool IsVideoScrubberLocked { get; set; }
		private DispatcherTimer timer;
		private double unmutedVolume;

		public MainPage()
		{
			InitializeComponent();
			btnPlayPause.Checked += new RoutedEventHandler(btnPlayPause_Checked);
			btnPlayPause.Unchecked += new RoutedEventHandler(btnPlayPause_Unchecked);
			VideoElement.CurrentStateChanged += new RoutedEventHandler(VideoElement_CurrentStateChanged);
			sliderScrubber.MouseLeftButtonUp += new MouseButtonEventHandler(sliderScrubber_MouseLeftButtonUp);			
			sliderScrubber.MouseLeftButtonDown += new MouseButtonEventHandler(sliderScrubber_MouseLeftButtonDown);
			sliderVolume.ValueChanged += new RoutedPropertyChangedEventHandler<double>(sliderVolume_ValueChanged);
			btnMute.Checked += new RoutedEventHandler(btn_mute_Checked);
			btnMute.Unchecked += new RoutedEventHandler(btn_mute_Unchecked);

			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromMilliseconds(50);
			timer.Tick += new EventHandler(timer_Tick);
			
		}

		#region Video Player

		void btn_mute_Checked(object sender, RoutedEventArgs e)
		{
			unmutedVolume = VideoElement.Volume;
			VideoElement.Volume = 0;
			BitmapImage bitImageUnmuted = new BitmapImage();
			bitImageUnmuted.UriSource = new Uri("/SilverlightApplication;component/images/unmuted.png", UriKind.Relative);
			muteImg.Source = bitImageUnmuted;
		}

		void btn_mute_Unchecked(object sender, RoutedEventArgs e)
		{
			VideoElement.Volume = unmutedVolume;
			BitmapImage bitImageMute = new BitmapImage();
			bitImageMute.UriSource = new Uri("/SilverlightApplication;component/images/mute.png", UriKind.Relative);
			muteImg.Source = bitImageMute;
		}

		void sliderScrubber_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			IsVideoScrubberLocked = false;
			VideoElement.Position = TimeSpan.FromMilliseconds(VideoElement.NaturalDuration.TimeSpan.TotalMilliseconds * sliderScrubber.Value);
		}

		void sliderScrubber_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			IsVideoScrubberLocked = true;
		}

		void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			VideoElement.Volume = sliderVolume.Value;
		}

		void timer_Tick(object sender, EventArgs e)
		{
			if (VideoElement.NaturalDuration.TimeSpan.TotalSeconds > 0 && !IsVideoScrubberLocked)
			{
				sliderScrubber.Value = VideoElement.Position.TotalSeconds / VideoElement.NaturalDuration.TimeSpan.TotalSeconds;
				txtVideoPosition.Text = string.Format("{0:00}:{1:00}", VideoElement.Position.Minutes, VideoElement.Position.Seconds);
				sliderScrubber.Value = VideoElement.Position.TotalSeconds / VideoElement.NaturalDuration.TimeSpan.TotalSeconds;
			}
		}

		void VideoElement_CurrentStateChanged(object sender, RoutedEventArgs e)
		{
			if (VideoElement.CurrentState == MediaElementState.Playing)
			{
				timer.Start();
			}
			else
			{
				timer.Stop();
			}
		}

		void btnPlayPause_Unchecked(object sender, RoutedEventArgs e)
		{			
			VideoElement.Play();
			BitmapImage bitImagePlay = new BitmapImage();
			bitImagePlay.UriSource = new Uri("/SilverlightApplication;component/images/play.png", UriKind.Relative);
			playPauseImg.Source = bitImagePlay;
		}

		private void btnPlayPause_Checked(object sender, RoutedEventArgs e)
		{
			VideoElement.Pause();
			BitmapImage bitImagePause = new BitmapImage();
			bitImagePause.UriSource = new Uri("/SilverlightApplication;component/images/pause.png", UriKind.Relative);
			playPauseImg.Source = bitImagePause;
		}

		private void ElementMedia_Open(object sender, RoutedEventArgs e)
		{
			sliderVolume.Value = VideoElement.Volume;
		}

		#endregion

		// After the Frame navigates, ensure the HyperlinkButton representing the current page is selected
		private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
		{
			foreach (UIElement child in LinksStackPanel.Children)
			{
				HyperlinkButton hb = child as HyperlinkButton;
				if (hb != null && hb.NavigateUri != null)
				{
					if (ContentFrame.UriMapper.MapUri(e.Uri).ToString().Equals(ContentFrame.UriMapper.MapUri(hb.NavigateUri).ToString()))
					{
						VisualStateManager.GoToState(hb, "ActiveLink", true);
					}
					else
					{
						VisualStateManager.GoToState(hb, "InactiveLink", true);
					}
				}
			}
		}

		// If an error occurs during navigation, show an error window
		private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			e.Handled = true;
			ChildWindow errorWin = new ErrorWindow(e.Uri);
			errorWin.Show();
		}

	}
}