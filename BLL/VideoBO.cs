using System;
using System.Collections.Generic;
using BO;
using DAL;
using System.Linq;

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

		public List<Video> obterTodosVideosPorCategorias(List<int> categorias)
		{
			List<Video> lista = new List<Video>();

			foreach (int cat in categorias)
			{
				lista.AddRange(obterAprovadosCategoria(cat));
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

		public List<Video> obterTodosVideosSubcategoriaUser(int idSubcat, Guid idUser)
		{
			List<Video> lista = null;
			lista = videosDataManager.obterVideosSubcategoriaUser(idSubcat, idUser);

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

		public List<Video> obterVideosPorAprovadosUser(Guid idUser)
		{
			List<Video> lista = null;
			lista = videosDataManager.obterVideosPorAprovadosUser(idUser);

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

		public Video obterVideoMaisRecenteUser(Guid idUser)
		{
			return videosDataManager.obterVideoMaisRecente(1);
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
			video.Estado = estadosDataManager.getById(1);

			return videosDataManager.inserirVideo(video);
		}

		public bool modificaVideo(string descricao, string titulo, string url, int id_video, List<Subcategoria> subcat)
		{

			Video video = obterVideo(id_video);
			if (video != null)
			{

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

				if (subcat == null)
				{
					subcat = new List<Subcategoria>();
				}

				List<Subcategoria> subCatsAdicionar = subcat.FindAll(i => !video.Subcategorias.Contains(i));
				List<Subcategoria> subCatsRemover = video.Subcategorias.Where(i => !subcat.Contains(i)).ToList<Subcategoria>();

				// subCatsAdicionar.ForEach(i => associaEtiqueta(i, video));
				foreach (Subcategoria subCat in subCatsAdicionar)
				{
					associaEtiqueta(subCat, video);
				}

				// subCatsRemover.ForEach(i => desassociaEtiqueta(i, video));
				foreach (Subcategoria subCat in subCatsRemover)
				{
					desassociaEtiqueta(subCat, video);
				}

				return videosDataManager.actualizaVideo(video);
			}
			else
			{
				return false;
			}
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

			return associaEtiqueta(subcategoria, video);
		}

		private bool associaEtiqueta(Subcategoria subcategoria, Video video)
		{
			bool ret = videosDataManager.inserirSubcategoriaVideo(video, subcategoria);

			actualizaRelacao(subcategoria, video);

			return ret;
		}

		public bool desassociaEtiqueta(int id_video, string subcat)
		{
			Subcategoria subcategoria = subcategoriasDataManager.obterSubCategoriaNome(subcat);
			Video video = videosDataManager.obterVideo(id_video);

			return desassociaEtiqueta(subcategoria, video);
		}

		private bool desassociaEtiqueta(Subcategoria subcategoria, Video video)
		{
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
