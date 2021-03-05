using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectValorant.Models
{
    public class Map
    {
        //The following properties define an Map
        public int mapid;
        public string mapName;
        public string AttackWinRate;
        public string DefenderWinRate;
        public DateTime mapdateadded;
        public decimal Popularity;

        //parameter-less constructor function
        public Map() { }
    }
}