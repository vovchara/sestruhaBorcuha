using Monosyne;
using Monosyne.Scene.V3;
using Scene.Model;
using Scene.Src.View;
using System;

namespace Scene.Controller
{
    public class LobbyController
    {
        public event Action OpenWelcomePopup = delegate { };

        private LobbyPopup _lobbyPopup;
        private readonly Game _game;
        private readonly RenderStatesNode _rootScene;
        private string _userData;

        public LobbyController()
        {
            var rootStorage = RootStorage.getInstance();
            _game = rootStorage.Game;
            _rootScene = rootStorage.RootScene;
            _userData = UserStorage.getInstance().UserName;
            
        }
        public void Start()
        {
            _lobbyPopup = new LobbyPopup(_game, _userData);
            var view = _lobbyPopup.View;
            _rootScene.AddChild(view);

            _lobbyPopup.backBtnClicked += _lobbyPopup_backBtnClicked;
        }

        private void _lobbyPopup_backBtnClicked()
        {
            OpenWelcomePopup();
        }

        public void Dispose()
        {
            if (_lobbyPopup != null)
            {
                _lobbyPopup.backBtnClicked -= _lobbyPopup_backBtnClicked;
                _lobbyPopup.Dispose();
            }
        }
    }
}