using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BO;

namespace DAL
{    
    public class CategoriaDO
    {
        private Entities db { get; set; }

         public CategoriaDO()
        {
            db = new Entities();
        }

        #region // SELECT

         public Categoria obterCategoriaId(int idCat)
         {
            Categoria aux = null;

             try
            {
                aux = (from cat in db.Categoria
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
                 lista = (from cat in db.Categoria select cat).ToList<Categoria>();
             }
             catch { }

             return lista;
         }

        #endregion
    }
}
