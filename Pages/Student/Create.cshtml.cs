using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dotnet_crud.Pages.Student
{
    public class CreateModel : PageModel
    {
        public StudentInfo studentInfo=new StudentInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            studentInfo.name = Request.Form["name"];
            studentInfo.subject = Request.Form["course"];
            if(studentInfo.name=="" || studentInfo.subject=="")
            {
                errorMessage = "All Field Are Requird.!";
                return;
            }


            // Data Insertion
            try
            {
                String connectionString = "Data Source=(localdb)\\local_db;Initial Catalog=dotnet_crud_db;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO students (name,subject) values (@name,@subject)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", studentInfo.name);
                        command.Parameters.AddWithValue("@subject", studentInfo.subject);
                        command.ExecuteNonQuery();

                    }
                }

                studentInfo.name = "";
                studentInfo.subject = "";
                successMessage = "Record Inserted";
                Response.Redirect("/Student/Index");


            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            
           


          




        }
    }
}
