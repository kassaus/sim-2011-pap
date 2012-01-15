using System;
using System.Collections.Generic;
using System.Data.Objects;
using BO;
using System.Linq;

namespace DAL
{
    public class VideoDO
    {
        #region // SELECT

        public List<Video> obterTodosVideos()
        {
            List<Video> lista = null;

            try
            {
                lista = (from video in DB.tabelas.Video where video.Estado.id != 3 orderby video.Estado.id ascending select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public List<Video> obterVideos(int estado)
        {
            List<Video> lista = null;
            try
            {
                lista = (from video in DB.tabelas.Video where video.Estado.id == estado && video.Estado.id != 3 select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public Video obterVideo(int id)
        {
            Video aux = null;
            try
            {
                aux = (from video in DB.tabelas.Video where video.id == id && video.Estado.id != 3 select video).FirstOrDefault<Video>();
            }
            catch { }

            return aux;
        }

        public List<Video> obterVideosPorUser(Guid idUser)
        {
            List<Video> lista = null;
            try
            {
                lista = (from video in DB.tabelas.Video where video.id_user == idUser && video.Estado.id != 3 select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public List<Video> obterVideosCategoria(int cat)
        {
            List<Video> lista = null;

            try
            {
                lista = (from subCat in DB.tabelas.Subcategoria from video in DB.tabelas.Video where subCat.Categoria.id == cat && video.Subcategorias.Contains(subCat) && video.Estado.id != 3 select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public List<Video> obterAprovadosCategoria(int cat)
        {
            List<Video> lista = null;

            try
            {
                lista = (from video in DB.tabelas.Video from subCat in DB.tabelas.Subcategoria where video.Estado.id == 2 && subCat.Categoria.id == cat && video.Subcategorias.Contains(subCat) select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public List<Video> obterVideosCategoriaUser(int cat, Guid idUser)
        {
            List<Video> lista = null;

            try
            {
                lista = (from video in DB.tabelas.Video from subCat in DB.tabelas.Subcategoria where video.id_user == idUser && subCat.Categoria.id == cat && video.Subcategorias.Contains(subCat) && video.Estado.id != 3 select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public List<Video> obterVideosAprovadosUser(Guid idUser)
        {
            List<Video> lista = null;

            try
            {
                lista = (from video in DB.tabelas.Video where video.Estado.id == 2 && video.id_user == idUser select video).ToList<Video>();
            }
            catch { }

            return lista;

        }

        public Video obterVideoMaisRecente(int estado)
        {
            Video aux = null;
            try
            {
                aux = (from video in DB.tabelas.Video where video.Estado.id == estado && video.Estado.id != 3 orderby video.data descending select video).FirstOrDefault<Video>();
            }
            catch { }

            return aux;

        }

        #endregion

        #region // INSERT, DELETE, UPDATE
        public bool inserirVideo(Video video)
        {
            bool sucesso = false;
            try
            {
                DB.tabelas.AddToVideo(video);
                sucesso = (DB.tabelas.SaveChanges() != 0);
            }
            catch { }
            return sucesso;
        }

        public bool actualizaVideo(Video video)
        {
            bool sucesso = false;
            Video aux = null;
            try
            {
                if (video.EntityState != System.Data.EntityState.Detached)
                {
                    aux = obterVideo(video.id);
                    if (aux == null)
                    {
                        return false;
                    }
                    aux.Estado = video.Estado;
                    aux.url = video.url;
                    aux.descricao = video.descricao;
                }
                sucesso = (DB.tabelas.SaveChanges() != 0);
            }
            catch { }
            return sucesso;
        }

        public bool removerVideo(Video video)
        {
            bool sucesso = false;
            Video aux = null;
            try
            {
                if (video.EntityState != System.Data.EntityState.Detached)
                {
                    aux = obterVideo(video.id);
                    if (aux == null)
                    {
                        return false;
                    }
                    DB.tabelas.DeleteObject(aux);
                    sucesso = (DB.tabelas.SaveChanges(SaveOptions.None) != 0);
                }
            }
            catch { }
            return sucesso;
        }



        #endregion
    }
}