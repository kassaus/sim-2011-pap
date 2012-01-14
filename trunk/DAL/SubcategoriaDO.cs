using System;
using System.Collections.Generic;
using System.Linq;
using BO;

namespace DAL
{
    public class SubcategoriaDO
    {
        private Entities db { get; set; }

        public SubcategoriaDO()
        {
            db = new Entities();
        }

        #region // SELECT
        public Subcategoria obterSubCategoriaNome(string nome)
        {
            Subcategoria aux = null;

            try
            {
                aux = (from subCat in db.Subcategoria
                       where subCat.nome == nome
                       select subCat).FirstOrDefault();
            }
            catch (Exception) { }

            return aux;
        }

        public Subcategoria obterSubCategoriaId(int idSubCat)
        {
            Subcategoria aux = null;

            try
            {
                aux = (from subCat in db.Subcategoria
                       where subCat.id == idSubCat
                       select subCat).FirstOrDefault();
            }
            catch (Exception) { }

            return aux;
        }

        public List<Subcategoria> obterTodasSubCategoriasCategoria(int cat)
        {
            List<Subcategoria> lista = null;
            try
            {
                lista = (from subCat in db.Subcategoria where subCat.Categoria.id == cat select subCat).ToList<Subcategoria>();
            }
            catch { }

            return lista;
        }

        public List<Subcategoria> obtemTodasSubCategoriasVideos()
        {
            List<Subcategoria> lista = null;

            try
            {
                lista = (from vsc in db.Video select vsc.Subcategorias).FirstOrDefault().ToList<Subcategoria>();
            }
            catch { }

            return lista;
        }

        #endregion

        #region // inserir, alterar e apagar

        public bool insereSubCategoria(Subcategoria novaSubcategoria) {
        bool sucesso = false;
            try
            {
                db.AddToSubcategoria(novaSubcategoria);
                sucesso = (db.SaveChanges() != 0);
            }
            catch { }
            return sucesso;
        }

        public bool modificaSubcategoria(Subcategoria subCat)
        {
            bool sucesso = false;
            Subcategoria aux = null;
            try
            {
                if (subCat.EntityState != System.Data.EntityState.Detached)
                {
                    aux = obterSubCategoriaId(subCat.id);
                    if (aux == null)
                    {
                        return false;
                    }
                    aux.nome = subCat.nome;
                    aux.Categoria = subCat.Categoria;
                    
                }
                sucesso = (db.SaveChanges() != 0);
            }
            catch { }
            return sucesso;
        }

        public bool removerSubCategoria(Subcategoria subCat)
        {
            bool sucesso = false;
            Subcategoria aux = null;
            try
            {
                if (subCat.EntityState != System.Data.EntityState.Detached)
                {
                    aux = obterSubCategoriaId(subCat.id);
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

