using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectValorant.Controllers
{
    public class MapDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private ValorantDbContext ProjectValorant = new ValorantDbContext();

        //This Controller Will access the maps table of our ProjectValorant database.
        /// <summary>
        /// Returns a list of maps in the system
        /// </summary>
        /// <example>GET api/MapData/ListMaps</example>

        [HttpGet]
        [Route("api/MapData/ListMaps/{SearchKey?}")]
        public IEnumerable<Map> ListMaps(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = ProjectValorant.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from maps where lower(MapName) like lower(@key) or lower(AttackWinRate) like lower(@key) or lower(concat(MapName, ' ', AttackWinRate)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Map Names
            List<Map> maps = new List<Map> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int mapid = (int)ResultSet["mapid"];
                string MapName = (string)ResultSet["MapName"];
                string AttackWinRate = (string)ResultSet["AttackWinRate"];
                string DefenderWinRate = (string)ResultSet["DefenderWinRate"];
                DateTime mapdateadded = (DateTime)ResultSet["mapdateadded"];
                decimal Popularity = (decimal)ResultSet["Popularity"];

                Map NewMap = new Map();
                NewMap.mapid = mapid;
                NewMap.MapName = MapName;
                NewMap.AttackWinRate = AttackWinRate;
                NewMap.DefenderWinRate = DefenderWinRate;
                NewMap.mapdateadded = mapdateadded;
                NewMap.Popularity = Popularity;
                //Add the Map Name to the List
                maps.Add(NewMap);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Map names
            return maps;
        }

        [HttpGet]
        public Map FindMap(int id)
        {
            Map NewMap = new Map();

            //Create an instance of a connection
            MySqlConnection Conn = ProjectValorant.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from maps where mapid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int mapid = (int)ResultSet["mapid"];
                string MapName = (string)ResultSet["MapName"];
                string AttackWinRate = (string)ResultSet["AttackWinRate"];
                string DefenderWinRate = (string)ResultSet["DefenderWinRate"];
                DateTime mapdateadded = (DateTime)ResultSet["mapdateadded"];
                decimal Popularity = (decimal)ResultSet["Popularity"];

                NewMap.mapid = mapid;
                NewMap.MapName = MapName;
                NewMap.AttackWinRate = AttackWinRate;
                NewMap.DefenderWinRate = DefenderWinRate;
                NewMap.mapdateadded = mapdateadded;
                NewMap.Popularity = Popularity;
            }

            return NewMap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>Post : /api/MapData/DeleteMap/3</example>
        [HttpPost]
        public void DeleteMap(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = ProjectValorant.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from maps where mapid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        [HttpPost]
        public void AddMap(Map NewMap)
        {
            //Create an instance of a connection
            MySqlConnection Conn = ProjectValorant.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into maps (MapName, AttackWinRate, DefenderWinRate, mapdateadded, Popularity) value (@MapName, @AttackWinRate, @DefenderWinRate, CURRENT_DATE(), @Popularity)";
            cmd.Parameters.AddWithValue("@MapName", NewMap.MapName);
            cmd.Parameters.AddWithValue("@AttackWinRate", NewMap.AttackWinRate);
            cmd.Parameters.AddWithValue("@DefenderWinRate", NewMap.DefenderWinRate);

            cmd.Parameters.AddWithValue("@Popularity", NewMap.Popularity);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

 
        public void UpdateMap(int id, [FromBody] Map MapInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = ProjectValorant.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update maps set MapName=@MapName, AttackWinRate=@AttackWinRate, DefenderWinRate=@DefenderWinRate, Popularity=@Popularity where mapid=@mapid";
            cmd.Parameters.AddWithValue("@MapName", MapInfo.MapName);
            cmd.Parameters.AddWithValue("@AttackWinRate", MapInfo.AttackWinRate);
            cmd.Parameters.AddWithValue("@DefenderWinRate", MapInfo.DefenderWinRate);
            cmd.Parameters.AddWithValue("@Popularity", MapInfo.Popularity);
            cmd.Parameters.AddWithValue("@mapid", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}

