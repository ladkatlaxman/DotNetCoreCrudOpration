using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspDotNetCoreCRUDOpration.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string ErrrorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.Name = Request.Form["Name"];
            clientInfo.Email = Request.Form["Email"];
            clientInfo.Phone = Request.Form["Phone"];
            clientInfo.Address = Request.Form["Address"];

            if (clientInfo.Name.Length==0||clientInfo.Email.Length==0
                ||clientInfo.Phone.Length==0||clientInfo.Address.Length==0)
            {
                ErrrorMessage = "All The Field Required";
                return;
            }

            try
            {

                string connectionString = "Data Source=DESKTOP-NIKT6KQ\\SQLEXPRESS01;Initial Catalog=Core;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "Insert into client" + "(Name,Email,Phone,Address)values" + "(@Name,@Email,@Phone,@Address);";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", clientInfo.Name);
                        cmd.Parameters.AddWithValue("@Email", clientInfo.Email);
                        cmd.Parameters.AddWithValue("@Phone", clientInfo.Phone);
                        cmd.Parameters.AddWithValue("@Address", clientInfo.Address);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                ErrrorMessage = ex.Message;
                return;
            }





            clientInfo.Name = "";
            clientInfo.Email = "";
            clientInfo.Phone = "";
            clientInfo.Address = "";
            SuccessMessage = "Client Added Successfully...";

            Response.Redirect("/Clients/Index");
        }
    }
}
