using System.Collections.Generic;
using BO;
using DAL;

namespace BLL
{
    public class SubcategoriaBO
    {
        private SubcategoriaDO subCategoriasDataManager { get; set; }
        private CategoriaDO categoriasDataManager { get; set; }

        public SubcategoriaBO()
        {
            subCategoriasDataManager = new SubcategoriaDO();
            categoriasDataManager = new CategoriaDO();
        }

        #region //Select

        public Subcategoria obterSubCategoriaNome(string nome)
        {
            return subCategoriasDataManager.obterSubCategoriaNome(nome);
        }

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
            subCat.Categoria = categoriasDataManager.obterCategoriaId(cat);
            subCat.nome = nome;

            return subCategoriasDataManager.insereSubCategoria(subCat);
        }

        public bool modificaSubcategoria(string nome, int cat)
        {
            Subcategoria subCat = new Subcategoria();
            subCat.Categoria = categoriasDataManager.obterCategoriaId(cat);
            subCat.nome = nome;

            return subCategoriasDataManager.modificaSubcategoria(subCat);
        }

        public bool removeSubcategoria(int idsubcat)
        {
            return removeSubcategoria(obterSubCategoriaId(idsubcat));
        }

        public bool removeSubcategoria(string nome)
        {
            return removeSubcategoria(obterSubCategoriaNome(nome));
        }

        public bool removeSubcategoria(Subcategoria subcat)
        {
            return subCategoriasDataManager.removerSubCategoria(subcat);
        }

        #endregion


    }
}
