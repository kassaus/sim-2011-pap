using System;
using System.Collections.Generic;
using BO;
using DAL;

namespace BLL
{
    public class VideoBO
    {
        private VideoDO videosDataManager { get; set; }

        public VideoBO()
        {
            videosDataManager = new VideoDO();
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

            lista = videosDataManager.obterVideos(2);

            if (lista == null)
            {
                lista = new List<Video>();
            }

            return lista;
        }

        public List<Video> obterVideosPorAprovar()
        {
            List<Video> lista = null;

            lista = videosDataManager.obterVideos(1);

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

        public Video obterVideoMaisRecente(int estado)
        {
            return videosDataManager.obterVideoMaisRecente(estado);
        }

        public List<Video> GetByUser(Guid idUser)
        {
			return videosDataManager.obterVideosPorUser(idUser);
        }

        #region // INSERT, DELETE, UPDATE

        public bool criarVideo(string descricao, Guid id_user, string url)
        {
            Video video = new Video();
            video.descricao = descricao;
            video.id_user = id_user;
            video.url = url;

            return videosDataManager.inserirVideo(video);
        }

        public bool modificaVideo(string descricao, Guid id_user, string url, int id_video)
        {
            Video video = new Video();
            video.descricao = descricao;
            video.id_user = id_user;
            video.url = url;
            video.id = id_video;

            return videosDataManager.modificaVideo(video);
        }

        public bool removeVideo(int id_video)
        {
            return videosDataManager.removerVideo(obterVideo(id_video));
        }


        #endregion
    }
}
