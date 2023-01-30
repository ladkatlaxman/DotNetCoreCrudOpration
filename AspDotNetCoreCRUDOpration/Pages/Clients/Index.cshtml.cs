using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspDotNetCoreCRUDOpration.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> clients = new List<ClientInfo>();
        
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-NIKT6KQ\\SQLEXPRESS01;Initial Catalog=Core;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from Client";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader=cmd.ExecuteReader())
                        {


                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.Id = "" + reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Email = reader.GetString(2);
                                clientInfo.Phone = reader.GetString(3);
                                clientInfo.Address = reader.GetString(4);
                                clientInfo.Created_at = reader.GetDateTime(5).ToString();

                                clients.Add(clientInfo);
                            }
                        }
                    } 
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }

    public class ClientInfo
    {
        public string Id;
        public string Name;
        public string Email;
        public string Phone;
        public string Address;
        public string Created_at;


    }
}
