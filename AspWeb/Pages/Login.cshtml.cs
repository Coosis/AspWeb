using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace AspWeb.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string password { get; set; }
        public void OnGet()
        {
        }

        private string connectionString = "Server=localhost;User ID=root;Password=Root12345@;Database=user_info";
        
        public IActionResult OnPost() {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string selectQuery = "SELECT * from basic_info";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    MySqlDataAdapter datadapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    datadapter.Fill(dataTable);
                    bool match_found = false;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if ((string)row["name"] == username && (string)row["password"] == password)
                        {
                            match_found = true;
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, username),
                                // Add more claims if needed
                            };

                            var identity = new ClaimsIdentity(claims, "HHUCookieScheme");

                            var principal = new ClaimsPrincipal(identity);

                            HttpContext.SignInAsync("HHUCookieScheme", principal);
							var response = new { success = true, message = "Login successful" };
                            connection.Close();

                            return RedirectToPage("/Index");
                        }
                    }
                    if (!match_found)
                    {
                        TempData["success"] = false;
                        TempData["message"] = "Name or password wrong. \nPlease try again.";
                        return RedirectToPage("/login");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                var fail_res = new { success = false, message = "Login error, message:" + ex.Message };
                return new OkObjectResult(fail_res);
            }

            return new OkObjectResult(new { success = false, message = "Unexpected error. Contact site's owner." });
        }
    }
}
