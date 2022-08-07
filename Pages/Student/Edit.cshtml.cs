using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dotnet_crud.Pages.Student
{
    public class EditModel : PageModel
    {
        public StudentInfo studentInfo= new StudentInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
             string id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=(localdb)\\local_db;Initial Catalog=dotnet_crud_db;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM students WHERE id=@id";
                    using(SqlCommand command = new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                studentInfo = new StudentInfo();
                                studentInfo.id = "" + reader.GetInt64(0);
                                studentInfo.name = reader.GetString(1);
                                studentInfo.subject = reader.GetString(2);
                                
                                
                            }

                        }

                    }
                }

            }
            catch(Exception ex)
            {
                errorMessage=ex.Message;
                return;
            }
        }

        public void OnPost()
        {
            studentInfo.id = Request.Form["id"];
            studentInfo.name = Request.Form["name"];
            studentInfo.subject = Request.Form["course"];

            if(studentInfo.name == "" || studentInfo.subject == "")
            {
                errorMessage = "All Field are rewuired..!";
                return;
            }

            try
            {
                string connectionString = "Data Source=(localdb)\\local_db;Initial Catalog=dotnet_crud_db;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE  students "+" SET name=@name, subject=@subject WHERE id=@id";
                    using (SqlCommand command=new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", studentInfo.name);
                        command.Parameters.AddWithValue("@subject", studentInfo.subject);
                        command.Parameters.AddWithValue("@id", studentInfo.id);
                        command.ExecuteNonQuery();

                    }

                }
                    

            }catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            Response.Redirect("/Student/Index");
        }
    }
}
