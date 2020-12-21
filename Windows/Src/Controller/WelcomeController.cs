using System;
using Scene.Model;
using Scene.Src.Controller;
using Scene.Src.Infra;
using Scene.Src.Model;
using Scene.View;

namespace Scene.Controller
{
    public class WelcomeController : ControllerBase
    {
        public event Action StartNewGame = delegate { };
        public event Action OpenLeaderBoard = delegate { };
        public event Action LoadGame = delegate { };

        private WelcomePopup _welcomePopup;

        public WelcomeController(ViewFactory viewFactory, RootSceneContainer sceneContainer, UserStorage userStorage) : base(sceneContainer, viewFactory, userStorage)
        {
        }

        public void Start()
        {
            _welcomePopup = _viewFactory.CreateView<WelcomePopup>();
            _welcomePopup.UserClickedStartGame += OnUserClickedStart;
            _welcomePopup.UserClickedLeaderBoardButton += OnUserClickedLeaderBoard;
            _welcomePopup.UserClickedLoadGameButton += OnUserClickedLoadGameButton;
            
            var view = _welcomePopup.View;
            _rootScene.AddChild(view);
        }

        private void OnUserClickedLoadGameButton()
        {
            LoadGame();
        }

        private void OnUserClickedLeaderBoard()
        {
            OpenLeaderBoard();
        }

        public void OnUserClickedStart(string userName)
        {
            var isUserExist = _userStorage.CheckExistingUsers(userName);
            if (isUserExist)
            {
                _welcomePopup.ExistedNameEntered();
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
                _welcomePopup.UserClickedLeaderBoardButton -= OnUserClickedLeaderBoard;
                _welcomePopup.UserClickedLoadGameButton -= OnUserClickedLoadGameButton;
                _welcomePopup.Dispose();
            }
        }
    }
}