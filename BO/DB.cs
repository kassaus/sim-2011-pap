// -----------------------------------------------------------------------
// <copyright file="DB.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BO
{
	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public static class DB
	{
		private static Entities _tabelas = null;

		public static Entities tabelas
		{
			get
			{
				if (_tabelas == null)
				{
					return _tabelas = new Entities();
				}

				return _tabelas;
			}

			private set
			{
				_tabelas = value;
			}
		}
	}
}
