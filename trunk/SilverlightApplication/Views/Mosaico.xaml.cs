using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Data;

namespace SilverlightApplication
{
	public partial class Mosaico : Page
	{
		public WebServiceLives.WebServiceClient cliente;
		public Mosaico()
		{
			InitializeComponent();

			cliente = new WebServiceLives.WebServiceClient();
			cliente.obterTodasCompleted += new EventHandler<WebServiceLives.obterTodasCompletedEventArgs>(cliente_obterTodasCompleted);
			cliente.obterTodasAsync();
		}

		private void cliente_obterTodasCompleted(object sender, WebServiceLives.obterTodasCompletedEventArgs e)
		{
			if (e.Error != null)
				MessageBox.Show(e.Error.Message);
			else
			{
				var listaCategorias = e.Result;
				cliente = new WebServiceLives.WebServiceClient();
				cliente.obtemPlayListCompleted += new EventHandler<WebServiceLives.obtemPlayListCompletedEventArgs>(cliente_obtemPlayListCompleted);
				List<int> idsCategorias = (from categoria in listaCategorias select categoria.id).ToList<int>();
				cliente.obtemPlayListAsync(idsCategorias);

				cliente.obtemPlayListCompleted += new EventHandler<WebServiceLives.obtemPlayListCompletedEventArgs>(cliente_obtemPlayListMosaicoCompleted1);
				cliente.obtemPlayListAsync(new List<int>() { 1 });

				cliente.obtemPlayListCompleted += new EventHandler<WebServiceLives.obtemPlayListCompletedEventArgs>(cliente_obtemPlayListMosaicoCompleted2);
				cliente.obtemPlayListAsync(new List<int>() { 2 });

				cliente.obtemPlayListCompleted += new EventHandler<WebServiceLives.obtemPlayListCompletedEventArgs>(cliente_obtemPlayListMosaicoCompleted3);
				cliente.obtemPlayListAsync(new List<int>() { 3 });

				cliente.obtemPlayListCompleted += new EventHandler<WebServiceLives.obtemPlayListCompletedEventArgs>(cliente_obtemPlayListMosaicoCompleted4);
				cliente.obtemPlayListAsync(new List<int>() { 4 });

				cliente.obtemPlayListCompleted += new EventHandler<WebServiceLives.obtemPlayListCompletedEventArgs>(cliente_obtemPlayListMosaicoCompleted5);
				cliente.obtemPlayListAsync(new List<int>() { 5 });

				cliente.obtemPlayListCompleted += new EventHandler<WebServiceLives.obtemPlayListCompletedEventArgs>(cliente_obtemPlayListMosaicoCompleted6);
				cliente.obtemPlayListAsync(new List<int>() { 6 });

			}
		}

		private void cliente_obtemPlayListCompleted(object sender, SilverlightApplication.WebServiceLives.obtemPlayListCompletedEventArgs e)
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

		private void cliente_obtemPlayListMosaicoCompleted1(object sender, SilverlightApplication.WebServiceLives.obtemPlayListCompletedEventArgs e)
		{
			if (e.Result == null || e.Result.Length == 0)
			{
				MessageBox.Show("Não existem Vídeos");
			}
			else
			{
				try
				{
					VideoUm.Source = e.Result;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.StackTrace);
				}
			}
		}

		private void cliente_obtemPlayListMosaicoCompleted2(object sender, SilverlightApplication.WebServiceLives.obtemPlayListCompletedEventArgs e)
		{
			if (e.Result == null || e.Result.Length == 0)
			{
				MessageBox.Show("Não existem Vídeos");
			}
			else
			{
				try
				{
					VideoDois.Source = e.Result;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.StackTrace);
				}
			}
		}

		private void cliente_obtemPlayListMosaicoCompleted3(object sender, SilverlightApplication.WebServiceLives.obtemPlayListCompletedEventArgs e)
		{
			if (e.Result == null || e.Result.Length == 0)
			{
				MessageBox.Show("Não existem Vídeos");
			}
			else
			{
				try
				{
					VideoTres.Source = e.Result;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.StackTrace);
				}
			}
		}

		private void cliente_obtemPlayListMosaicoCompleted4(object sender, SilverlightApplication.WebServiceLives.obtemPlayListCompletedEventArgs e)
		{
			if (e.Result == null || e.Result.Length == 0)
			{
				MessageBox.Show("Não existem Vídeos");
			}
			else
			{
				try
				{
					VideoQuatro.Source = e.Result;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.StackTrace);
				}
			}
		}

		private void cliente_obtemPlayListMosaicoCompleted5(object sender, SilverlightApplication.WebServiceLives.obtemPlayListCompletedEventArgs e)
		{
			if (e.Result == null || e.Result.Length == 0)
			{
				MessageBox.Show("Não existem Vídeos");
			}
			else
			{
				try
				{
					VideoCinco.Source = e.Result;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.StackTrace);
				}
			}
		}


		void cliente_obtemPlayListMosaicoCompleted6(object sender, WebServiceLives.obtemPlayListCompletedEventArgs e)
		{
			if (e.Result == null || e.Result.Length == 0)
			{
				MessageBox.Show("Não existem Vídeos");
			}
			else
			{
				try
				{
					VideoSeis.Source = e.Result;

				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.StackTrace);
				}
			}
		}



		private void btnRemoveFiltro_Click(object sender, RoutedEventArgs e)
		{
			//recarregaValoresIniciaisPagina();

		}

		//private void recarregaValoresIniciaisPagina()
		//{

		//    cliente.obterTodasCompleted += new EventHandler<WebServiceLives.obterTodasCompletedEventArgs>(cliente_obterTodasCompleted);
		//    cliente.obterTodasAsync();

		//    cliente.obtemVideosAprovadosCompleted += new EventHandler<WebServiceLives.obtemVideosAprovadosCompletedEventArgs>(cliente_obtemVideosAprovadosCompleted);
		//    cliente.obtemVideosAprovadosAsync();
		//}
		//private void cliente_obtemTodasSubcatCategoriaCompleted(object sender, SilverlightApplication.WebServiceLives.obtemTodasSubcatCategoriaCompletedEventArgs e)
		//{
		//    if (e.Error != null)
		//        MessageBox.Show(e.Error.Message);
		//    else
		//    {
		//        var subcat1 = from sc in e.Result where sc.Categoria.id == 1 && !string.IsNullOrWhiteSpace(sc.nome) orderby sc.nome select sc;
		//        cliente.obtemTodasSubcatCategoriaAsync(1);
		//        var subcat2 = from sc in e.Result where sc.Categoria.id == 1 && !string.IsNullOrWhiteSpace(sc.nome) orderby sc.nome select sc;
		//        cliente.obtemTodasSubcatCategoriaAsync(2);
		//        var subcat3 = from sc in e.Result where sc.Categoria.id == 1 && !string.IsNullOrWhiteSpace(sc.nome) orderby sc.nome select sc;
		//        cliente.obtemTodasSubcatCategoriaAsync(3);
		//        var subcat4 = from sc in e.Result where sc.Categoria.id == 1 && !string.IsNullOrWhiteSpace(sc.nome) orderby sc.nome select sc;
		//        cliente.obtemTodasSubcatCategoriaAsync(4);
		//        var subcat5 = from sc in e.Result where sc.Categoria.id == 1 && !string.IsNullOrWhiteSpace(sc.nome) orderby sc.nome select sc;
		//        cliente.obtemTodasSubcatCategoriaAsync(5);
		//        var subcat6 = from sc in e.Result where sc.Categoria.id == 1 && !string.IsNullOrWhiteSpace(sc.nome) orderby sc.nome select sc;
		//        cliente.obtemTodasSubcatCategoriaAsync(6);
		//        SubcatUm.DataContext = subcat1;
		//        SubcatDois.DataContext = subcat2;
		//        SubcatTres.DataContext = subcat3;
		//        SubcatQuatro.DataContext = subcat4;
		//        SubcatCinco.DataContext = subcat5;
		//        SubcatSeis.DataContext = subcat6;
		//    }

		//}
		// Executes when the user navigates to this page.
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}