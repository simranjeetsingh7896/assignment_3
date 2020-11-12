using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using System.Web.UI.WebControls;
using assignment_3.Models;
using MySql.Data.MySqlClient;

namespace assignment_3.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the students table of our school database.
        /// <summary>
        /// Returns a list of students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of classes (studentid,studentfname,studentlname,studentnumber,enroldate)
        /// </returns>

        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Student> ListStudents()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Students";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Students 
            List<Student> Students = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                uint Studentid = (uint)ResultSet["studentid"];
                string Studentfname = (string)ResultSet["studentfname"];
                string Studentlname = (string)ResultSet["studentlname"];
                string Studentnumber = (string)ResultSet["studentnumber"];
                DateTime Enroldate = (DateTime)ResultSet["enroldate"];

                Student NewStudent = new Student();
                NewStudent.Studentid = Studentid;
                NewStudent.Studentfname = Studentfname;
                NewStudent.Studentlname = Studentlname;
                NewStudent.Studentnumber = Studentnumber;
                NewStudent.Enroldate = Enroldate;


                //Add the Class Name to the List
                Students.Add(NewStudent);
            }
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of class names
            return Students;
        }


        //This Controller Will access the students table of our school database.
        /// <summary>
        /// Returns a list of students in the system with a studentid
        /// </summary>
        /// <example>GET api/StudentData/FindStudent/{id}</example>
        /// <returns>
        /// A list of student (studentid,studentfname,studentlname,studentnumber,enroldate)
        /// </returns>

        [HttpGet]
        [Route("api/StudentData/FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Students where studentid =" + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                uint Studentid = (uint)ResultSet["studentid"];
                string Studentfname = (string)ResultSet["studentfname"];
                string Studentlname = (string)ResultSet["studentlname"];
                string Studentnumber = (string)ResultSet["studentnumber"];
                DateTime Enroldate = (DateTime)ResultSet["enroldate"];

                NewStudent.Studentid = Studentid;
                NewStudent.Studentfname = Studentfname;
                NewStudent.Studentlname = Studentlname;
                NewStudent.Studentnumber = Studentnumber;
                NewStudent.Enroldate = Enroldate;
            }
            return NewStudent;
        }
    }
}