using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Data;

namespace AspWeb.Pages.PageDetail
{
	public class DetailPage03Val
	{
		public static bool signed;
		public static bool fav;
	}
	public class detailpage03Model : PageModel
    {
		public string _pagename = "远离生活的建筑没有生命力";
		public string _pagelink = "/PageDetail/detailpage03";
		private string connectionString = "Server=localhost;User ID=root;Password=Root12345@;Database=user_info";
		public void OnGet()
        {
			DetailPage03Val.signed = User.Identity.IsAuthenticated;
			if (DetailPage03Val.signed)
			{
				try
				{
					using MySqlConnection connection = new MySqlConnection(connectionString);
					connection.Open();

					string selectQuery = "SELECT * from " + User.Identity.Name;
					using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
					{
						MySqlDataAdapter datadapter = new MySqlDataAdapter(command);
						DataTable dataTable = new DataTable();
						datadapter.Fill(dataTable);
						foreach (DataRow row in dataTable.Rows)
						{
							if (row["page_name"] == null || row["page_link"] == null)
								continue;

							if ((string)row["page_name"] == _pagename || (string)row["page_link"] == _pagelink)
							{
								DetailPage03Val.fav = true;
								break;
							}
						}
					}
					connection.Close();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}
		}

		public IActionResult OnPostFav()
		{
			if (!User.Identity.IsAuthenticated)
				return RedirectToPage("/login");

			if (DetailPage03Val.fav)
			{
				try
				{
					using MySqlConnection connection = new MySqlConnection(connectionString);
					connection.Open();

					string delete_query = "DELETE FROM " + User.Identity.Name + " WHERE page_name = @Page_Name";
					MySqlCommand command = new MySqlCommand(delete_query, connection);
					command.Parameters.AddWithValue("@Page_Name", _pagename);
					command.ExecuteNonQuery();
					connection.Close();

					DetailPage03Val.fav = false;
					return RedirectToPage(_pagelink);
				}
				catch (Exception ex)
				{
					TempData["message"] = "error caught during: Faved:Yes, try:" + ex.ToString();
					return RedirectToPage("/login");
				}
			}
			else
			{
				try
				{
					using MySqlConnection connection = new MySqlConnection(connectionString);
					connection.Open();
					string add_site_query = "INSERT INTO " + User.Identity.Name + " (page_name, page_link) VALUES (@Page_Name, @Page_Link);";
					MySqlCommand command = new MySqlCommand(add_site_query, connection);
					command.Parameters.AddWithValue("@Page_Name", _pagename);
					command.Parameters.AddWithValue("@Page_Link", _pagelink);
					command.ExecuteNonQuery();
					connection.Close();

					DetailPage03Val.fav = true;
				}
				catch (Exception ex)
				{
					TempData["message"] = "error caught during: Faved:false, try:" + ex.ToString();
					return RedirectToPage("/login");
				}

				return RedirectToPage(_pagelink);
			}
		}
	}
}
