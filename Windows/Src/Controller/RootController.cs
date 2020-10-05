using Monosyne;
using Monosyne.Scene.V3;
using Scene.Model;
using Scene.Src.Controller;
using Scene.Src.View;
using System;

namespace Scene.Controller
{
    public class RootController
    {
        private WelcomeController _welcomeController;
        private LobbyController _lobbyController;
        private LevelController _levelController;

        public RootController(Game game, RenderStatesNode rootSceneNode)
        {
            var rootStorage = RootStorage.getInstance();
            rootStorage.Game = game;
            rootStorage.RootScene = rootSceneNode;
        }

        public void Start()
        {
            _welcomeController = new WelcomeController();
            _welcomeController.StartNewGame += StartNewGameHandler;
            _welcomeController.Start();
        }

        private void StartNewGameHandler()
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
            _levelController.OpenLobbyScreen += OpenLobbyScreenHandler;
        }

        private void OpenLobbyScreenHandler()
        {
            DisposeLevelIfNeded();
            StartNewGameHandler();
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
        }

        private void DisposeLevelIfNeded()
        {
            if (_levelController != null)
            {
                _levelController.OpenLobbyScreen -= OpenLobbyScreenHandler;
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
                _welcomeController.StartNewGame -= StartNewGameHandler;
                _welcomeController.Dispose();
                _welcomeController = null;
            }
        }
    }
}