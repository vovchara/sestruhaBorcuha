using Monosyne;
using Monosyne.Scene.V3;
using Newtonsoft.Json;
using Scene.Model;
using Scene.Src;
using Scene.Src.Controller;
using Scene.Src.Model;
using Scene.Src.View;
using System;

namespace Scene.Controller
{
    public class LobbyController: ControllerBase
    {
        public event Action OpenWelcomePopup = delegate { };
        public event Action<int> OpenLevelPopup = delegate { };

        private LobbyPopup _lobbyPopup;
        private readonly UserModel _myUserModel;
        private readonly LevelConfigStorage _levelConfigStorage;

        public LobbyController()
        {
            _myUserModel = UserStorage.getInstance().MyUserModel;
            _levelConfigStorage = LevelConfigStorage.getInstance();
        }
        public void Start()
        {
            _lobbyPopup = new LobbyPopup(_game, _myUserModel.UserName, _myUserModel.UserScore);
            var view = _lobbyPopup.View;
            _rootScene.AddChild(view);

            _lobbyPopup.backBtnClicked += OnUserClickedBackBtn;
            _lobbyPopup.lvlBtnIsClicked += OnUserClicked_lvlBtn;

            var levelsConfigs = _levelConfigStorage.GetAllLevelConfigs();
            if(levelsConfigs == null)
            {
                return;
            }
            else
            {
                foreach (var conf in levelsConfigs)
                {
                    if (conf.MinScores <= _myUserModel.UserScore)
                    {
                        _lobbyPopup.EnableLevel(conf.LevelId);
                    }
              }
            }
        }

        private void OnUserClicked_lvlBtn(int levelNumber)
        {
            OpenLevelPopup(levelNumber);
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