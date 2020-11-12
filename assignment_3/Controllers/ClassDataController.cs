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
    public class ClassDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the classes table of our school database.
        /// <summary>
        /// Returns a list of classes in the system
        /// </summary>
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns>
        /// A list of classes (classid,classcode,teacherid,startdate,finishdate,classname)
        /// </returns>
        
        [HttpGet]
        [Route("api/ClassData/ListClasses")]
        public IEnumerable<Class>ListClasses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Classes 
            List<Class> Classes = new List<Class> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int ClassId = (int)ResultSet["classid"];
                string Classcode = (string)ResultSet["classcode"];
                long TeacherId = (long)ResultSet["teacherid"];
                DateTime Startdate = (DateTime)ResultSet["startdate"];
                DateTime Finishdate = (DateTime)ResultSet["finishdate"];
                string Classname = (string)ResultSet["classname"];

                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.Classcode = Classcode;
                NewClass.TeacherId = TeacherId;
                NewClass.Startdate = Startdate;
                NewClass.Finishdate = Finishdate;
                NewClass.Classname = Classname;

             //Add the Class Name to the List
             Classes.Add(NewClass);
            }
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of class names
            return Classes;
        }

        //This Controller Will access the classes table of our school database.
        /// <summary>
        /// Returns a list of classes in the system with classid
        /// </summary>
        /// <example>GET api/ClassData/FindClass/{id}</example>
        /// <returns>
        /// A list of classes (classid,classcode,teacherid,startdate,finishdate,classname)
        /// </returns>
       
        [HttpGet]
        [Route("api/ClassData/FindClass/{id}")]
        public Class FindClass(int id)
        {
            Class NewClass = new Class();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where classid ="+id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int ClassId = (int)ResultSet["classid"];
                string Classcode = (string)ResultSet["classcode"];
                long TeacherId = (long)ResultSet["teacherid"];
                DateTime Startdate = (DateTime)ResultSet["startdate"];
                DateTime Finishdate = (DateTime)ResultSet["finishdate"];
                string Classname = (string)ResultSet["classname"];
              
                NewClass.ClassId = ClassId;
                NewClass.Classcode = Classcode;
                NewClass.TeacherId = TeacherId;
                NewClass.Startdate = Startdate;
                NewClass.Finishdate = Finishdate;
                NewClass.Classname = Classname;
            }

            return NewClass;
        }
    }
}
