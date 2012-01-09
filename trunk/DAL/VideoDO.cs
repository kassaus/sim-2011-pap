using BO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Data.Objects.DataClasses;

namespace DAL
{
    public class VideoDO
    {
        private livesEntities db { get; set; }

        public VideoDO()
        {
            db = new livesEntities();
        }

        #region // SELECT

        public List<Video> obterTodosVideos()
        {
            List<Video> lista = null;

            try
            {
                lista = (from video in db.Video select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public List<Video> obterVideos(int estado)
        {
            List<Video> lista = null;
            try
            {
                lista = (from video in db.Video where video.Estado.id == estado select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public Video obterVideo(int id)
        {
            Video aux = null;
            try
            {
                aux = (from video in db.Video where video.id == id select video).FirstOrDefault<Video>();
            }
            catch { }

            return aux;
        }

        public List<Video> obterVideosPorUser(Guid idUser)
        {
            List<Video> lista = null;
            try
            {
                lista = (from video in db.Video where video.id_user == idUser select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public List<Video> obterVideosCategoria(int cat)
        {
            List<Video> lista = null;

            try
            {
                lista = (from subCat in db.Subcategoria from video in db.Video where subCat.Categoria.id == cat && video.Subcategorias.Contains(subCat) select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public List<Video> obterAprovadosCategoria(int cat)
        {
            List<Video> lista = null;

            try
            {
                lista = (from video in db.Video from subCat in db.Subcategoria where video.Estado.id == 2 && subCat.Categoria.id == cat && video.Subcategorias.Contains(subCat) select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        //public List<Video> obterPorUser(Guid idUser)
        //{
        //    List<Video> lista = null;
        //    try
        //    {
        //        lista = (from video in db.Video where video.id_user == idUser select video).ToList<Video>();
        //    }
        //    catch { }

        //    return lista;
        //}

       

        public List<Video> obterVideosCategoriaUser(int cat, Guid idUser)
        {
            List<Video> lista = null;

            try
            {
                lista = (from video in db.Video from subCat in db.Subcategoria where video.id_user == idUser && subCat.Categoria.id == cat && video.Subcategorias.Contains(subCat) select video).ToList<Video>();
            }
            catch { }

            return lista;
        }

        public List<Video> obterVideosAprovadosUser(Guid idUser)
        {
            List<Video> lista = null;

            try
            {
                lista = (from video in db.Video where video.Estado.id == 2 && video.id_user == idUser select video).ToList<Video>();
            }
            catch { }

            return lista;

        }

        public Video obterVideoMaisRecente(int estado)
        {
            Video aux = null;
            try
            {
                aux = (from video in db.Video where video.Estado.id == estado orderby video.data descending select video).FirstOrDefault<Video>();
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
                db.AddToVideo(video);
                sucesso = (db.SaveChanges() != 0);
            }
            catch { }
            return sucesso;
        }

        public bool modificaVideo(Video video)
        {
            bool sucesso = false;
            Video aux = null;
            try
            {
                if (video.EntityState == System.Data.EntityState.Detached)
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
                sucesso = (db.SaveChanges() != 0);
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
                if (video.EntityState == System.Data.EntityState.Detached)
                {
                    aux = obterVideo(video.id);
                    if (aux == null)
                    {
                        return false;
                    }
                    db.DeleteObject(aux);
                    sucesso = (db.SaveChanges() != 0);
                }
            }
            catch { }
            return sucesso;
        }


        #endregion
    }
}