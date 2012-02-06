using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using BLL;
using BO;
using System.Collections.Generic;
using System.Web;

namespace Lives
{
	[ServiceContract(Namespace = "")]
	[SilverlightFaultBehavior]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class WebService
	{
		public static VideoBO gestorVideos = new VideoBO();
		public static CategoriaBO gestorCategorias = new CategoriaBO();
		public static SubcategoriaBO gestorsubcategorias = new SubcategoriaBO();

		[OperationContract]
		public List<Video> obtemTodosVideos()
		{
			return gestorVideos.obterTodosVideos();
		}

		[OperationContract]
		public List<Video> obtemVideosAprovados()
		{
			return gestorVideos.obterVideosAprovados();
		}

		[OperationContract]
		public List<Video> obtemVideosPorAprovar()
		{
			return gestorVideos.obterVideosPorAprovar();
		}

		[OperationContract]
		public Video obtemVideo(int id)
		{
			return gestorVideos.obterVideo(id);
		}

		[OperationContract]
		public List<Video> obtemAprovadosCategoria(int cat)
		{
			return gestorVideos.obterAprovadosCategoria(cat);
		}

		[OperationContract]
		public List<Video> obtemVideosCategoriaUser(int cat, Guid idUser)
		{
			return gestorVideos.obterVideosCategoriaUser(cat, idUser);
		}

		[OperationContract]
		public List<Video> obtemTodosVideosSubcategoria(int idSubcat)
		{
			return gestorVideos.obterTodosVideosSubcategoria(idSubcat);
		}

		[OperationContract]
		public List<Video> obtemTodosVideosSubcategoriaUser(int idSubcat, Guid idUser)
		{
			return gestorVideos.obterTodosVideosSubcategoriaUser(idSubcat, idUser);
		}

		[OperationContract]
		public List<Video> obtemVideosAprovadosUser(Guid idUser)
		{
			return gestorVideos.obterVideosAprovadosUser(idUser);
		}

		[OperationContract]
		public List<Video> obtemVideosPorAprovarUser(Guid idUser)
		{
			return gestorVideos.obterVideosPorAprovadosUser(idUser);
		}

		[OperationContract]
		public List<Video> obtemVideosUser(Guid idUser)
		{
			return gestorVideos.obterVideosUser(idUser);
		}

		[OperationContract]
		public Video obtemVideoMaisRecente(int estado)
		{
			return gestorVideos.obterVideoMaisRecente(estado);
		}

		[OperationContract]
		public List<Video> obtemTodosVideosPorCategorias(List<int> categorias)
		{
			return gestorVideos.obterTodosVideosPorCategorias(categorias);
		}

		[OperationContract]
		public Categoria obtemCategoriaId(int id)
		{
			return gestorCategorias.obterCategoriaId(id);
		}

		[OperationContract]
		public List<Categoria> obterTodas()
		{
			return gestorCategorias.obterTodas();
		}

		[OperationContract]
		public Subcategoria obtemSubCategoriaNome(string nome)
		{
			return gestorsubcategorias.obterSubCategoriaNome(nome);
		}

		[OperationContract]
		public Subcategoria obtemSubCategoriaId(int idSubCat)
		{
			return gestorsubcategorias.obterSubCategoriaId(idSubCat);
		}

		[OperationContract]
		public List<Subcategoria> obtemTodasSubcatCategoria(int cat)
		{
			return gestorsubcategorias.obterTodasSubCategoriasCategoria(cat);
		}

		[OperationContract]
		public List<Subcategoria> obtemTodasSubcatVideos()
		{
			return gestorsubcategorias.obtemTodasSubCategoriasVideos();
		}

		[OperationContract]
		public string obtemPlayList(List<int> cats)
		{

			List<Video> videos = gestorVideos.obterTodosVideosPorCategorias(cats);

			List<string> urls = (from v in videos select converteUrlParaAbsoluto(v.url)).ToList<string>();

			string playList = "<ASX version = \"3.0\">";
			foreach (var u in urls)
				playList += "<Entry><REF HREF = \"" + u + "\" /></Entry>";
			playList += "</ASX>";


			HttpContext.Current.Session["PlayList"] = playList;

			string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
					+ HttpContext.Current.Request.ApplicationPath + "/PlayList.aspx";
			return url;

		}

		private string converteUrlParaAbsoluto(string video)
		{
			return String.Format("{0}{1}/Videos/{2}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), HttpContext.Current.Request.ApplicationPath, video);
		}
	}
}
