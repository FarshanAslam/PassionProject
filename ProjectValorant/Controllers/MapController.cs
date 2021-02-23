using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectValorant.Controllers
{
    public class MapController : ApiController
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        //Get: /Map/List
        public ActionResult List(string SearchKey = null)
        {
            MapDataController controller = new MapDataController();
            IEnumerable<Map> maps = controller.ListMaps(SearchKey);
            return View(maps);
        }

        //Get: /Map/Show/{id}
        public ActionResult Show(int id)
        {
            MapDataController controller = new MapDataController();
            Map SelectedMap = controller.FindMap(id);

            return View(SelectedMap);
        }

        //Get: /Map/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            MapDataController controller = new MapDataController();
            Map NewMap = controller.FindMap(id);


            return View(NewMap);
        }

        //Post: /Map/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            MapDataController controller = new MapDataController();
            controller.DeleteMap(id);
            return RedirectToAction("List");
        }

        //Get: /Map/Add
        public ActionResult Add()
        {
            return View();
        }

        //Post: /Map/Create
        [HttpPost]
        public ActionResult Create(string MapName, string AttackWinRate, string DefenderWinRate, decimal Popularity)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(MapName);
            Debug.WriteLine(AttackWinRate);
            Debug.WriteLine(DefenderWinRate);
            Debug.WriteLine(Popularity);

            Map NewMap = new Map();
            NewMap.MapName = MapName;
            NewMap.AttackWinRate = AttackWinRate;
            NewMap.DefenderWinRate = DefenderWinRate;
            NewMap.Popularity = Popularity;

            MapDataController controller = new MapDataController();
            controller.AddMap(NewMap);

            return RedirectToAction("List");
        }


        /// <example>GET : /Map/Update/5</example>
        public ActionResult Update(int id)
        {
            MapDataController controller = new MapDataController();
            Map SelectedMap = controller.FindMap(id);

            return View(SelectedMap);
        }

        [HttpPost]
        public ActionResult Update(int id, string MapName, string AttackWinRate, string DefenderWinRate, decimal Popularity)
        {
            Map MapInfo = new Map();
            MapInfo.MapName = MapName;
            MapInfo.AttackWinRate = AttackWinRate;
            MapInfo.DefenderWinRate = DefenderWinRate;
            MapInfo.Popularity = Popularity;

            MapDataController controller = new MapDataController();
            controller.UpdateMap(id, MapInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}
