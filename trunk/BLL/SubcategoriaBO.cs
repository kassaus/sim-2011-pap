using System.Collections.Generic;
using BO;
using DAL;

namespace BLL
{
    public class SubcategoriaBO
    {
        private SubcategoriaDO subCategoriasDataManager { get; set; }
        private CategoriaBO categoriasManager { get; set; }

        public SubcategoriaBO()
        {
            subCategoriasDataManager = new SubcategoriaDO();
            categoriasManager = new CategoriaBO();
        }

        #region //Select

        public Subcategoria obterSubCategoriaId(int idSubCat)
        {
            return subCategoriasDataManager.obterSubCategoriaId(idSubCat);
        }

        public List<Subcategoria> obterTodasSubCategoriasCategoria(int cat)
        {
            List<Subcategoria> lista = null;

            lista = subCategoriasDataManager.obterTodasSubCategoriasCategoria(cat);

            if (lista == null)
            {
                lista = new List<Subcategoria>();
            }

            return lista;
        }

        public List<Subcategoria> obtemTodasSubCategoriasVideos()
        {
            List<Subcategoria> lista = null;
            lista = subCategoriasDataManager.obtemTodasSubCategoriasVideos();

            if (lista == null)
            {
                lista = new List<Subcategoria>();
            }

            return lista;
        }

        #endregion

        #region // inserir, alterar e apagar

        public bool criarSubCategoria(string nome, int cat)
        {
            Subcategoria subCat = new Subcategoria();
            subCat.Categoria = categoriasManager.obterCategoriaId(cat);
            subCat.nome = nome;

            return subCategoriasDataManager.insereSubCategoria(subCat);
        }

        public bool modificaSubcategoria(string nome, int cat)
        {
            Subcategoria subCat = new Subcategoria();
            subCat.Categoria = categoriasManager.obterCategoriaId(cat);
            subCat.nome = nome;

            return subCategoriasDataManager.modificaSubcategoria(subCat);
        }

        public bool removeSubcategoria(int idsubcat)
        {
            return subCategoriasDataManager.removerSubCategoria(obterSubCategoriaId(idsubcat));
        }

        #endregion
    }
}
