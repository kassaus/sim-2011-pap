using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using System;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Data;
using Player;

namespace SilverlightApplication
{
	public partial class Listagem : Page
	{
		public WebServiceLives.WebServiceClient cliente;

		public Listagem()
		{
			InitializeComponent();

			cliente = new WebServiceLives.WebServiceClient();

			cliente.obterTodasCompleted += new EventHandler<WebServiceLives.obterTodasCompletedEventArgs>(cliente_obterTodasCompleted);
			cliente.obterTodasAsync();

			cliente.obtemVideosAprovadosCompleted += new EventHandler<WebServiceLives.obtemVideosAprovadosCompletedEventArgs>(cliente_obtemVideosAprovadosCompleted);
			cliente.obtemVideosAprovadosAsync();
		}

		private void cliente_obterTodasCompleted(object sender, WebServiceLives.obterTodasCompletedEventArgs e)
		{
			if (e.Error != null)
				MessageBox.Show(e.Error.Message);
			else
			{
				var listaCategorias = e.Result;
				cliente = new WebServiceLives.WebServiceClient();
				Categorias.DataContext = e.Result;

				cliente.obtemPlayListCompleted += new EventHandler<WebServiceLives.obtemPlayListCompletedEventArgs>(cliente_obtemPlayListCompleted);

				List<int> idsCategorias = (from categoria in listaCategorias select categoria.id).ToList<int>();
				cliente.obtemPlayListAsync(idsCategorias);
			}
		}

		private void cliente_obtemVideosAprovadosCompleted(object sender, SilverlightApplication.WebServiceLives.obtemVideosAprovadosCompletedEventArgs e)
		{

			if (e.Result == null || e.Result.Count == 0)
			{
				MessageBox.Show("Não exitem videos");
			}
			else
			{
				PagedCollectionView pageView = new PagedCollectionView(e.Result);
				paginador.Source = pageView;
				paginador.PageSize = 3;
				ListaVideos.ItemsSource = pageView;
			}
		}
		void cliente_obtemPlayListCompleted(object sender, WebServiceLives.obtemPlayListCompletedEventArgs e)
		{
			if (e.Result == null || e.Result.Length == 0)
			{
				MessageBox.Show("Não existem Vídeos");
			}
			else
			{
				try
				{
					Video.Source = e.Result;

				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.StackTrace);
				}
			}
		}


		private void btnAtualizar_Click(object sender, RoutedEventArgs e)
		{
			List<int> IdCatsSelecionadas = new List<int>();
			List<CheckBox> itensSelecionados = null;
			itensSelecionados = obtemCheckDaCombo(Categorias);
			if (itensSelecionados != null)
			{
				foreach (CheckBox idCat in itensSelecionados)
				{
					IdCatsSelecionadas.Add(Convert.ToInt32(idCat.Tag));
				}

				cliente.obtemPlayListAsync(IdCatsSelecionadas);
				cliente.obtemTodosVideosPorCategoriasCompleted += new EventHandler<WebServiceLives.obtemTodosVideosPorCategoriasCompletedEventArgs>(cliente_obtemTodosVideosPorCategoriasCompleted);
				cliente.obtemTodosVideosPorCategoriasAsync(IdCatsSelecionadas);
			}


		}


		private void cliente_obtemTodosVideosPorCategoriasCompleted(object sender, SilverlightApplication.WebServiceLives.obtemTodosVideosPorCategoriasCompletedEventArgs e)
		{

			if (e.Error != null)
				MessageBox.Show(e.Error.Message);

			if (e.Result == null || e.Result.Count == 0)
			{
				recarregaValoresIniciaisPagina();
			}
			else
			{
				PagedCollectionView pageView = new PagedCollectionView(e.Result);
				paginador.Source = pageView;
				paginador.PageSize = 3;
				ListaVideos.ItemsSource = null;
				ListaVideos.ItemsSource = pageView;
			}

		}
		List<CheckBox> obtemCheckDaCombo(ComboBox name)
		{
			List<CheckBox> itensSelecionados = new List<CheckBox>();
			ComboBoxItem item = null;
			CheckBox itemCheckBox = null;
			for (int i = 0; i < 6; i++)
			{
				item = name.ItemContainerGenerator.ContainerFromIndex(i) as ComboBoxItem;
				itemCheckBox = FindFirstElementInVisualTree<CheckBox>(item);
				if (itemCheckBox.IsChecked == true)
				{
					itensSelecionados.Add(itemCheckBox);
				}

			}
			return itensSelecionados;
		}

		private T FindFirstElementInVisualTree<T>(DependencyObject parentElement)
		  where T : DependencyObject
		{
			var count = VisualTreeHelper.GetChildrenCount(parentElement);
			if (count == 0)
				return null;

			for (int i = 0; i < count; i++)
			{
				var child = VisualTreeHelper.GetChild(parentElement, i);
				if (child != null && child is T)
					return (T)child;
				else
				{
					var result = FindFirstElementInVisualTree<T>(child);
					if (result != null)
						return result;
				}
			}
			return null;
		}

		private void recarregaValoresIniciaisPagina()
		{
			cliente.obterTodasCompleted += new EventHandler<WebServiceLives.obterTodasCompletedEventArgs>(cliente_obterTodasCompleted);
			cliente.obterTodasAsync();

			cliente.obtemVideosAprovadosCompleted += new EventHandler<WebServiceLives.obtemVideosAprovadosCompletedEventArgs>(cliente_obtemVideosAprovadosCompleted);
			cliente.obtemVideosAprovadosAsync();
		}

		// Executes when the user navigates to this page.
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		private void VideoPreview_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Video videoPlayer = sender as Video;
			WebServiceLives.Video sourceVideo = videoPlayer.DataContext as WebServiceLives.Video;
			videoPlayer.Source = UrlConverter.Convert(sourceVideo.url);
		}

		private void btnRemoveFiltro_Click(object sender, RoutedEventArgs e)
		{
			recarregaValoresIniciaisPagina();

		}

		private void VideoPreview_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Video previewVideo = sender as Video;
			Video.Source = previewVideo.Source;
		}
	}

}