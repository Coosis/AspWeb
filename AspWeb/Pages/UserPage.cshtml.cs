using AspWeb.Pages.PageDetail;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Data;

namespace AspWeb.Pages
{
    public class PageFaved
    {
        public string Name;
        public string Link;
    }
    public class UserPageModel : PageModel
    {
        public List<PageFaved> pageFaved;
		private string connectionString = "Server=localhost;User ID=root;Password=Root12345@;Database=user_info";
		public void OnGet()
        {
            if (!User.Identity.IsAuthenticated)
				Response.Redirect("/login");
			pageFaved = new List<PageFaved>();
            string username = User.Identity.Name;
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

                        PageFaved pf = new PageFaved();
                        pf.Name = (string)row["page_name"];
                        pf.Link = (string)row["page_link"];
                        pageFaved.Add(pf);
					}
				}
				connection.Close();
			}
            catch(Exception ex){
                TempData["message"] = ex.ToString();
                TempData["success"] = false;
                Response.Redirect("/login");
            }
		}

        public IActionResult OnGetLogout()
        {
			HttpContext.SignOutAsync();
            TempData["message"] = "Signed out successfully.";
			return RedirectToPage("/Index");
		}

        public IActionResult OnPostGetPages()
        {

            return RedirectToPage("");
        }
    }
}
