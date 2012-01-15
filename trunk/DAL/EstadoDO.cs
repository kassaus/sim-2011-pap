using System.Linq;
using BO;
// -----------------------------------------------------------------------
// <copyright file="EstadoDO.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace DAL
{

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class EstadoDO
	{
		public Estado getById(int id)
		{
			return (from estado in DB.tabelas.Estado where estado.id == id select estado).FirstOrDefault<Estado>();
		}
	}
}
