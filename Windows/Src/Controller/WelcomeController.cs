using System;
using Monosyne;
using Monosyne.Scene.V3;
using Scene.Model;
using Scene.Storage;
using Scene.View;

namespace Scene.Controller
{
    public class WelcomeController : IDisposable
    {
        public event Action StartNewGame = delegate { };
        
        private readonly Game _game;
        private readonly RenderStatesNode _rootScene;
        private WelcomePopup _welcomePopup;

        public WelcomeController()
        {
            _game = RootStorage.getInstance().Game;
            _rootScene = RootStorage.getInstance().RootScene;
        }

        public void Start()
        {
            _welcomePopup = new WelcomePopup(_game);
            _welcomePopup.StartGameClicked += OnStartGameClicked;
            
            var view = _welcomePopup.View;
            _rootScene.AddChild(view);
        }

        private void OnStartGameClicked(string userName)
        {
            var userModel = new UserModel(userName, 0);
            UserStorage.getInstance().User = userModel;

            StartNewGame();
        }

        public void Dispose()
        {
            if (_welcomePopup != null)
            {
                _welcomePopup.StartGameClicked -= OnStartGameClicked;
                _welcomePopup.Dispose();
            }
        }
    }
}