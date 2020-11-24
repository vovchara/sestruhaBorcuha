using Monosyne;
using Monosyne.Scene.V3;
using Scene.Model;
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
            var userSaves = new UserSavesController();
            userSaves.Start();
            userSaves.Dispose();

            _welcomeController = new WelcomeController();
            _welcomeController.StartNewGame += OpenLobbyHandler;
            _welcomeController.OpenLeaderBoard += OpenLeaderBoardHandler;
            _welcomeController.LoadGame += LoadGameHandler;
            _welcomeController.Start();
        }

        private void LoadGameHandler()
        {
            _welcomeController.Dispose();
            _loadGameController = new LoadGameController();
            _loadGameController.Start();
            _loadGameController.CloseLoadGamePopup += CloseLoadGamePopupHandler;
            _loadGameController.ContinueGameForUser += ContinueGameForUserHandler;
        }

        private void ContinueGameForUserHandler(UserModel userSave)
        {
            UserStorage.getInstance().MyUserModel = userSave;
            OpenLobbyHandler();
        }

        private void CloseLoadGamePopupHandler()
        {
            DisposeLoadGamePopupIfNeeded();
            Start();
        }

        private void OpenLeaderBoardHandler()
        {
            _welcomeController.Dispose();
            _ledearBoardController = new LeaderBoardController();
            _ledearBoardController.Start();
            _ledearBoardController.CloseLeaderBoard += CloseLeaderBoardHandler;
        }

        private void CloseLeaderBoardHandler()
        {
            DisposeLeaderBoardIfNeeded();
            Start();
        }

        private void OpenLobbyHandler()
        {
            DisposeWelcomeIfNeeded();
            
            _lobbyController = new LobbyController();
            _lobbyController.Start();
            _lobbyController.OpenWelcomePopup += OpenWelcomePopupHandler;
            _lobbyController.OpenLevelPopup += StartLvl;
        }

        private void StartLvl(int levelNumber)
        {
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
            DisposeLoadGamePopupIfNeeded();
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

        private void DisposeLoadGamePopupIfNeeded()
        {
            if (_loadGameController != null)
            {
                _loadGameController.CloseLoadGamePopup -= CloseLoadGamePopupHandler;
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
                _lobbyController.Dispose();
                _lobbyController = null;
            }
        }

        private void DisposeWelcomeIfNeeded()
        {
            if (_welcomeController != null)
            {
                _welcomeController.StartNewGame -= OpenLobbyHandler;
                _welcomeController.Dispose();
                _welcomeController = null;
            }
        }
    }
}