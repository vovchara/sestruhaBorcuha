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
            SaveUserToDisk();

            _lobbyPopup = new LobbyPopup(_game, _myUserModel.UserName, _myUserModel.UserScore);
            var view = _lobbyPopup.View;
            _rootScene.AddChild(view);

            _lobbyPopup.backBtnClicked += OnUserClickedBackBtn;
            _lobbyPopup.lvlBtnIsClicked += OnUserClicked_lvlBtn;

            var levelsConfigs = _levelConfigStorage.GetAllLevelConfigs();
            //  var levelsConfigs = System.IO.File.Exists(ConfigConstants.LevelConfigPath);
            //  var savedLevelConfigs = System.IO.File.ReadAllText(ConfigConstants.LevelConfigPath);
            //   var arrayOfLevelConfigs = JsonConvert.DeserializeObject<LevelConfigModel[]>(savedLevelConfigs);
            foreach (var conf in levelsConfigs)
            {
                if(conf.MinScores <= _myUserModel.UserScore)
                {
                    _lobbyPopup.EnableLevel(conf.LevelId);
                }
            }
        }

        private void SaveUserToDisk()
        {
            var allUsers = UserStorage.getInstance().AllUsers;
            var allUsersJson = JsonConvert.SerializeObject(allUsers, Formatting.Indented);
            System.IO.File.WriteAllText(ConfigConstants.UserSavePath, allUsersJson);
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