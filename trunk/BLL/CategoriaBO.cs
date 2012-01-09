using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;
using DAL;

namespace BLL
{
    public class CategoriaBO
    {
         private CategoriaDO categoriasDataManager { get; set; }

         public CategoriaBO()
        {
            categoriasDataManager = new CategoriaDO();
        }

        #region //Select

         public Categoria obterCategoriaId(int id)
        {
            return categoriasDataManager.obterCategoriaId(id);
        }

        public List<Categoria> obterTodas()
        {
            return categoriasDataManager.obterTodasCategorias();
        }    

        #endregion
    }
}
