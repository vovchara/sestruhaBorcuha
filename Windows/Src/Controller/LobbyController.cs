using Scene.Model;
using Scene.Src.Controller;
using Scene.Src.Infra;
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

        public LobbyController(ViewFactory viewFactory, RootSceneContainer sceneContainer, LevelConfigStorage levelConfigStorage, UserStorage userStorage) : base(sceneContainer, viewFactory, userStorage)
        {
            _myUserModel = userStorage.MyUserModel;
            _levelConfigStorage = levelConfigStorage;
        }
        public void Start()
        {
            _lobbyPopup = _viewFactory.CreateView<LobbyPopup>();
            var view = _lobbyPopup.View;
            _rootScene.AddChild(view);

            _lobbyPopup.backBtnClicked += OnUserClickedBackBtn;
            _lobbyPopup.lvlBtnIsClicked += OnUserClicked_lvlBtn;

            var levelsConfigs = _levelConfigStorage.GetAllLevelConfigs();
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