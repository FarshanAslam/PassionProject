using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectValorant.Controllers
{
    public class CharacterController : ApiController
    {
        // GET: Character
        public ActionResult Index()
        {
            return View();
        }

        //Get: /Character/List
        public ActionResult List()
        {
            CharacterDataController controller = new CharacterDataController();
            IEnumerable<Character> Characters = controller.ListCharacters();
            return View(Characters);
        }

        //Get: /Character/Show/{id}
        public ActionResult Show(int id)
        {
            CharacterDataController controller = new CharacterDataController();
            Character NewCharacter = controller.FindCharacter(id);


            return View(NewCharacter);
        }
    }
}
