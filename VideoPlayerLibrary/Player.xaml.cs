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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace Player
{
	public partial class Video : UserControl
	{
		private bool IsVideoScrubberLocked { get; set; }
		private DispatcherTimer timer;
		private double unmutedVolume;

		public string Source
		{
			get
			{
				return VideoElement.Source.ToString();
			}

			set
			{
				VideoElement.Source = new Uri(value, UriKind.Absolute);
			}
		}

		public bool HasControls
		{
			get
			{
				if (Controls.Visibility == Visibility.Visible)
				{
					return true;
				}

				return false;
			}

			set
			{
				Controls.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		public bool AutoPlay
		{
			get
			{
				return VideoElement.AutoPlay;
			}

			set
			{
				VideoElement.AutoPlay = value;
			}
		}

		public bool Mute
		{
			get
			{
				return VideoElement.IsMuted;
			}

			set
			{
				VideoElement.IsMuted = value;
			}
		}

		public Video()
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

		#region Video Video

		void btn_mute_Checked(object sender, RoutedEventArgs e)
		{
			unmutedVolume = VideoElement.Volume;
			VideoElement.Volume = 0;
			BitmapImage bitImageUnmuted = new BitmapImage();
			bitImageUnmuted.UriSource = new Uri("/VideoPlayerLibrary;component/images/unmuted.png", UriKind.Relative);
			muteImg.Source = bitImageUnmuted;
		}

		void btn_mute_Unchecked(object sender, RoutedEventArgs e)
		{
			VideoElement.Volume = unmutedVolume;
			BitmapImage bitImageMute = new BitmapImage();
			bitImageMute.UriSource = new Uri("/VideoPlayerLibrary;component/images/mute.png", UriKind.Relative);
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
			bitImagePlay.UriSource = new Uri("/VideoPlayerLibrary;component/images/pause.png", UriKind.Relative);
			playPauseImg.Source = bitImagePlay;
		}

		private void btnPlayPause_Checked(object sender, RoutedEventArgs e)
		{
			VideoElement.Pause();
			BitmapImage bitImagePause = new BitmapImage();
			bitImagePause.UriSource = new Uri("/VideoPlayerLibrary;component/images/play.png", UriKind.Relative);
			playPauseImg.Source = bitImagePause;
		}

		private void ElementMedia_Open(object sender, RoutedEventArgs e)
		{
			sliderVolume.Value = VideoElement.Volume;
			sliderScrubber.Value = 0.5;
			sliderScrubber_MouseLeftButtonUp(null, null);
			VideoElement.Position = TimeSpan.Zero;
		}

		#endregion

		private void VideoElement_MediaEnded(object sender, RoutedEventArgs e)
		{
			VideoElement.Source = new Uri(VideoElement.Source.ToString());

		}

	}
}
