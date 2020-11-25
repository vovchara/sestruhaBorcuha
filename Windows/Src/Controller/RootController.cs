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
    public class RootController
    {
        private WelcomeController _welcomeController;
        private LobbyController _lobbyController;
        private LevelController _levelController;
        private LeaderBoardController _ledearBoardController;
        private LoadGameController _loadGameController;

        public RootController(Game game, RenderStatesNode rootSceneNode)
        {
            var rootStorage = RootStorage.getInstance();
            rootStorage.Game = game;
            rootStorage.RootScene = rootSceneNode;
        }

        public void Start()
        {
            var userSaves = new LoadUserSavesController();
            userSaves.Start();
            userSaves.Dispose();
            CreateWelcomeController();
        }

        private void CreateWelcomeController()
        {
            _welcomeController = new WelcomeController();
            _welcomeController.StartNewGame += OpenLobbyHandler;
            _welcomeController.OpenLeaderBoard += OpenLeaderBoardHandler;
            _welcomeController.LoadGame += LoadGameHandler;
            _welcomeController.Start();
        }

        private void LoadGameHandler()
        {
            DisposeWelcomeIfNeeded();
            _loadGameController = new LoadGameController();
            _loadGameController.Start();
            _loadGameController.CloseLoadGamePopup += CloseLoadGamePopupHandler;
            _loadGameController.ContinueGameForUser += ContinueGameForUserHandler;
        }

        private void ContinueGameForUserHandler(UserModel userSave)
        {
            DisposeLoadGameControllerIfNeeded();
            UserStorage.getInstance().MyUserModel = userSave;
            OpenLobbyHandler();
        }

        private void CloseLoadGamePopupHandler()
        {
            DisposeLoadGameControllerIfNeeded();
            CreateWelcomeController();
        }

        private void OpenLeaderBoardHandler()
        {
            DisposeWelcomeIfNeeded();
            _ledearBoardController = new LeaderBoardController();
            _ledearBoardController.Start();
            _ledearBoardController.CloseLeaderBoard += CloseLeaderBoardHandler;
        }

        private void CloseLeaderBoardHandler()
        {
            DisposeLeaderBoardIfNeeded();
            CreateWelcomeController();
        }

        private void OpenLobbyHandler()
        {
            DisposeWelcomeIfNeeded();
            SaveUserToDisk();
            _lobbyController = new LobbyController();
            _lobbyController.Start();
            _lobbyController.OpenWelcomePopup += OpenWelcomePopupHandler;
            _lobbyController.OpenLevelPopup += StartLvl;
        }

        private void SaveUserToDisk()
        {
            var allUsers = UserStorage.getInstance().AllUsers;
            var allUsersJson = JsonConvert.SerializeObject(allUsers, Formatting.Indented);
            System.IO.File.WriteAllText(ConfigConstants.UserSavePath, allUsersJson);
        }

        private void StartLvl(int levelNumber)
        {
            DisposeLobbyIfNeeded();
            _levelController = new LevelController(levelNumber);
            _levelController.Start();
            _levelController.OpenLobbyScreen += BackFromLevelToLobby;
        }

        private void BackFromLevelToLobby()
        {
            DisposeLevelIfNeded();
            OpenLobbyHandler();
        }

        private void OpenWelcomePopupHandler()
        {
            DisposeLobbyIfNeeded();
            Start();
        }

        public void Dispose()
        {
            DisposeWelcomeIfNeeded();
            DisposeLobbyIfNeeded();
            DisposeLevelIfNeded();
            DisposeLeaderBoardIfNeeded();
            DisposeLoadGameControllerIfNeeded();
        }

        private void DisposeLeaderBoardIfNeeded()
        {
            if (_ledearBoardController != null)
            {
                _ledearBoardController.CloseLeaderBoard -= CloseLeaderBoardHandler;
                _ledearBoardController.Dispose();
                _ledearBoardController = null;
            }
        }

        private void DisposeLoadGameControllerIfNeeded()
        {
            if (_loadGameController != null)
            {
                _loadGameController.CloseLoadGamePopup -= CloseLoadGamePopupHandler;
                _loadGameController.ContinueGameForUser -= ContinueGameForUserHandler;
                _loadGameController.Dispose();
                _loadGameController = null;
            }
        }

        private void DisposeLevelIfNeded()
        {
            if (_levelController != null)
            {
                _levelController.OpenLobbyScreen -= BackFromLevelToLobby;
                _levelController.Dispose();
                _levelController = null;
            }
        }
        private void DisposeLobbyIfNeeded()
        {
            if (_lobbyController != null)
            {
                _lobbyController.OpenWelcomePopup -= OpenWelcomePopupHandler;
                _lobbyController.OpenLevelPopup -= StartLvl;
                _lobbyController.Dispose();
                _lobbyController = null;
            }
        }

        private void DisposeWelcomeIfNeeded()
        {
            if (_welcomeController != null)
            {
                _welcomeController.StartNewGame -= OpenLobbyHandler;
                _welcomeController.OpenLeaderBoard -= OpenLeaderBoardHandler;
                _welcomeController.LoadGame -= LoadGameHandler;
                _welcomeController.Dispose();
                _welcomeController = null;
            }
        }
    }
}