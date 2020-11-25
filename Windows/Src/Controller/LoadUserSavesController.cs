using Newtonsoft.Json;
using Scene.Model;
using Scene.Src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scene.Src.Controller
{
    public class LoadUserSavesController : ControllerBase
    {
        public void Start()
        {
            var hasSavedUsers = System.IO.File.Exists(ConfigConstants.UserSavePath);
            if (hasSavedUsers)
            {
                var savedUsersText = System.IO.File.ReadAllText(ConfigConstants.UserSavePath);
                if (savedUsersText == "")
                {
                    UserStorage.getInstance().AllUsers = new List<UserModel>();
                }
                else
                {
                    var arrayOfUsers = JsonConvert.DeserializeObject<UserModel[]>(savedUsersText);
                    UserStorage.getInstance().AllUsers = arrayOfUsers.ToList();
                }
            }
            else
            {
                UserStorage.getInstance().AllUsers = new List<UserModel>();
            }
        }

        public void Dispose()
        {

        }
    }
}
