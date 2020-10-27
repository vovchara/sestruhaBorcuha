using Monosyne;
using Monosyne.Scene.V3;
using Scene.Model;
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
        private string _userName;
        private readonly long _userScore;
        private readonly LevelConfigStorage _levelConfigStorage;

        public LobbyController()
        {
            _userName = UserStorage.getInstance().UserName;
            _userScore = UserStorage.getInstance().UserScore;
            _levelConfigStorage = LevelConfigStorage.getInstance();
        }
        public void Start()
        {
            _lobbyPopup = new LobbyPopup(_game, _userName, _userScore);
            var view = _lobbyPopup.View;
            _rootScene.AddChild(view);

            _lobbyPopup.backBtnClicked += OnUserClickedBackBtn;
            _lobbyPopup.lvlBtnIsClicked += OnUserClicked_lvlBtn;

            var levelsConfigs = _levelConfigStorage.GetAllLevelConfigs();
            foreach (LevelConfigModel conf in levelsConfigs)
            {
                if(conf.MinScores <= _userScore)
                {
                    _lobbyPopup.EnableLevel(conf.LevelId);
                }
            }
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