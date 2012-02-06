using System;

namespace SilverlightApplication
{
	public abstract class UrlConverter
	{
		public static string Convert(string relativeUrl)
		{
			return String.Format("{0}://{1}/{2}/{3}/{4}", "http", "localhost", "Lives", "Videos", relativeUrl);
		}
	}
}
