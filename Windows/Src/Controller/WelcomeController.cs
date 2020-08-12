using System;
using Monosyne;
using Monosyne.Scene.V3;
using Scene.Model;
using Scene.View;

namespace Scene.Controller
{
    public class WelcomeController
    {
        public event Action StartNewGame = delegate { };

        private readonly Game _game;
        private readonly RenderStatesNode _rootScene;

        private WelcomePopup _welcomePopup;

        public WelcomeController()
        {
            var rootStorage = RootStorage.getInstance();
            _game = rootStorage.Game;
            _rootScene = rootStorage.RootScene;
        }

        public void Start()
        {
            _welcomePopup = new WelcomePopup(_game);
            _welcomePopup.UserClickedStartGame += OnUserClickedStart;
            
            var view = _welcomePopup.View;
            _rootScene.AddChild(view);
        }

        private void OnUserClickedStart(string userName)
        {
            UserStorage.getInstance().UserName = userName;
            StartNewGame();
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