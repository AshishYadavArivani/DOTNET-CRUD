using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace dotnet_crud.Pages.Student
{
    public class DeleteModel : PageModel
    {
        public  StudentInfo studentinfo= new StudentInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            string connectionString = "Data Source=(localdb)\\local_db;Initial Catalog=dotnet_crud_db;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM students WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            Response.Redirect("/Student/Index");
            

        }
    }
}
