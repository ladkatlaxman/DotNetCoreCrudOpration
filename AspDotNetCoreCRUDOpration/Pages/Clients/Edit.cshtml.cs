using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspDotNetCoreCRUDOpration.Pages.Clients
{
    public class EditModel : PageModel
    {

        public ClientInfo clientInfo = new ClientInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=DESKTOP-NIKT6KQ\\SQLEXPRESS01;Initial Catalog=Core;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from Client where Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {


                            if (reader.Read())
                            {

                                clientInfo.Id = "" + reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Email = reader.GetString(2);
                                clientInfo.Phone = reader.GetString(3);
                                clientInfo.Address = reader.GetString(4);

                            }
                        }  
                    }
                }  
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }


        }

        public void OnPost()
        {

            clientInfo.Id = Request.Form["id"];
            clientInfo.Name = Request.Form["Name"];
            clientInfo.Email = Request.Form["Email"];
            clientInfo.Phone = Request.Form["Phone"];
            clientInfo.Address = Request.Form["Address"];

            if (clientInfo.Name.Length == 0 || clientInfo.Email.Length == 0
                || clientInfo.Phone.Length == 0 || clientInfo.Address.Length == 0)
            {
                ErrorMessage = "All The Field Required";
                return;
            }
            try
            {
                string connectionString = "Data Source=DESKTOP-NIKT6KQ\\SQLEXPRESS01;Initial Catalog=Core;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "update client set Name=@Name,Email=@Email,Phone=@Phone,Address=@Address where id=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", clientInfo.Name);
                        cmd.Parameters.AddWithValue("@Email", clientInfo.Email);
                        cmd.Parameters.AddWithValue("@Phone", clientInfo.Phone);
                        cmd.Parameters.AddWithValue("@Address", clientInfo.Address);
                        cmd.Parameters.AddWithValue("@id", clientInfo.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception  ex)
            {

                ErrorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Clients/Index");
        }
    }
}
