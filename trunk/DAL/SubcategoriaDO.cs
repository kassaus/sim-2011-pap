using BO;
using System.Linq;
using System.Collections.Generic;

namespace DAL
{
    public class SubcategoriaDO
    {
        #region // SELECT
        public Subcategoria obterSubCategoriaNome(string nome)
        {
            Subcategoria aux = null;

            try
            {
                aux = (from subCat in DB.tabelas.Subcategoria
                       where subCat.nome == nome
                       select subCat).FirstOrDefault();
            }
            catch { }

            return aux;
        }

        public Subcategoria obterSubCategoriaId(int idSubCat)
        {
            Subcategoria aux = null;

            try
            {
                aux = (from subCat in DB.tabelas.Subcategoria
                       where subCat.id == idSubCat
                       select subCat).FirstOrDefault();
            }
            catch { }

            return aux;
        }

        public List<Subcategoria> obterTodasSubCategoriasCategoria(int cat)
        {
            List<Subcategoria> lista = null;
            try
            {
                lista = (from subCat in DB.tabelas.Subcategoria where subCat.Categoria.id == cat orderby subCat.nome select subCat).ToList<Subcategoria>();
            }
            catch { }

            return lista;
        }

        public List<Subcategoria> obtemTodasSubCategoriasVideos()
        {
            List<Subcategoria> lista = null;

            try
            {
                lista = (from vsc in DB.tabelas.Video select vsc.Subcategorias).FirstOrDefault().ToList<Subcategoria>();
            }
            catch { }

            return lista;
        }

        #endregion

        #region // inserir, alterar e apagar

        public bool insereSubCategoria(Subcategoria novaSubcategoria)
        {
            bool sucesso = false;
            try
            {
                DB.tabelas.AddToSubcategoria(novaSubcategoria);
                sucesso = (DB.tabelas.SaveChanges() != 0);
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
                sucesso = (DB.tabelas.SaveChanges() != 0);
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
                    DB.tabelas.DeleteObject(aux);
                    sucesso = (DB.tabelas.SaveChanges() != 0);
                }
            }
            catch { }
            return sucesso;
        }

        #endregion



    }


}

