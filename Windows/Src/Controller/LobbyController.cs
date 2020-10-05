using Monosyne;
using Monosyne.Scene.V3;
using Scene.Model;
using Scene.Src.Controller;
using Scene.Src.View;
using System;

namespace Scene.Controller
{
    public class LobbyController: ControllerBase
    {
        public event Action OpenWelcomePopup = delegate { };
        public event Action<int> OpenLevelPopup = delegate { };

        private LobbyPopup _lobbyPopup;
        private string _userData;

        public LobbyController()
        {
            _userData = UserStorage.getInstance().UserName;  
        }
        public void Start()
        {
            _lobbyPopup = new LobbyPopup(_game, _userData);
            var view = _lobbyPopup.View;
            _rootScene.AddChild(view);

            _lobbyPopup.backBtnClicked += OnUserClickedBackBtn;
            _lobbyPopup.lvlBtnIsClicked += OnUserClicked_lvlBtn;
        }

        private void OnUserClicked_lvlBtn(int levelNumber)
        {
            OpenLevelPopup(levelNumber);
            Dispose();
        }

        private void OnUserClickedBackBtn()
        {
            OpenWelcomePopup();
        }

        public void Dispose()
        {
            if (_lobbyPopup != null)
            {
                _lobbyPopup.backBtnClicked -= OnUserClickedBackBtn;
                _lobbyPopup.lvlBtnIsClicked -= OnUserClicked_lvlBtn;
                _lobbyPopup.Dispose();
            }
        }
    }
}