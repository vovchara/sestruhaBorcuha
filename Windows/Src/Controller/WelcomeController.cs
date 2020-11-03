using System;
using Monosyne;
using Monosyne.Scene.V3;
using Newtonsoft.Json;
using Scene.Model;
using Scene.Src.Controller;
using Scene.Src.Model;
using Scene.View;

namespace Scene.Controller
{
    public class WelcomeController : ControllerBase
    {
        public event Action StartNewGame = delegate { };

        private WelcomePopup _welcomePopup;
        private UserStorage _userStorage;

        public void Start()
        {
            _welcomePopup = new WelcomePopup(_game);
            _welcomePopup.UserClickedStartGame += OnUserClickedStart;
            
            var view = _welcomePopup.View;
            _rootScene.AddChild(view);
        }

        public void OnUserClickedStart(string userName)
        {
            _userStorage = UserStorage.getInstance();
            var isUserExist = _userStorage.CheckExistingUsers(userName);
            if (isUserExist)
            {
                // show error popup
                return;
            }
            else
            {
                var userModel = new UserModel(userName, 0);
                _userStorage.AddMyUser(userModel);
                StartNewGame();
            }
        }

        public void Dispose()
        {
            if (_welcomePopup != null)
            {
                _welcomePopup.UserClickedStartGame -= OnUserClickedStart;
                _welcomePopup.Dispose();
            }
        }
    }
}