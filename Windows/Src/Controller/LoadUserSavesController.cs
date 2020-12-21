using Newtonsoft.Json;
using Scene.Model;
using Scene.Src.Infra;
using Scene.Src.Model;
using System.Collections.Generic;
using System.Linq;

namespace Scene.Src.Controller
{
    public class LoadUserSavesController : ControllerBase
    {
        public LoadUserSavesController(RootSceneContainer sceneContainer, ViewFactory viewFactory, UserStorage userStorage) : base(sceneContainer, viewFactory, userStorage)
        {
        }
        public void Start()
        {
            var hasSavedUsers = System.IO.File.Exists(ConfigConstants.UserSavePath);
            if (hasSavedUsers)
            {
                var savedUsersText = System.IO.File.ReadAllText(ConfigConstants.UserSavePath);
                if (savedUsersText == "")
                {
                    _userStorage.AllUsers = new List<UserModel>();
                }
                else
                {
                    var arrayOfUsers = JsonConvert.DeserializeObject<UserModel[]>(savedUsersText);
                    _userStorage.AllUsers = arrayOfUsers.ToList();
                }
            }
            else
            {
                _userStorage.AllUsers = new List<UserModel>();
            }
        }

        public void Dispose()
        {

        }
    }
}
