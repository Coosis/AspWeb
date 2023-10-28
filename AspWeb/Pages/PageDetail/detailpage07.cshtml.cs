using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Data;

namespace AspWeb.Pages.PageDetail
{
	public class DetailPage07Val
	{
		public static bool signed;
		public static bool fav;
	}
	public class detailpage07Model : PageModel
    {
		public string _pagename = "�����ң�Ϊ������ ƴ�����¹ھ�";
		public string _pagelink = "/PageDetail/detailpage07";

		private string connectionString = "Server=localhost;User ID=root;Password=Root12345@;Database=user_info";
		public void OnGet()
		{
			DetailPage07Val.signed = User.Identity.IsAuthenticated;
			if (DetailPage07Val.signed)
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
								DetailPage07Val.fav = true;
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

			if (DetailPage07Val.fav)
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

					DetailPage07Val.fav = false;
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

					DetailPage07Val.fav = true;
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
