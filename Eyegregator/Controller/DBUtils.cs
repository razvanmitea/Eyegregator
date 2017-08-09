using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Eyegregator
{
	public static class GetDBData
	{
		public static List<Keywords> GetDataFromTable(TaskScheduler uiScheduler, string table)
		{
			RSSActivity.DB.QueryAsync<Keywords> ("SELECT * FROM " + table).ContinueWith (t => {
				return t.Result.Select(x=>x).ToList();
			}, uiScheduler);
			return null;
		}

		public static void InsertNewEntry_Keywords(string keyword)
		{
			var newKeyword = new Keywords { Keyword = keyword};
			RSSActivity.DB.InsertAsync (newKeyword);
		}

		public static void DeleteEntry_Keywords(Keywords entry)
		{
			RSSActivity.DB.DeleteAsync (entry);
		}

		public static object FindEntry_Keywords(string keyword,TaskScheduler uiScheduler)
		{
			var entry = new Keywords();
			RSSActivity.DB.QueryAsync<Keywords> ("SELECT * FROM Keywords where Keyword = '"+ keyword +"'").ContinueWith (t => {
				entry = t.Result.Select(x=>x).First();
			}, uiScheduler);
			return entry;

		}
	}
}

