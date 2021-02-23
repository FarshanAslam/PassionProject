using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectValorant.Controllers
{
    public class CharacterDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private ValorantDbContext ProjectValorant = new ValorantDbContext();

        //This Controller Will access the Characters table of our ProjectValorant database.
        /// <summary>
        /// Returns a list of Characters in the system
        /// </summary>
        /// <example>GET api/CharacterData/ListCharacters</example>

        [HttpGet]
        public IEnumerable<Character> ListCharacters()
        {
            //Create an instance of a connection
            MySqlConnection Conn = ProjectValorant.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Characters";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Author Names
            List<Character> Characters = new List<Character> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int characterid = (int)ResultSet["characterid"];
                string characterfname = (string)ResultSet["characterfname"];
                string characterlname = (string)ResultSet["characterlname"];
                string characterrole = (string)ResultSet["characterrole"];
                DateTime dateadded = (DateTime)ResultSet["dateadded"];

                Character NewCharacter = new Character();
                NewCharacter.characterid = characterid;
                NewCharacter.characterfname = characterfname;
                NewCharacter.characterlname = characterlname;
                NewCharacter.characterrole = characterrole;
                NewCharacter.dateadded = dateadded;
                //Add the Character Name to the List
                Characters.Add(NewCharacter);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Characters names
            return Characters;
        }

        [HttpGet]
        public Character FindCharacter(int id)
        {
            Character NewCharacter = new Character();

            //Create an instance of a connection
            MySqlConnection Conn = ProjectValorant.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Characters where characterid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int characterid = (int)ResultSet["characterid"];
                string characterfname = (string)ResultSet["characterfname"];
                string characterlname = (string)ResultSet["characterlname"];
                string characterrole = (string)ResultSet["characterrole"];
                DateTime dateadded = (DateTime)ResultSet["dateadded"];

                NewCharacter.characterid = characterid;
                NewCharacter.characterfname = characterfname;
                NewCharacter.characterlname = characterlname;
                NewCharacter.characterrole = characterrole;
                NewCharacter.dateadded = dateadded;
            }

            return NewCharacter;
        }
    }
}
