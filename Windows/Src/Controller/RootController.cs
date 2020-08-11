using System;
using Monosyne;
using Monosyne.Scene.V3;
using Scene.Storage;

namespace Scene.Controller
{
    public class RootController : IDisposable
    {
        private WelcomeController _welcomeController;
        private LobbyController _lobbyController;

        public RootController(Game game, RenderStatesNode rootSceneNode)
        {
            InitializeRootStorage(game, rootSceneNode);
        }
        
        private void InitializeRootStorage(Game game, RenderStatesNode rootSceneNode)
        {
            var rootStorage = RootStorage.getInstance();
            rootStorage.Game = game;
            rootStorage.RootScene = rootSceneNode;
        }

        public void Start()
        {
            CreateAndRunWelcome();
        }

        public void Dispose()
        {
            DestroyWelcomeIfNeeded();
            DestroyLobbyIfNeeded();
        }
        
        private void CreateAndRunWelcome()
        {
            _welcomeController = new WelcomeController();
            _welcomeController.StartNewGame += StartNewGameHandler;
            _welcomeController.Start();
        }
        
        private void StartNewGameHandler()
        {
            DestroyWelcomeIfNeeded();
            CreateAndRunLobby();
        }

        private void DestroyWelcomeIfNeeded()
        {
            if (_welcomeController != null)
            {
                _welcomeController.StartNewGame -= StartNewGameHandler;
                _welcomeController?.Dispose();
                _welcomeController = null;
            }
        }
        
        private void CreateAndRunLobby()
        {
            _lobbyController = new LobbyController();
            _lobbyController.Start();
        }

        private void DestroyLobbyIfNeeded()
        {
            if (_lobbyController != null)
            {
                _lobbyController.Dispose();
                _lobbyController = null;
            }
        }
    }
}