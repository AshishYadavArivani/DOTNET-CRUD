using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dotnet_crud.Pages.Student
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> listStudents = new List<StudentInfo>();
        public string errorMessage="";
        public string successMessage = "";
        public void OnGet()
        {

            string connectionString = "Data Source=(localdb)\\local_db;Initial Catalog=dotnet_crud_db;Integrated Security=True";
            try
            {
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Select * FROM students";
                    using(SqlCommand command = new SqlCommand(sql,connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo studnetInfo = new StudentInfo();

                                studnetInfo.id = "" + reader.GetInt64(0);
                                studnetInfo.name = reader.GetString(1);
                                studnetInfo.subject =reader.GetString(2);
                                listStudents.Add(studnetInfo);
                               
                            }
                        }
                    }


                }
               


            }catch(Exception ex)
            {

                Console.WriteLine("Exception is :" + ex.ToString());

            }
        }
    }

    public class StudentInfo
    {
        public string id;
        public string name;
        public string subject;
      
    }

}
