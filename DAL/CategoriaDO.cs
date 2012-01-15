using System;
using System.Collections.Generic;
using BO;
using System.Linq;

namespace DAL
{
    public class CategoriaDO
    {
        #region // SELECT

        public Categoria obterCategoriaId(int idCat)
        {
            Categoria aux = null;

            try
            {
                aux = (from cat in DB.tabelas.Categoria
                       where cat.id == idCat
                       select cat).FirstOrDefault();
            }
            catch (Exception) { }

            return aux;
        }

        public List<Categoria> obterTodasCategorias()
        {
            List<Categoria> lista = null;
            try
            {
                lista = (from cat in DB.tabelas.Categoria select cat).ToList<Categoria>();
            }
            catch { }

            return lista;
        }

        #endregion
    }
}
