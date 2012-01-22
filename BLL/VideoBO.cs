using System;
using System.Collections.Generic;
using BO;
using DAL;

namespace BLL
{
	public class VideoBO
	{
		private VideoDO videosDataManager { get; set; }
		private EstadoDO estadosDataManager { get; set; }
		private SubcategoriaDO subcategoriasDataManager { get; set; }

		public VideoBO()
		{
			videosDataManager = new VideoDO();
			estadosDataManager = new EstadoDO();
			subcategoriasDataManager = new SubcategoriaDO();
		}

		public List<Video> obterTodosVideos()
		{
			List<Video> lista = null;

			lista = videosDataManager.obterTodosVideos();

			if (lista == null)
			{
				lista = new List<Video>();
			}

			return lista;
		}

		public List<Video> obterVideosAprovados()
		{
			List<Video> lista = null;

			lista = videosDataManager.obterVideosPorEstado(2);

			if (lista == null)
			{
				lista = new List<Video>();
			}

			return lista;
		}

		public List<Video> obterVideosPorAprovar()
		{
			List<Video> lista = null;

			lista = videosDataManager.obterVideosPorEstado(1);

			if (lista == null)
			{
				lista = new List<Video>();
			}

			return lista;
		}

		public Video obterVideo(int id)
		{
			return videosDataManager.obterVideo(id);
		}

		public List<Video> obterVideosCategoria(int cat)
		{
			List<Video> lista = null;

			lista = videosDataManager.obterVideosCategoria(cat);

			if (lista == null)
			{
				lista = new List<Video>();
			}

			return lista;
		}

		public List<Video> obterAprovadosCategoria(int cat)
		{
			List<Video> lista = null;
			lista = videosDataManager.obterAprovadosCategoria(cat);

			if (lista == null)
			{
				lista = new List<Video>();
			}

			return lista;
		}

		public List<Video> obterVideosCategoriaUser(int cat, Guid idUser)
		{
			List<Video> lista = null;
			lista = videosDataManager.obterVideosCategoriaUser(cat, idUser);

			if (lista == null)
			{
				lista = new List<Video>();
			}

			return lista;
		}

		public List<Video> obterTodosVideosSubcategoria(int idSubcat)
		{
			List<Video> lista = null;
			lista = videosDataManager.obterVideosSubcategoria(idSubcat);

			if (lista == null)
			{
				lista = new List<Video>();
			}

			return lista;

		}

		public List<Video> obterVideosAprovadosUser(Guid idUser)
		{
			List<Video> lista = null;
			lista = videosDataManager.obterVideosAprovadosUser(idUser);

			if (lista == null)
			{
				lista = new List<Video>();
			}

			return lista;
		}

		public List<Video> obterVideosUser(Guid idUser)
		{
			List<Video> lista = null;
			lista = videosDataManager.obterVideosPorUser(idUser);

			if (lista == null)
			{
				lista = new List<Video>();
			}

			return lista;
		}

		public Video obterVideoMaisRecente(int estado)
		{
			return videosDataManager.obterVideoMaisRecente(estado);
		}

		public List<Video> GetByUser(Guid idUser)
		{
			return videosDataManager.obterVideosPorUser(idUser);
		}

		#region // INSERT, DELETE, UPDATE

		public bool criarVideo(string descricao, Guid id_user, string titulo, string url)
		{
			Video video = new Video();
			video.descricao = descricao;
			video.id_user = id_user;
			video.url = url;
			video.titulo = titulo;

			return videosDataManager.inserirVideo(video);
		}

		public bool modificaVideo(string descricao, string titulo, string url, int id_video)
		{
			Video video = obterVideo(id_video);

			if (descricao != "" && descricao != null)
			{
				video.descricao = descricao;
			}

			if (url != "" && url != null)
			{
				video.url = url;
			}

			if (titulo != "" && titulo != null)
			{
				video.titulo = titulo;
			}

			return videosDataManager.actualizaVideo(video);
		}

		public bool removeVideo(int id_video)
		{
			return reprova(id_video);
		}

		private bool modificaEstado(int id, int estado)
		{
			Video video = videosDataManager.obterVideo(id);
			video.Estado = estadosDataManager.getById(estado);

			return videosDataManager.actualizaVideo(video);
		}

		public bool aprovar(int id)
		{
			return modificaEstado(id, 2);
		}

		public bool reprova(int id)
		{
			return modificaEstado(id, 3);
		}

		public bool desaprova(int id)
		{
			return modificaEstado(id, 1);
		}

		public bool associaEtiqueta(int id_video, int subcat)
		{
			Subcategoria subcategoria = subcategoriasDataManager.obterSubCategoriaId(subcat);
			Video video = videosDataManager.obterVideo(id_video);

			bool ret = videosDataManager.inserirSubcategoriaVideo(video, subcategoria);

			actualizaRelacao(subcategoria, video);

			return ret;
		}

		public bool desassociaEtiqueta(int id_video, string subcat)
		{
			Subcategoria subcategoria = subcategoriasDataManager.obterSubCategoriaNome(subcat);
			Video video = videosDataManager.obterVideo(id_video);

			bool ret = videosDataManager.removerSubcategoriaVideo(video, subcategoria);

			actualizaRelacao(subcategoria, video);

			return ret;
		}

		private static void actualizaRelacao(Subcategoria subcategoria, Video video)
		{
			subcategoria.Videos.Clear();
			video.Subcategorias.Clear();
			DB.tabelas.AcceptAllChanges();
			subcategoria.Videos.Load();
			video.Subcategorias.Load();
		}

		#endregion
	}
}
