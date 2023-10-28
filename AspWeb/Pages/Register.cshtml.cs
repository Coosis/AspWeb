using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Data;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;

namespace AspWeb.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string password { get; set; }

        private string connectionString = "Server=localhost;User ID=root;Password=Root12345@;Database=user_info";
        public void OnGet()
        {

        }

        public IActionResult OnPost() {
            string str = "";
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return RedirectToPage("/register");
            try
            {
                str += "Err01";
				using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

				str += "Err02";
				string selectQuery = "SELECT * from basic_info";

				str += "Err03";
				using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    MySqlDataAdapter datadapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    datadapter.Fill(dataTable);
                    bool match_found = false;
                    int biggest = -1;
					str += "Err04";
					foreach (DataRow row in dataTable.Rows)
                    {
                        if ((int)row["id"]>biggest)
                            biggest = (int)row["id"];

                        if (row["name"] == null || row["email"] == null)
                            continue;

                        if ((string)row["name"] == username || (string)row["email"] == email)
                        {
                            match_found = true;

                            var response = new { success = true, message = "Login successful" };
                            connection.Close();

							str += "Err05";
							TempData["register_success"] = false;
                            TempData["register_info"] = "The username or email had been taken.";
                            return RedirectToPage("/register");
                        }
                    }
                    if (!match_found)
                    {
                        //inserting into user list
                        string add_user_query = "INSERT INTO basic_info (id, name, email, password) VALUES (@ID, @Username, @Email, @Password);";
                        MySqlCommand command2 = new MySqlCommand(add_user_query, connection);
                        command2.Parameters.AddWithValue("@ID", biggest+1);
                        command2.Parameters.AddWithValue("@Username", username);
                        command2.Parameters.AddWithValue("@Email", email);
                        command2.Parameters.AddWithValue("@Password", password);
                        command2.ExecuteNonQuery();
						str += "Err06";

						//creating user table
						string user_table_query = "CREATE TABLE IF NOT EXISTS " + username + " (id INT AUTO_INCREMENT PRIMARY KEY, page_name VARCHAR(255), page_link VARCHAR(255));";
						MySqlCommand command3 = new MySqlCommand(user_table_query, connection);
						command3.ExecuteNonQuery();
						str += "Err07";

						connection.Close();
                        TempData["register_success"] = true;
                        TempData["register_info"] = "Register Success! User: " + username + " Email: " + email;
                        return RedirectToPage("/login");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["register_success"] = false;
                TempData["register_info"] = "Register failed! Message: "+ str + ex.Message;
				str += ex.Message;

				return RedirectToPage("/register");
            }

            TempData["register_success"] = false;
            TempData["register_info"] = "Unexpected failure: " + str;
            return RedirectToPage("/register");
        }
    }
}
