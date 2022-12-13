using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public const string connString = "Server=tcp:manimohaith.database.windows.net,1433;Initial Catalog=studentDB;Persist Security Info=False;User ID=stuadmin;Password=man1@123E;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public StudentController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                SqlDataReader myReader;
                string sqlCommand = "Select Id, RegisterNo, Name, Department, Address From Studenttbl";
                List<Student> students = new List<Student>();
                using (SqlConnection myCon = new SqlConnection(connString))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(sqlCommand, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        while (myReader.Read())
                        {
                            Student stud = new Student();
                            stud.Id = Convert.ToInt32(myReader.GetValue(0));
                            stud.RegisterNo = myReader.GetValue(1).ToString();
                            stud.Name = myReader.GetValue(2).ToString();
                            stud.Department = myReader.GetValue(3).ToString();
                            stud.Address = myReader.GetValue(4).ToString();
                            students.Add(stud);
                        }

                        myReader.Close();
                        myCon.Close();
                    }
                }

                return Ok(students);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Post(Student student)
        {
            try
            {                
                string sqlCommand = "INSERT INTO Studenttbl (RegisterNo,Name,Department,Address) Values (@RegisterNo,@Name,@Department,@Address)";                
                using (SqlConnection myCon = new SqlConnection(connString))
                {
                    myCon.Open();
                    using (SqlCommand sqlCmd = new SqlCommand(sqlCommand, myCon))
                    {
                        sqlCmd.Parameters.AddWithValue("@RegisterNo", student.RegisterNo);
                        sqlCmd.Parameters.AddWithValue("@Name", student.Name);
                        sqlCmd.Parameters.AddWithValue("@Department", student.Department);
                        sqlCmd.Parameters.AddWithValue("@Address", student.Address);                        
                        int rowInserted = sqlCmd.ExecuteNonQuery();
                        myCon.Close();
                    }
                }

                return Ok("Inserted Succcesfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
