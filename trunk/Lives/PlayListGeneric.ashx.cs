using System.Web;
using System.Collections.Generic;
using System;
using BO;
using BLL;
using System.Web.Services;
using System.Web.Security;
namespace Lives
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class PlayListGeneric : IHttpHandler
    {

        private VideoBO gestorVideos { get; set; }

        public List<Video> getLista(string cat, string subcat)
        {

            gestorVideos = new VideoBO();

            var listaVideos = gestorVideos.obterVideosAprovados();
            List<Video> listaAux = new List<Video>();

            if (cat != null)
            {
                if (int.Parse(cat) > 0 || int.Parse(cat) < 7)
                {
                    switch (int.Parse(cat))
                    {
                        case 1:
                            {
                                if (subcat != null)
                                {
                                    listaAux = gestorVideos.obterTodosVideosSubcategoriaAprovados(int.Parse(subcat));
                                }
                                else
                                {
                                    listaAux = gestorVideos.obterAprovadosCategoria(int.Parse(cat));
                                }
                                return listaAux;
                                break;
                            }
                        case 2:
                            {
                                if (subcat != null)
                                {
                                    listaAux = gestorVideos.obterTodosVideosSubcategoriaAprovados(int.Parse(subcat));
                                }
                                else
                                {
                                    listaAux = gestorVideos.obterAprovadosCategoria(int.Parse(cat));
                                }
                                return listaAux;
                                break;
                            }
                        case 3:
                            {
                                if (subcat != null)
                                {
                                    listaAux = gestorVideos.obterTodosVideosSubcategoriaAprovados(int.Parse(subcat));
                                }
                                else
                                {
                                    listaAux = gestorVideos.obterAprovadosCategoria(int.Parse(cat));
                                }
                                return listaAux;
                                break;
                            }
                        case 4:
                            {
                                if (subcat != null)
                                {
                                    listaAux = gestorVideos.obterTodosVideosSubcategoriaAprovados(int.Parse(subcat));
                                }
                                else
                                {
                                    listaAux = gestorVideos.obterAprovadosCategoria(int.Parse(cat));
                                }
                                return listaAux;
                                break;
                            }
                        case 5:
                            {
                                if (subcat != null)
                                {
                                    listaAux = gestorVideos.obterTodosVideosSubcategoriaAprovados(int.Parse(subcat));
                                }
                                else
                                {
                                    listaAux = gestorVideos.obterAprovadosCategoria(int.Parse(cat));
                                }
                                return listaAux;
                                break;
                            }
                        case 6:
                            {
                                if (subcat != null)
                                {
                                    listaAux = gestorVideos.obterTodosVideosSubcategoriaAprovados(int.Parse(subcat));
                                }
                                else
                                {
                                    listaAux = gestorVideos.obterAprovadosCategoria(int.Parse(cat));
                                }
                                return listaAux;
                                break;
                            }
                    }
                }
            }
            return null;
        }
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var categoria = context.Request.QueryString["cat"];
                var subcategoria = context.Request.QueryString["subcat"];
                List<Video> lista = getLista(categoria, subcategoria);
                //context.Response.ContentType = "video/x-ms-asf";
                context.Response.ContentType = "video/x-ms";
                context.Response.Write("<ASX version = \"3.0\">");
                context.Response.Write("<Title>Simple ASX Demo</Title>");
                for (int i = 0; i < lista.Count; i++)
                {
                    context.Response.Write("<Entry><Ref HREF=\"http://localhost/Lives/Videos/" + lista[i].url + "/" + "\" /></Entry>");
                }
                context.Response.Write("</ASX>");
            }
            catch (Exception e) { }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}